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
    public class FrequencyController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: Frequency
        public ActionResult Index()
        {
            return View(db.Frequencies.ToList());
        }

        // GET: Frequency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Frequency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,trip_id,start_time,end_time,headway_secs,exact_times")] Frequencies frequencies)
        {
            if (ModelState.IsValid)
            {
                db.Frequencies.Add(frequencies);
                db.SaveChanges();
                return RedirectToAction("Index");
                //var checkRecord = db.Frequencies.Find(frequencies.id);

                //if (checkRecord != null)
                //{
                //    ModelState.AddModelError(string.Empty, "This record already exists.");
                //    return View(frequencies);
                //}
                //else
                //{
                //    db.Frequencies.Add(frequencies);
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}
            }

            return View(frequencies);
        }

        // GET: Frequency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frequencies frequencies = db.Frequencies.Find(id);
            if (frequencies == null)
            {
                return HttpNotFound();
            }
            return View(frequencies);
        }

        // POST: Frequency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,trip_id,start_time,end_time,headway_secs,exact_times")] Frequencies frequencies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frequencies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(frequencies);
        }

        // GET: Frequency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frequencies frequencies = db.Frequencies.Find(id);
            if (frequencies == null)
            {
                return HttpNotFound();
            }
            return View(frequencies);
        }

        // POST: Frequency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Frequencies frequencies = db.Frequencies.Find(id);
            db.Frequencies.Remove(frequencies);
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
