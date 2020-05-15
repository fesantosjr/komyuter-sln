using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using komyuter.core.DomainClasses;
using komyuter.data;

namespace komyuter.web_agency.Controllers
{
    [Authorize]
    public class RTServiceAlertController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: RTServiceAlert
        public ActionResult Index()
        {
            var data = db.Database
                .SqlQuery<RTServiceAlerts>("RTServiceAlertsGetFormatted")
                .ToList();

            return View(data);
        }

        // GET: RTServiceAlert/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTServiceAlerts rTServiceAlerts = db.RTServiceAlerts.Find(id);
            if (rTServiceAlerts == null)
            {
                return HttpNotFound();
            }
            return View(rTServiceAlerts);
        }

        // GET: RTServiceAlert/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RTServiceAlert/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,route_id,stop_id,header,description,start_date,end_date")] RTServiceAlerts rTServiceAlerts)
        {
            if (ModelState.IsValid)
            {
                db.RTServiceAlerts.Add(rTServiceAlerts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rTServiceAlerts);
        }

        // GET: RTServiceAlert/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<RTServiceAlerts> data = db.Database
                .SqlQuery<RTServiceAlerts>("RTServiceAlertsGetFormattedByID @id",
                            new SqlParameter("@id", id))
                .ToList();

            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            RTServiceAlerts rTServiceAlerts = data[0];
            return View(rTServiceAlerts);
        }

        // POST: RTServiceAlert/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,header,description,start_date,end_date")] RTServiceAlerts rTServiceAlerts)
        {
            if (ModelState.IsValid)
            {
                RTServiceAlerts recOrig = db.RTServiceAlerts.Find(rTServiceAlerts.id);
                recOrig.header = rTServiceAlerts.header;
                recOrig.description = rTServiceAlerts.description;
                recOrig.start_date = rTServiceAlerts.start_date;
                recOrig.end_date = rTServiceAlerts.end_date;

                db.Entry(recOrig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rTServiceAlerts);
        }

        // GET: RTServiceAlert/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<RTServiceAlerts> data = db.Database
                .SqlQuery<RTServiceAlerts>("RTServiceAlertsGetFormattedByID @id",
                            new SqlParameter("@id", id))
                .ToList();

            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            RTServiceAlerts rTServiceAlerts = data[0];
            return View(rTServiceAlerts);
        }

        // POST: RTServiceAlert/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RTServiceAlerts rTServiceAlerts = db.RTServiceAlerts.Find(id);
            db.RTServiceAlerts.Remove(rTServiceAlerts);
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
