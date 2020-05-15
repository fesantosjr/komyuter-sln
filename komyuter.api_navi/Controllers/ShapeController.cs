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

namespace komyuter.api_navi.Controllers
{
    public class ShapeController : ApiController
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: api/Shape/5
        [ResponseType(typeof(Shapes))]
        public IHttpActionResult GetShapes(string route_id)
        {
            var routeIdParam = new SqlParameter("@route_id", route_id);
            var shapes = db.Database
                .SqlQuery<Shapes>("ShapesGetByRoute @route_id", routeIdParam)
                .ToList();

            if (shapes == null)
            {
                return NotFound();
            }
            return Ok(shapes);
        }

        //// GET: api/Shape
        //public IQueryable<Shapes> GetShapes()
        //{
        //    return db.Shapes;
        //}

        //// GET: api/Shape/5
        //[ResponseType(typeof(Shapes))]
        //public IHttpActionResult GetShapes(int id)
        //{
        //    Shapes shapes = db.Shapes.Find(id);
        //    if (shapes == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(shapes);
        //}

        //// PUT: api/Shape/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutShapes(int id, Shapes shapes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != shapes.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(shapes).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ShapesExists(id))
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

        //// POST: api/Shape
        //[ResponseType(typeof(Shapes))]
        //public IHttpActionResult PostShapes(Shapes shapes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Shapes.Add(shapes);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = shapes.id }, shapes);
        //}

        //// DELETE: api/Shape/5
        //[ResponseType(typeof(Shapes))]
        //public IHttpActionResult DeleteShapes(int id)
        //{
        //    Shapes shapes = db.Shapes.Find(id);
        //    if (shapes == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Shapes.Remove(shapes);
        //    db.SaveChanges();

        //    return Ok(shapes);
        //}

        //protected override void dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.dispose();
        //    }
        //    base.dispose(disposing);
        //}

        //private bool ShapesExists(int id)
        //{
        //    return db.Shapes.Count(e => e.id == id) > 0;
        //}
    }
}