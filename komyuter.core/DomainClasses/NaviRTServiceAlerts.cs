using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
    public class NaviRTServiceAlerts
    {
        public int id { get; set; }
        public int rt_id { get; set; }
        public string route_id { get; set; }
        public string stop_id { get; set; }
        public string header { get; set; }
        public string description { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
