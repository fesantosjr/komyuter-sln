using System;

namespace komyuter.core.DomainClasses
{
    public class Calendar
    {
        public string service_id { get; set; }
        public bool monday { get; set; }
        public bool tuesday { get; set; }
        public bool wednesday { get; set; }
        public bool thursday { get; set; }
        public bool friday { get; set; }
        public bool saturday { get; set; }
        public bool sunday { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

    }
}
