using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.GlobeClasses
{
    public class globe_sms_mo
    {
        public _inboundSMSMessageList inboundSMSMessageList { get; set; }
    }

    public class _inboundSMSMessageList
    {
        public List<_inboundSMSMessage> inboundSMSMessage { get; set; }
        public string numberOfMessagesInThisBatch { get; set; }
        public string resourceURL { get; set; }
        public string totalNumberOfPendingMessages { get; set; }
    }

    public class _inboundSMSMessage
    {
        public string dateTime { get; set; }
        public string destinationAddress { get; set; }
        public string messageId { get; set; }
        public string message { get; set; }
        public string resourceURL { get; set; }
        public string senderAddress { get; set; }
    }
}
