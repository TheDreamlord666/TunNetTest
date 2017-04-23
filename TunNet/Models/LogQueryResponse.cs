using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TunNet.Models
{
    public class LogQueryResponse
    {
        public LogEntry[] LogEntries { get; set; }
        public string Error { get; set; }
    }
}