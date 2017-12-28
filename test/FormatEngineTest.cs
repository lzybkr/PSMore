using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Management.Infrastructure;
using PSMore.Formatting;
using Xunit;

namespace Test
{
    public class FormatEngine
    {
        public FormatEngine()
        {
            var iss = InitialSessionState.CreateDefault();
            var rs = RunspaceFactory.CreateRunspace(iss);
            rs.Open();
            Runspace.DefaultRunspace = rs;

            using (var ps = PowerShell.Create(RunspaceMode.CurrentRunspace))
            {
                ps.AddCommand("Import-Module").AddArgument(typeof(PSMore.Commands.OutStringCommand).Assembly.Location).Invoke();
            }
        }

        string[] Format(object obj)
        {
            using (var ps = PowerShell.Create(RunspaceMode.CurrentRunspace))
            {
                ps.AddCommand("Out-PSMoreString").AddParameter("Stream");
                return ps.Invoke<string>(new[] { obj }).ToArray();
            }
        }

        static string[] FormatList(object obj, IEnumerable<string> properties)
        {
            using (var ps = PowerShell.Create(RunspaceMode.CurrentRunspace))
            {
                ps.AddCommand("Format-PSMoreList").AddParameter("Property", properties.ToArray());
                ps.AddCommand("Out-PSMoreString").AddParameter("Stream");
                return ps.Invoke<string>(new[] { obj }).ToArray();
            }
        }

        [Fact]
        public void TestToString()
        {
            var result = Format(typeof(string));
            Assert.Single(result);
            Assert.Equal(typeof(string).ToString(), result[0]);
        }

        private IEnumerable<ListDescriptorEntry> ListEntries(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                yield return new ListDescriptorPropertyEntry(propertyName);
            }
        }

        [Fact]
        public void TestListFormat()
        {
            var currentProcess = Process.GetCurrentProcess();
            dynamic p = new PSObject(currentProcess);

            var properties = new [] {"ProcessName", "MachineName", "Company"};
            var result = FormatList(p, properties);
            Assert.Equal(4, result.Length);
            var fmt = "{0,-" + properties.Max(prop => prop.Length) + "} : {1}";
            Assert.Equal(string.Format(fmt, "ProcessName", currentProcess.ProcessName), result[0]);
            Assert.Equal(string.Format(fmt, "MachineName", currentProcess.MachineName), result[1]);
            Assert.Equal(string.Format(fmt, "Company", p.Company), result[2]);
            Assert.Equal("", result[3]);

            properties = new[] { "Company", "ProcessName" };
            result = FormatList(p, properties);
            Assert.Equal(3, result.Length);
            fmt = "{0,-" + properties.Max(prop => prop.Length) + "} : {1}";
            Assert.Equal(string.Format(fmt, "Company", p.Company), result[0]);
            Assert.Equal(string.Format(fmt, "ProcessName", currentProcess.ProcessName), result[1]);
            Assert.Equal("", result[2]);

            properties = new[] { "Company", "ProcessName" };
            result = FormatList(p, properties);
            Assert.Equal(3, result.Length);
            fmt = "{0,-" + properties.Max(prop => prop.Length) + "} : {1}";
            Assert.Equal(string.Format(fmt, "Company", p.Company), result[0]);
            Assert.Equal(string.Format(fmt, "ProcessName", currentProcess.ProcessName), result[1]);
            Assert.Equal("", result[2]);
        }

        [Fact]
        public void TestWhen()
        {
            var currentProcess = Process.GetCurrentProcess();
            var cimInstance = new CimInstance("Win32_Process", "root/cimv2");
            cimInstance.CimInstanceProperties.Add(CimProperty.Create("ProcessId", currentProcess.Id, CimFlags.None));
            cimInstance.CimInstanceProperties.Add(CimProperty.Create("Name", currentProcess.ProcessName, CimFlags.None));
            cimInstance.CimInstanceProperties.Add(CimProperty.Create("HandleCount", currentProcess.HandleCount, CimFlags.None));
            cimInstance.CimInstanceProperties.Add(CimProperty.Create("WorkingSetSize", currentProcess.WorkingSet64, CimFlags.None));
            cimInstance.CimInstanceProperties.Add(CimProperty.Create("VirtualSize", currentProcess.VirtualMemorySize64, CimFlags.None));

            var properties = new [] {"ProcessId", "Name", "HandleCount", "WorkingSetSize", "VirtualSize"};
            var result = Format(cimInstance);
            Assert.Equal(6, result.Length);
            var fmt = "{0,-" + properties.Max(prop => prop.Length) + "} : {1}";
            Assert.Equal(string.Format(fmt, "ProcessId", currentProcess.Id), result[0]);
            Assert.Equal(string.Format(fmt, "Name", currentProcess.ProcessName), result[1]);
            Assert.Equal(string.Format(fmt, "HandleCount", currentProcess.HandleCount), result[2]);
            Assert.Equal(string.Format(fmt, "WorkingSetSize", currentProcess.WorkingSet64), result[3]);
            Assert.Equal(string.Format(fmt, "VirtualSize", currentProcess.VirtualMemorySize64), result[4]);
            Assert.Equal("", result[5]);
        }
    }
}
