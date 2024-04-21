using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safenet_2._0.Models
{
    public class Software
    {
        public string? Name { get; set; }
        
        public string? URL { get; set; }

        public bool IsBlocked { get; set; }
    }
}
