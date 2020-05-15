using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
    public class Trips
    {
        public string route_id { get; set; }
        public string service_id { get; set; }
        public string trip_id { get; set; }
        public string trip_headsign { get; set; }
        public string trip_short_name { get; set; }
        public int direction_id { get; set; }
        public string block_id { get; set; }
        public string shape_id { get; set; }
        public Int32?  wheelchair_accessible { get; set; }
        public Int32? bikes_allowed { get; set; }
        
    }
}
