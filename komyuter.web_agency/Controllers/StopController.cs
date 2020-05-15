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
    public class StopController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: Stop
        public ActionResult Index()
        {
            return View(db.Stops.ToList());
        }

        // GET: Stop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stop_id,stop_code,stop_name,stop_desc,stop_lat,stop_lon,zone_id,stop_url,location_type,parent_station,wheelchair_boarding")] Stops stops)
        {
            if (ModelState.IsValid)
            {
                var checkRecord = db.Stops.Find(stops.stop_id);

                if (checkRecord != null)
                {
                    ModelState.AddModelError(string.Empty, "This record already exists.");
                    return View(stops);
                }
                else
                {
                    db.Stops.Add(stops);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(stops);
        }

        // GET: Stop/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stops stops = db.Stops.Find(id);
            if (stops == null)
            {
                return HttpNotFound();
            }
            return View(stops);
        }

        // POST: Stop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stop_id,stop_code,stop_name,stop_desc,stop_lat,stop_lon,zone_id,stop_url,location_type,parent_station,wheelchair_boarding")] Stops stops)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stops).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stops);
        }

        // GET: Stop/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stops stops = db.Stops.Find(id);
            if (stops == null)
            {
                return HttpNotFound();
            }
            return View(stops);
        }

        // POST: Stop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Stops stops = db.Stops.Find(id);
            db.Stops.Remove(stops);
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
