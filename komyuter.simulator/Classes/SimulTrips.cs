using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.simulator.Classes
{
    public class SimulTrips
    {
        public string trip_id { get; set; }
        public string trip_headsign { get; set; }
        public string route_id { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public int direction_id { get; set; }
        public string shape_id { get; set; }

        public int delay { get; set; }
        public TimeSpan actual_start_time { get; set; }
        public TimeSpan trip_update_time { get; set; }
        public string update_status { get; set; }
    }
}
