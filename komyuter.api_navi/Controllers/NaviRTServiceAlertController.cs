using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using komyuter.core.DomainClasses;
using komyuter.data;

namespace komyuter.api_navi.Controllers
{
    public class NaviRTServiceAlertController : ApiController
    {
        private KomyuterContext db = new KomyuterContext();


        // GET: api/NaviRTServiceAlert/5
        [ResponseType(typeof(NaviRTServiceAlerts))]
        public IHttpActionResult GetNaviRTServiceAlerts(string route_id)
        {
            var serviceAlerts = db.Database
                .SqlQuery<NaviRTServiceAlerts>("NaviRTServiceAlertsGetByRouteID @route_id",
                    new SqlParameter("@route_id", route_id))
                .ToList();

            if (serviceAlerts == null)
            {
                return NotFound();
            }
            return Ok(serviceAlerts);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NaviRTServiceAlertsExists(int id)
        {
            return db.NaviRTServiceAlerts.Count(e => e.id == id) > 0;
        }
    }
}