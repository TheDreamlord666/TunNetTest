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
    public class CalculatorControllerTests
    {
        [TestMethod]
        public void Test_Add_Basic()
        {
            CalculatorController cc = new CalculatorController();
            cc.Request = new HttpRequestMessage();
            cc.Configuration = new HttpConfiguration();
            var addParams = new AdditionParams { Param1 = "123", Param2 = "321"};

            HttpResponseMessage response = cc.AddNumbers(addParams);

            CalculatorResponse result;
            Assert.IsTrue(response.TryGetContentValue<CalculatorResponse>(out result));
            Assert.AreEqual("444", result.Result);
        }

        [TestMethod]
        public void Test_Add_LargeNumbers()
        {
            CalculatorController cc = new CalculatorController();
            cc.Request = new HttpRequestMessage();
            cc.Configuration = new HttpConfiguration();
            var addParams = new AdditionParams
            {
                Param1 = "123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890",
                Param2 = "123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890"
            };

            HttpResponseMessage response = cc.AddNumbers(addParams);

            CalculatorResponse result;
            Assert.IsTrue(response.TryGetContentValue<CalculatorResponse>(out result));
            Assert.AreEqual("247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780247135780", 
                result.Result);
        }

        [TestMethod]
        public void Test_Add_Param1Wrong()
        {
            CalculatorController cc = new CalculatorController();
            cc.Request = new HttpRequestMessage();
            cc.Configuration = new HttpConfiguration();
            var addParams = new AdditionParams
            {
                Param1 = "XYZ123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890",
                Param2 = "123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890"
            };

            HttpResponseMessage response = cc.AddNumbers(addParams);

            CalculatorResponse result;
            Assert.IsTrue(response.TryGetContentValue<CalculatorResponse>(out result));
            Assert.IsNotNull(result.Error);
            Assert.IsNull(result.Result);
            Assert.IsFalse(result.Param1Valid);
            Assert.IsTrue(result.Param2Valid);
        }

        [TestMethod]
        public void Test_Add_Param2Wrong()
        {
            CalculatorController cc = new CalculatorController();
            cc.Request = new HttpRequestMessage();
            cc.Configuration = new HttpConfiguration();
            var addParams = new AdditionParams
            {
                Param1 = "123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890",
                Param2 = "123abc567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890"
            };

            HttpResponseMessage response = cc.AddNumbers(addParams);

            CalculatorResponse result;
            Assert.IsTrue(response.TryGetContentValue<CalculatorResponse>(out result));
            Assert.IsNotNull(result.Error);
            Assert.IsNull(result.Result);
            Assert.IsTrue(result.Param1Valid);
            Assert.IsFalse(result.Param2Valid);
        }

        [TestMethod]
        public void Test_Add_BothParamsWrong()
        {
            CalculatorController cc = new CalculatorController();
            cc.Request = new HttpRequestMessage();
            cc.Configuration = new HttpConfiguration();
            var addParams = new AdditionParams
            {
                Param1 = "123xyz",
                Param2 = "-345abc321"
            };

            HttpResponseMessage response = cc.AddNumbers(addParams);

            CalculatorResponse result;
            Assert.IsTrue(response.TryGetContentValue<CalculatorResponse>(out result));
            Assert.IsNotNull(result.Error);
            Assert.IsNull(result.Result);
            Assert.IsFalse(result.Param1Valid);
            Assert.IsFalse(result.Param2Valid);
        }

        [TestMethod]
        public void Test_Add_NegativeValues()
        {
            CalculatorController cc = new CalculatorController();
            cc.Request = new HttpRequestMessage();
            cc.Configuration = new HttpConfiguration();
            var addParams = new AdditionParams
            {
                Param1 = "123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890",
                Param2 = "-123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890123567890"
            };

            HttpResponseMessage response = cc.AddNumbers(addParams);

            CalculatorResponse result;
            Assert.IsTrue(response.TryGetContentValue<CalculatorResponse>(out result));
            Assert.AreEqual("0", result.Result);
        }

        [TestMethod]
        public void Test_Add_BothParamsWrongCheckDecimals()
        {
            CalculatorController cc = new CalculatorController();
            cc.Request = new HttpRequestMessage();
            cc.Configuration = new HttpConfiguration();
            var addParams = new AdditionParams
            {
                Param1 = "123.123",
                Param2 = "321.321"
            };

            HttpResponseMessage response = cc.AddNumbers(addParams);

            CalculatorResponse result;
            Assert.IsTrue(response.TryGetContentValue<CalculatorResponse>(out result));
            Assert.IsNotNull(result.Error);
            Assert.IsNull(result.Result);
            Assert.IsFalse(result.Param1Valid);
            Assert.IsFalse(result.Param2Valid);
        }
    }
}
