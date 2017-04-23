using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Threading.Tasks;
using TunNet.Controllers;
using TunNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TunNet.Tests
{
    [TestClass]
    public class LogControllerTests
    {
        [TestMethod]
        public void Test_Read_Basic()
        {
            LogController lc = new LogController();
            lc.Request = new HttpRequestMessage();
            lc.Configuration = new HttpConfiguration();

            HttpResponseMessage response = lc.GetLogEntries(null);

            LogQueryResponse result;
            Assert.IsTrue(response.TryGetContentValue<LogQueryResponse>(out result));
            Assert.IsNotNull(result.LogEntries);
            Assert.AreEqual("", result.Error);
        }

        [TestMethod]
        public void Test_Read_Range()
        {
            LogController lc = new LogController();
            lc.Request = new HttpRequestMessage();
            lc.Configuration = new HttpConfiguration();
            var range = new LogRange { Start = 1, End = 10 };

            HttpResponseMessage response = lc.GetLogEntries(range);

            LogQueryResponse result;
            Assert.IsTrue(response.TryGetContentValue<LogQueryResponse>(out result));
            Assert.IsNotNull(result.LogEntries);
            Assert.AreEqual("", result.Error);
        }

        [TestMethod]
        public void Test_Read_RangeWrong()
        {
            LogController lc = new LogController();
            lc.Request = new HttpRequestMessage();
            lc.Configuration = new HttpConfiguration();
            var range = new LogRange { Start = 10, End = 1 };

            HttpResponseMessage response = lc.GetLogEntries(range);

            LogQueryResponse result;
            Assert.IsTrue(response.TryGetContentValue<LogQueryResponse>(out result));
            Assert.IsNotNull(result.LogEntries);
            Assert.AreEqual("", result.Error);
        }
    }
}
