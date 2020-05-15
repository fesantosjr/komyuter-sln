using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace komyuter.api_navi.Models
{
    public class NaviStop
    {
        public string stop_id { get; set; }
        public int stop_sequence { get; set; }
        public string stop_name { get; set; }
        public double stop_lat { get; set; }
        public double stop_lon { get; set; }
        public int default_zoom { get; set; }
        public double center_lat { get; set; }
        public double center_lon { get; set; }
    }
}