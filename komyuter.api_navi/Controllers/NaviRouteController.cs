using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using komyuter.core.DomainClasses;
using komyuter.data;

using komyuter.api_navi.Models;

namespace komyuter.api_navi.Controllers
{
    public class NaviRouteController : ApiController
    {
        private KomyuterContext db = new KomyuterContext();

        //// GET: api/NaviRoute
        //public IQueryable<Routes> GetRoutes()
        //{
        //    return db.Routes;
        //}

        // GET: api/NaviRoute/5
        [ResponseType(typeof(NaviRoute))]
        public IHttpActionResult GetRoutes(string route_id, string trip_id, string start_time)
        {
            NaviRoute naviRoute = new NaviRoute();

            naviRoute.navi_stops = db.Database
                .SqlQuery<NaviStop>("StopsGetByRoute @route_id",
                            new SqlParameter("@route_id", route_id))
                .ToList();

            naviRoute.navi_shapes = db.Database
                .SqlQuery<Shapes>("ShapesGetByRoute @route_id",
                            new SqlParameter("@route_id", route_id))
                .ToList();

            naviRoute.TripPrevious = db.Database
                .SqlQuery<NaviTripHistory>("TripGetPreviousV2 @trip_id, @trip_time",
                            new SqlParameter("@trip_id", trip_id), new SqlParameter("@trip_time", start_time))
                .ToList();

            naviRoute.TripNext = db.Database
                .SqlQuery<NaviTripHistory>("TripGetNextV2 @trip_id, @trip_time",
                            new SqlParameter("@trip_id", trip_id), new SqlParameter("@trip_time", start_time))
                .ToList();

            List<NaviRoute> naviRoutes = new List<NaviRoute>();
            naviRoutes.Add(naviRoute);

            if (naviRoutes == null)
            {
                return NotFound();
            }

            return Ok(naviRoutes);
        }

        //// PUT: api/NaviRoute/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutRoutes(string id, Routes routes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != routes.route_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(routes).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RoutesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/NaviRoute
        //[ResponseType(typeof(Routes))]
        //public IHttpActionResult PostRoutes(Routes routes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Routes.Add(routes);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (RoutesExists(routes.route_id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = routes.route_id }, routes);
        //}

        //// DELETE: api/NaviRoute/5
        //[ResponseType(typeof(Routes))]
        //public IHttpActionResult DeleteRoutes(string id)
        //{
        //    Routes routes = db.Routes.Find(id);
        //    if (routes == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Routes.Remove(routes);
        //    db.SaveChanges();

        //    return Ok(routes);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool RoutesExists(string id)
        //{
        //    return db.Routes.Count(e => e.route_id == id) > 0;
        //}
    }
}