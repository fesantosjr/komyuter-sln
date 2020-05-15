using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace komyuter.api_navi.Models
{
    public class NaviFrequency
    {
        public NaviFrequency() { }

        public NaviFrequency(int _id, string _trip_id, TimeSpan _start_time, TimeSpan _arrival_time, string _trip_headsign, string _stop_name, int _direction_id, int _delay)
        {
            this.id = _id;
            this.trip_id = _trip_id;
            this.start_time = _start_time;
            this.arrival_time = _arrival_time;
            this.trip_headsign = _trip_headsign;
            this.stop_name = _stop_name;
            this.direction_id = _direction_id;
            this.rt_delay = _delay;
        }

        public Int64 id { get; set; }
        public string trip_id { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public int headway_secs { get; set; }
        public int exact_times { get; set; }
        public string trip_headsign { get; set; }
        public TimeSpan arrival_time { get; set; }
        public string stop_name { get; set; }
        public double stop_lat { get; set; }
        public double stop_lon { get; set; }
        public int direction_id { get; set; }

        public int rt_delay { get; set; }
    }
}