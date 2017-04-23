using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TunNet.Models
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string FirstParameter { get; set; }
        public string SecondParameter { get; set; }
        public string Result { get; set; }

        public LogEntry(DateTime timestamp, string firstParam, string secondParam, string result)
        {
            Timestamp = timestamp;
            FirstParameter = firstParam;
            SecondParameter = secondParam;
            Result = result;
        }

        public LogEntry(string logEntryString)
        {
            string[] line = logEntryString.Trim().Split(',');
            Timestamp = Convert.ToDateTime(line[0].Trim());
            FirstParameter = line[1].Trim();
            SecondParameter = line[2].Trim();
            Result = line[3].Trim();
        }

        public override string ToString()
        {
            return String.Format("{0:d/M/yyyy HH:mm:ss}", Timestamp) + ", " +
                   FirstParameter + ", " +
                   SecondParameter + ", " +
                   Result;
        }
    }
}