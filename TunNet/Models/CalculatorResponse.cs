using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TunNet.Models
{
    public class CalculatorResponse
    {
        public string Result { get; set; }
        public bool Param1Valid { get; set; }
        public bool Param2Valid { get; set; }
        public string Error { get; set; }
    }
}