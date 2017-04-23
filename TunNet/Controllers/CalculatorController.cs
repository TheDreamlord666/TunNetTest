using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TunNet.Models;
using System.Numerics;

namespace TunNet.Controllers
{
    public class CalculatorController : ApiController
    {
        [HttpPost]
        [Route("api/calculator/add")]
        public HttpResponseMessage AddNumbers(AdditionParams values)
        {
            try
            {
                CalculatorResponse result = new CalculatorResponse();

                BigInteger val1, val2;
                result.Param1Valid = BigInteger.TryParse(values.Param1, out val1);
                result.Param2Valid = BigInteger.TryParse(values.Param2, out val2);

                if (result.Param1Valid && result.Param2Valid)
                {
                    result.Result = BigInteger.Add(val1, val2).ToString();
                }
                else
                {
                    result.Error = "Invalid input parameters";
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                LogController.AddLogEntryAsync(new LogEntry(DateTime.Now, values.Param1, values.Param2, !string.IsNullOrEmpty(result.Result) ? result.Result : result.Error));
                return response;
            }
            catch (Exception ex)
            {
                // NOTE: proper exception logging with the selected logger should be implemented here
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Exception encountered during data processing: " + ex.Message);
                return response;
            }
            
        }
    }
}
