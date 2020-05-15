using System;

namespace komyuter.core.DomainClasses
{
    public class Frequencies
    {
        public int id { get; set; }
        public string trip_id { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public int headway_secs { get; set; }
        public int exact_times { get; set; }
    }
}
    