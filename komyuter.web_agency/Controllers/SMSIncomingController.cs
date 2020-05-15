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
    public class SMSIncomingController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: SMSIncoming
        public ActionResult Index()
        {
            return View(db.SMSIncomings.ToList());
        }

        // GET: SMSIncoming/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSIncoming sMSIncoming = db.SMSIncomings.Find(id);
            if (sMSIncoming == null)
            {
                return HttpNotFound();
            }
            return View(sMSIncoming);
        }

        // GET: SMSIncoming/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SMSIncoming/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sms_id,message,receive_date,process_date,process_remarks")] SMSIncoming sMSIncoming)
        {
            if (ModelState.IsValid)
            {
                db.SMSIncomings.Add(sMSIncoming);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sMSIncoming);
        }

        // GET: SMSIncoming/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSIncoming sMSIncoming = db.SMSIncomings.Find(id);
            if (sMSIncoming == null)
            {
                return HttpNotFound();
            }
            return View(sMSIncoming);
        }

        // POST: SMSIncoming/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sms_id,message,receive_date,process_date,process_remarks")] SMSIncoming sMSIncoming)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sMSIncoming).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sMSIncoming);
        }

        // GET: SMSIncoming/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSIncoming sMSIncoming = db.SMSIncomings.Find(id);
            if (sMSIncoming == null)
            {
                return HttpNotFound();
            }
            return View(sMSIncoming);
        }

        // POST: SMSIncoming/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SMSIncoming sMSIncoming = db.SMSIncomings.Find(id);
            db.SMSIncomings.Remove(sMSIncoming);
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
