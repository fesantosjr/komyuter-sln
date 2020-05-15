using System;

namespace komyuter.core.DomainClasses
{
    public class Stops
    {
        public string stop_id { get; set; }
        public string stop_code { get; set; }
        public string stop_name { get; set; }
        public string stop_desc { get; set; }
        public double stop_lat { get; set; }
        public double stop_lon { get; set; }
        public int zone_id { get; set; }
        public string stop_url { get; set; }
        public int location_type { get; set; }
        public int parent_station { get; set; }
        public int wheelchair_boarding { get; set; }

    }
}
