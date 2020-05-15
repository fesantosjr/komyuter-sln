using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace komyuter.api_sms.Classes
{
    public class globe_sms_mox
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

/*
 {
  "inboundSMSMessageList":{
      "inboundSMSMessage":[
         {
            "dateTime":"Fri Nov 22 2013 12:12:13 GMT+0000 (UTC)",
            "destinationAddress":"tel:21581234",
            "messageId":null,
            "message":"Hello",
            "resourceURL":null,
            "senderAddress":"tel:+639171234567"
         }
       ],
       "numberOfMessagesInThisBatch":1,
       "resourceURL":null,
       "totalNumberOfPendingMessages":null
   }
}
     */
