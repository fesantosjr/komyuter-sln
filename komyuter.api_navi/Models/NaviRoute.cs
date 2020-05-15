using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using komyuter.core.DomainClasses;

namespace komyuter.api_navi.Models
{
    public class NaviRoute
    {
        public List<NaviStop> navi_stops { get; set; }
        public List<Shapes> navi_shapes { get; set; }
        public List<NaviTripHistory> TripPrevious { get; set; }
        public List<NaviTripHistory> TripNext { get; set; }
    }

    public class NaviTripHistory
    {
        public string trip_id { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public string type { get; set; }
    }
}