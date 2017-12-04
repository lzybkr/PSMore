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
            var rs = RunspaceFactory.CreateRunspace(InitialSessionState.CreateDefault());
            rs.Open();
            Runspace.DefaultRunspace = rs;
        }

        static string[] Format(object o)
        {
            return PSMore.Formatting.FormatEngine.Format(o).ToArray();
        }


        static IEnumerable<string> Format(object o, FormatDirective directive)
        {
            return PSMore.Formatting.FormatEngine.Format(o, directive).ToArray();
        }

        [Fact]
        public void TestToString()
        {
            var result = Format(typeof(string));
            Assert.Single(result);
            Assert.Equal(typeof(string).ToString(), result[0]);
        }

        private IEnumerable<ListEntry> ListEntries(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                yield return new ListEntry(propertyName);
            }
        }

        [Fact]
        public void TestListFormat()
        {
            var currentProcess = Process.GetCurrentProcess();
            dynamic p = new PSObject(currentProcess);

            var properties = new [] {"ProcessName", "MachineName", "Company"};
            var l = new ListFormat(ListEntries(properties));
            var result = Format(p, l);
            Assert.Equal(3, result.Length);
            var fmt = "{0,-" + properties.Max(prop => prop.Length) + "} : {1}";
            Assert.Equal(string.Format(fmt, "ProcessName", currentProcess.ProcessName), result[0]);
            Assert.Equal(string.Format(fmt, "MachineName", currentProcess.MachineName), result[1]);
            Assert.Equal(string.Format(fmt, "Company", p.Company), result[2]);

            properties = new[] { "Company", "ProcessName" };
            l = new ListFormat(ListEntries(properties));
            result = Format(p, l);
            Assert.Equal(2, result.Length);
            fmt = "{0,-" + properties.Max(prop => prop.Length) + "} : {1}";
            Assert.Equal(string.Format(fmt, "Company", p.Company), result[0]);
            Assert.Equal(string.Format(fmt, "ProcessName", currentProcess.ProcessName), result[1]);
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
            Assert.Equal(5, result.Length);
            var fmt = "{0,-" + properties.Max(prop => prop.Length) + "} : {1}";
            Assert.Equal(string.Format(fmt, "ProcessId", currentProcess.Id), result[0]);
            Assert.Equal(string.Format(fmt, "Name", currentProcess.ProcessName), result[1]);
            Assert.Equal(string.Format(fmt, "HandleCount", currentProcess.HandleCount), result[2]);
            Assert.Equal(string.Format(fmt, "WorkingSetSize", currentProcess.WorkingSet64), result[3]);
            Assert.Equal(string.Format(fmt, "VirtualSize", currentProcess.VirtualMemorySize64), result[4]);
        }
    }
}
