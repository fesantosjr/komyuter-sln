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
    public class StopTimeController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: StopTime
        public ActionResult Index()
        {
            return View(db.StopTimes.ToList());
        }

        // GET: StopTime/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StopTimes stopTimes = db.StopTimes.Find(id);
            if (stopTimes == null)
            {
                return HttpNotFound();
            }
            return View(stopTimes);
        }

        // GET: StopTime/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StopTime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,trip_id,arrival_time,departure_time,stop_id,stop_sequence")] StopTimes stopTimes)
        {
            if (ModelState.IsValid)
            {
                stopTimes.pickup_type = 0;
                stopTimes.drop_off_type = 0;

                db.StopTimes.Add(stopTimes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stopTimes);
        }

        // GET: StopTime/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StopTimes stopTimes = db.StopTimes.Find(id);
            if (stopTimes == null)
            {
                return HttpNotFound();
            }
            return View(stopTimes);
        }

        // POST: StopTime/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,trip_id,arrival_time,departure_time,stop_id,stop_sequence")] StopTimes stopTimes)
        {
            if (ModelState.IsValid)
            {
                //stopTimes.pickup_type = 0;
                //stopTimes.drop_off_type = 0;

                db.Entry(stopTimes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stopTimes);
        }

        // GET: StopTime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StopTimes stopTimes = db.StopTimes.Find(id);
            if (stopTimes == null)
            {
                return HttpNotFound();
            }
            return View(stopTimes);
        }

        // POST: StopTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StopTimes stopTimes = db.StopTimes.Find(id);
            db.StopTimes.Remove(stopTimes);
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
