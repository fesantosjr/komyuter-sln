using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
    public class LBSLog
    {
        public int log_id { get; set; }
        public int device_id { get; set; }
        public string log { get; set; }
        public DateTime date_created { get; set; }
    }
}
