using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using komyuter.core.DomainClasses;
using komyuter.data;

namespace komyuter.web_agency.Controllers
{
    [Authorize]
    public class RouteController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: Route
        public ActionResult Index()
        {
            return View(db.Routes.ToList());
        }

        // GET: Route/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = db.Routes.Find(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // GET: Route/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Route/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "route_id,agency_id,route_short_name,route_long_name,route_type,route_text_color,route_color,route_url,route_desc,default_zoom,center_lat,center_lon")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                var checkRecord = db.Routes.Find(routes.route_id);

                if (checkRecord != null)
                {
                    ModelState.AddModelError(string.Empty, "This record already exists.");
                    return View(routes);
                }
                else
                {
                    db.Routes.Add(routes);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(routes);
        }

        // GET: Route/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = db.Routes.Find(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // POST: Route/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "route_id,agency_id,route_short_name,route_long_name,route_type,route_text_color,route_color,route_url,route_desc,default_zoom,center_lat,center_lon")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(routes);
        }

        // GET: Route/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = db.Routes.Find(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // POST: Route/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Routes routes = db.Routes.Find(id);
            db.Routes.Remove(routes);
            db.SaveChanges();
            return RedirectToAction("Index");
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
