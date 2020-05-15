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
    public class NaviRTVehiclePositionController : ApiController
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: api/NaviRTVehiclePosition/5
        [ResponseType(typeof(NaviRTVehiclePositions))]
        public IHttpActionResult GetNaviRTVehiclePositions(string route_id, string trip_id, string direction_id, string trip_date, string trip_time, long timestamp_from, long timestamp_to)
        {
            var vehiclePositions = db.Database
                .SqlQuery<NaviRTVehiclePositions>("NaviRTVehiclePositionsGet @route_id, @trip_id, @direction_id, @trip_date, @trip_time, @timestamp_from, @timestamp_to",
                    new SqlParameter("@route_id", route_id),
                    new SqlParameter("@trip_id", trip_id),
                    new SqlParameter("@direction_id", direction_id),
                    new SqlParameter("@trip_date", trip_date),
                    new SqlParameter("@trip_time", trip_time),
                    new SqlParameter("@timestamp_from", timestamp_from),
                    new SqlParameter("@timestamp_to", timestamp_to))
                .ToList();

            if (vehiclePositions == null)
            {
                return NotFound();
            }
            return Ok(vehiclePositions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NaviRTVehiclePositionsExists(int id)
        {
            return db.NaviRTVehiclePositions.Count(e => e.id == id) > 0;
        }
    }
}