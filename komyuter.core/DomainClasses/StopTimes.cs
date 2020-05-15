using System;

namespace komyuter.core.DomainClasses
{
    public class StopTimes
    {
        public int id { get; set; }
        public string trip_id { get; set; }
        public TimeSpan arrival_time { get; set; }
        //public int arrival_time_seconds { get; set; }
        public TimeSpan departure_time { get; set; }
        //public int departure_time_seconds { get; set; }
        public string stop_id { get; set; }
        public int stop_sequence { get; set; }
        public string stop_headsign { get; set; }
        public int pickup_type { get; set; }
        public int drop_off_type { get; set; }
        public string shape_dist_traveled { get; set; }
    }
}
