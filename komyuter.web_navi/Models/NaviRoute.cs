using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using komyuter.core.DomainClasses;

namespace komyuter.web_navi.Models
{
    public class NaviRoute
    {
        public List<NaviStop> navi_stops { get; set; }
        public List<Shapes> navi_shapes { get; set; }
        public List<NaviTripHistory> TripPrevious { get; set; }
        public List<NaviTripHistory> TripNext { get; set; }
        public List<NaviReverseTrip> ReverseTrip { get; set; }

        public string SelectedRouteId { get; set; }
        public string SelectedTripId { get; set; }
        public string SelectedStopId { get; set; }
        public string SelectedDirectionId { get; set; }
        public string SelectedTripDate { get; set; }
        public string SelectedTripTime { get; set; }
        
    }

    public class NaviTripHistory
    {
        public string trip_id { get; set; }
        public TimeSpan actual_start_time { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public string type { get; set; }
    }

    public class NaviReverseTrip
    {
        public string trip_id { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public TimeSpan arrival_time { get; set; }
        public int direction_id { get; set; }
        public string trip_headsign { get; set; }
        public string orig_trip_headsign { get; set; }
    }
}