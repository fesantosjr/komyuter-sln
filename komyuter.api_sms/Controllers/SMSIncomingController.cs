using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using komyuter.core.DomainClasses;
using komyuter.core.Common;
using komyuter.data;

using Newtonsoft.Json;
using System.Data.SqlClient;
using komyuter.core.GlobeClasses;
using Newtonsoft.Json.Linq;

namespace komyuter.api_sms.Controllers
{
    public class SMSIncomingController : ApiController
    {
        private KomyuterContext db = new KomyuterContext();

        // POST: api/SMSIncoming
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostSMSIncoming([FromBody]JToken jsonbody)
        {
            try
            {
                if (jsonbody.HasValues)
                {
                    string processResult = "";
                    if (((JProperty)jsonbody.First).Name == "error")
                    {
                        processResult = ((JProperty)jsonbody.First).Value.ToString();
                    }
                    else if (((JProperty)jsonbody.First).Name == "inboundSMSMessageList")
                    {
                        globe_sms_mo sms_mo = JsonConvert.DeserializeObject<globe_sms_mo>(jsonbody.ToString());
                        processResult = processMessage(sms_mo);
                    }
                    else
                    {
                        processResult = "Message not supported";
                    }

                    SMSIncoming sms = new SMSIncoming
                    {
                        message = jsonbody.ToString(),
                        receive_date = DateTime.UtcNow
                    };


                    // do message processing
                    sms.process_date = DateTime.UtcNow;
                    sms.process_remarks = processResult;// processResult;

                    db.SMSIncomings.Add(sms);

                    await db.SaveChangesAsync();

                    return Ok(sms);
                }

                return BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        //public async Task<IHttpActionResult> PostSMSIncoming(globe_sms_mo sms_mo)
        //{
        //    try
        //    {
        //        string jsonstring = JsonConvert.SerializeObject(sms_mo);

        //        SMSIncoming sms = new SMSIncoming
        //        {
        //            message = jsonstring,
        //            receive_date = DateTime.UtcNow
        //        };

        //        string processResult = processMessage(sms_mo);

        //        // do message processing
        //        sms.process_date = DateTime.UtcNow;
        //        sms.process_remarks = processResult;

        //        db.SMSIncomings.Add(sms);

        //        await db.SaveChangesAsync();

        //        return Ok(sms);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        throw;
        //    }
        //}


        private string processMessage(globe_sms_mo sms_mo)
        {
            string result = "";

            try
            {
                // TRIPSTART ROUTE VEHICLENUMBER
                string msg = sms_mo.inboundSMSMessageList.inboundSMSMessage[0].message;
                string mobilenum = Functions.CleanNumber(sms_mo.inboundSMSMessageList.inboundSMSMessage[0].senderAddress);

                if (Functions.ValidateMessage(msg))
                {
                    string cleanMessage = msg.ToUpper();
                    string[] msgParts = msg.ToUpper().Split(' ');

                    switch (msgParts[0])
                    {
                        case "START":
                            result = processTrip(mobilenum, msgParts);
                            break;
                        case "ALERT":
                            result = processAlert(mobilenum, msgParts[1], msg);
                            break;
                        default:
                            throw new Exception("Invalid message format (" + msgParts[0] + ").");
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return result;
        }

        private string processAlert(string mobileNumber, string route, string cleanMessage)
        {
            var serviceAlert = cleanMessage.Replace("ALERT " + route + " ", "");

            RTServiceAlerts rtSa = new RTServiceAlerts();
            rtSa.route_id = "ROU" + route;
            rtSa.header = "Service Advisory for " + route;
            rtSa.description = serviceAlert;
            rtSa.start_date = DateTime.UtcNow.AddHours(8);
            db.RTServiceAlerts.Add(rtSa);

            return "success";
        }

        private string processTrip(string mobileNumber, string[] msgParts)
        {
            // START MRT3 NB 0915

            DateTime phDateTime = DateTime.UtcNow.AddHours(8);
            DateTime startDateTime = new DateTime(phDateTime.Year, phDateTime.Month, phDateTime.Day, Convert.ToInt32(msgParts[3].Substring(0, 2)), Convert.ToInt32(msgParts[3].Substring(2, 2)), 0);

            string route_id = "ROU" + msgParts[1];
            string trip_id = "";
            int direction_id = (msgParts[2] == "SB" || msgParts[2] == "EB") ? 0 : 1;
            int delay = Functions.ComputeDelay(startDateTime, phDateTime);
            string start_date = startDateTime.ToString("yyyyMMdd");
            string start_time = startDateTime.ToString("HH:mm:ss");

            if (msgParts.Length > 4)
            {
                DateTime delayStart = new DateTime(phDateTime.Year, phDateTime.Month, phDateTime.Day, 
                    Convert.ToInt32(msgParts[4].Substring(0, 2)), Convert.ToInt32(msgParts[4].Substring(2, 2)), Convert.ToInt32(msgParts[4].Substring(4, 2)));
                delay = Functions.ComputeDelay(startDateTime, delayStart);
            }

            List<Trips> trips = db.Database
                .SqlQuery<Trips>("TripGetByRouteDirectionDay @route_id, @direction_id, @day_param",
                             new SqlParameter("@route_id", route_id),
                              new SqlParameter("@direction_id", direction_id),
                               new SqlParameter("@day_param", Functions.GetDayParam(startDateTime)))
                .ToList();

            if (trips.Count == 0)
                throw new Exception("Trip ID not found");

            trip_id = trips[0].trip_id;

            RTTripUpdates rtTrip = new RTTripUpdates();

            rtTrip.route_id = route_id;
            rtTrip.trip_id = trip_id;
            rtTrip.direction_id = direction_id;
            rtTrip.delay = delay;
            rtTrip.start_date = start_date;
            rtTrip.start_time = start_time;
            rtTrip.mobile_number = mobileNumber;

            db.RTTripUpdates.Add(rtTrip);

            return "success";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}