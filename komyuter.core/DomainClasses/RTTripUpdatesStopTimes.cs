using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
    public class RTTripUpdatesStopTimes
    {
        public int id { get; set; }
        public int rt_trip_update_id { get; set; }
        public int stop_sequence { get; set; }
        public int arrival_delay { get; set; }
        public string stop_id { get; set; }
    }
}
