using System;

namespace komyuter.core.DomainClasses
{
    public class Routes
    {
        public string route_id { get; set; }
        public string agency_id { get; set; }
        public string route_short_name { get; set; }
        public string route_long_name { get; set; }
        public int route_type { get; set; }
        public string route_text_color { get; set; }
        public string route_color { get; set; }
        public string route_url { get; set; }
        public string route_desc { get; set; }


        public int default_zoom { get; set; }
        public double center_lat { get; set; }
        public double center_lon { get; set; }

    }
}
