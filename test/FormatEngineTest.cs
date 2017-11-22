using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSMore.Formatting;

namespace FormatEngineTest
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            var rs = RunspaceFactory.CreateRunspace(InitialSessionState.CreateDefault());
            rs.Open();
            Runspace.DefaultRunspace = rs;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var result = FormatEngine.Format(typeof(string)).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(typeof(string).ToString(), result[0]);
        }

        private IEnumerable<ListEntry> ListEntries(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                yield return new ListEntry(propertyName);
            }
        }

        [TestMethod]
        public void TestListFormat()
        {
            var currentProcess = Process.GetCurrentProcess();
            dynamic p = new PSObject(currentProcess);

            var properties = new [] {"ProcessName", "MachineName", "Company"};
            var l = new ListFormat(ListEntries(properties));
            IEnumerable<string> r = FormatEngine.Format(p, l);
            var result = r.ToArray();
            Assert.AreEqual(3, result.Length);
            var fmt = "{0,-" + properties.Max(prop => prop.Length) + "} : {1}";
            Assert.AreEqual(string.Format(fmt, "ProcessName", currentProcess.ProcessName), result[0]);
            Assert.AreEqual(string.Format(fmt, "MachineName", currentProcess.MachineName), result[1]);
            Assert.AreEqual(string.Format(fmt, "Company", p.Company), result[2]);
        }
    }
}
