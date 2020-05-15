using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
    public class MobileDevice
    {
        public int mobile_device_id { get; set; }
        public string mobile_number { get; set; }
        public string access_token { get; set; }
        public DateTime optin_date { get; set; }
        public DateTime? optout_date { get; set; }
    }
}
