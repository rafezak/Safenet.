using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safenet_2._0.Models
{
    public class LogEntry
    {

        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
    }
}
