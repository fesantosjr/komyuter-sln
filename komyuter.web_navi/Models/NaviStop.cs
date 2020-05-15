using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace komyuter.web_navi.Models
{
    public class NaviStop
    {
        public string stop_id { get; set; }
        public int stop_sequence { get; set; }
        public string stop_name { get; set; }
        public double stop_lat { get; set; }
        public double stop_lon { get; set; }

        public TimeSpan arrival_time { get; set; }
        public TimeSpan departure_time { get; set; }

        public int delay { get; set; }
    }
}