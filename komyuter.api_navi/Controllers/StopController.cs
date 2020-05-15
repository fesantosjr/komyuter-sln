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
    public class StopController : ApiController
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: api/Stop/5
        [ResponseType(typeof(NaviStop))]
        public IHttpActionResult GetStops(string route_id)
        {
            var routeIdParam = new SqlParameter("@route_id", route_id);
            var stops = db.Database
                .SqlQuery<NaviStop>("StopsGetByRoute @route_id", routeIdParam)
                .ToList();

            if (stops == null)
            {
                return NotFound();
            }
            return Ok(stops);

            //Stops stops = db.Stops.Find(id);
            //if (stops == null)
            //{
            //    return NotFound();
            //}

            //return Ok(stops);
        }

        //// GET: api/Stop
        //public IQueryable<Stops> GetStops()
        //{
        //    return db.Stops;
        //}

        //// GET: api/Stop/5
        //[ResponseType(typeof(Stops))]
        //public IHttpActionResult GetStops(string id)
        //{
        //    Stops stops = db.Stops.Find(id);
        //    if (stops == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(stops);
        //}

        //// PUT: api/Stop/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutStops(string id, Stops stops)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != stops.stop_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(stops).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StopsExists(id))
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

        //// POST: api/Stop
        //[ResponseType(typeof(Stops))]
        //public IHttpActionResult PostStops(Stops stops)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Stops.Add(stops);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (StopsExists(stops.stop_id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = stops.stop_id }, stops);
        //}

        //// DELETE: api/Stop/5
        //[ResponseType(typeof(Stops))]
        //public IHttpActionResult DeleteStops(string id)
        //{
        //    Stops stops = db.Stops.Find(id);
        //    if (stops == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Stops.Remove(stops);
        //    db.SaveChanges();

        //    return Ok(stops);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StopsExists(string id)
        {
            return db.Stops.Count(e => e.stop_id == id) > 0;
        }
    }
}