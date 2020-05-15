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
    public class RTTripUpdateController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: RTTripUpdate
        public ActionResult Index()
        {
            return View(db.RTTripUpdates.ToList());
        }

        // GET: RTTripUpdate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTTripUpdates rTTripUpdates = db.RTTripUpdates.Find(id);
            if (rTTripUpdates == null)
            {
                return HttpNotFound();
            }
            return View(rTTripUpdates);
        }

        // GET: RTTripUpdate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RTTripUpdate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,trip_id,route_id,direction_id,delay,start_date,start_time,mobile_number")] RTTripUpdates rTTripUpdates)
        {
            if (ModelState.IsValid)
            {
                db.RTTripUpdates.Add(rTTripUpdates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rTTripUpdates);
        }

        // GET: RTTripUpdate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTTripUpdates rTTripUpdates = db.RTTripUpdates.Find(id);
            if (rTTripUpdates == null)
            {
                return HttpNotFound();
            }
            return View(rTTripUpdates);
        }

        // POST: RTTripUpdate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,trip_id,route_id,direction_id,delay,start_date,start_time,mobile_number")] RTTripUpdates rTTripUpdates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rTTripUpdates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rTTripUpdates);
        }

        // GET: RTTripUpdate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTTripUpdates rTTripUpdates = db.RTTripUpdates.Find(id);
            if (rTTripUpdates == null)
            {
                return HttpNotFound();
            }
            return View(rTTripUpdates);
        }

        // POST: RTTripUpdate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RTTripUpdates rTTripUpdates = db.RTTripUpdates.Find(id);
            db.RTTripUpdates.Remove(rTTripUpdates);
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
