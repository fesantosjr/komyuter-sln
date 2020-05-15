using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
    public class SMSIncoming
    {
        public int sms_id { get; set; }
        public string message { get; set; }
        public DateTime receive_date { get; set; }
        public DateTime? process_date { get; set; }
        public string process_remarks { get; set; }
    }
}
