using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;

namespace Safenet_2._0.Models
{
    public class Port
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LocalPorts { get; set; }
        public int Protocol {  get; set; }

        //to log history
        public List<LogEntry> LogEntries { get; set; }

    }
}
