using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TunNet.Models;

namespace TunNet.Controllers
{
    public class LogController : ApiController
    {
        // NOTE: This should of course be in the application setup/configuration, wherever it is
        private static string logFileName = AppDomain.CurrentDomain.BaseDirectory + @"\TunNetLog.txt";

        [HttpGet]
        [Route("api/log/read")]
        public HttpResponseMessage GetLogEntries(LogRange range)
        {
            // if no range is provided, it will return the whole log file, otherwise only the entries between the specified range, to allow for pagination
            try
            {
                var entries = range != null ? ReadLog(range.Start, range.End) : ReadLog();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new LogQueryResponse { LogEntries = entries.ToArray(), Error = "" });
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, new LogQueryResponse { LogEntries = null, Error = "Unable to retrieve log entries. Error: " + e.Message });
                return response;
            }
        }

        public static async Task<bool> AddLogEntryAsync(LogEntry logEntry)
        {
            using (StreamWriter writer = File.AppendText(logFileName))
            {
                try
                {
                    await writer.WriteLineAsync(logEntry.ToString());
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        private List<LogEntry> ReadLog()
        {
            List<LogEntry> logEntries = new List<LogEntry>();
            try
            {
                if (File.Exists(logFileName))
                {
                    string[] lines = File.ReadAllLines(logFileName);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        logEntries.Add(new LogEntry(lines[i]));
                    }
                }
                return logEntries;
            }
            catch (Exception ex)
            {
                // NOTE: proper exception logging with the selected logger should be implemented here
                throw ex;
            }
        }

        private List<LogEntry> ReadLog(int start, int end)
        {
            List<LogEntry> logEntries = new List<LogEntry>();
            try
            {
                if (File.Exists(logFileName))
                {
                    List<string> lines = File.ReadLines(logFileName).Skip(start).Take(end - start).ToList();
                    for (int i = 0; i < lines.Count; i++)
                    {
                        logEntries.Add(new LogEntry(lines[i]));
                    }
                }
                return logEntries;
            }
            catch (Exception ex)
            {
                // NOTE: proper exception logging with the selected logger should be implemented here
                throw ex;
            }
        }
        
    }
}
