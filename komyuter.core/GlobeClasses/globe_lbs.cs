using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.GlobeClasses
{
    public class globe_lbs
    {
        public _terminalLocationList terminalLocationList { get; set; }
    }

    public class _terminalLocationList
    {
        public _terminalLocation terminalLocation { get; set; }
    }

    public class _terminalLocation
    {
        public string address { get; set; }
        public _currentLocation currentLocation { get; set; }
        public string locationRetrievalStatus { get; set; }
    }

    public class _currentLocation
    {
        public string accuracy { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string map_url { get; set; }
        public string timestamp { get; set; }
    }
}
