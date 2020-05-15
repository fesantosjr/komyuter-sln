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
    public class RTVehiclePositionController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: RTVehiclePosition
        public ActionResult Index()
        {
            return View(db.RTVehiclePositions.ToList());
        }

        // GET: RTVehiclePosition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTVehiclePositions rTVehiclePositions = db.RTVehiclePositions.Find(id);
            if (rTVehiclePositions == null)
            {
                return HttpNotFound();
            }
            return View(rTVehiclePositions);
        }

        // GET: RTVehiclePosition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RTVehiclePosition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,route_id,trip_id,direction_id,start_date,start_time,latitude,longitude,timestamp")] RTVehiclePositions rTVehiclePositions)
        {
            if (ModelState.IsValid)
            {
                db.RTVehiclePositions.Add(rTVehiclePositions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rTVehiclePositions);
        }

        // GET: RTVehiclePosition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTVehiclePositions rTVehiclePositions = db.RTVehiclePositions.Find(id);
            if (rTVehiclePositions == null)
            {
                return HttpNotFound();
            }
            return View(rTVehiclePositions);
        }

        // POST: RTVehiclePosition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,route_id,trip_id,direction_id,start_date,start_time,latitude,longitude,timestamp")] RTVehiclePositions rTVehiclePositions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rTVehiclePositions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rTVehiclePositions);
        }

        // GET: RTVehiclePosition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTVehiclePositions rTVehiclePositions = db.RTVehiclePositions.Find(id);
            if (rTVehiclePositions == null)
            {
                return HttpNotFound();
            }
            return View(rTVehiclePositions);
        }

        // POST: RTVehiclePosition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RTVehiclePositions rTVehiclePositions = db.RTVehiclePositions.Find(id);
            db.RTVehiclePositions.Remove(rTVehiclePositions);
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
