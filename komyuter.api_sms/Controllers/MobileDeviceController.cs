using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using komyuter.core.DomainClasses;
using komyuter.data;

using Newtonsoft.Json;
using komyuter.core.GlobeClasses;

namespace komyuter.api_sms.Controllers
{
    public class MobileDeviceController : ApiController
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: api/MobileDevice
        public string GetMobileDevices(string access_token, string subscriber_number)
        {
            MobileDevice md = new MobileDevice
            {
                mobile_number = subscriber_number,
                access_token = access_token,
                optin_date = DateTime.UtcNow,
                optout_date = (DateTime?)null
            };

            db.MobileDevices.Add(md);
            db.SaveChanges();

            return "success";
        }

        // POST: api/MobileDevice
        [ResponseType(typeof(void))]
        public IHttpActionResult PostMobileDevice(globe_unsubscribe data)
        {
            //return BadRequest();
            //string jsonstring = JsonConvert.SerializeObject(unsubscribe);

            MobileDevice mb = db.MobileDevices
                            .Where(m => m.mobile_number == data.unsubscribed.subscriber_number &&
                                         m.access_token == data.unsubscribed.access_token)
                            .FirstOrDefault();

            if (mb == null)
            {
                return NotFound();
            }

            mb.optout_date = DateTime.UtcNow;
            db.Entry(mb).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(mb);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MobileDeviceExists(int id)
        {
            return db.MobileDevices.Count(e => e.mobile_device_id == id) > 0;
        }
    }
}