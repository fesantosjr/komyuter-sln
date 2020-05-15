using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.GlobeClasses
{
    public class globe_unsubscribe
    {
        public _unsubscribed unsubscribed { get; set; }
    }

    public class _unsubscribed
    {
        public string subscriber_number { get; set; }
        public string access_token { get; set; }
        public string time_stamp { get; set; }
    }
}
