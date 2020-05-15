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
    public class MobileDeviceController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: MobileDevice
        public ActionResult Index()
        {
            return View(db.MobileDevices.ToList());
        }

        // GET: MobileDevice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileDevice mobileDevice = db.MobileDevices.Find(id);
            if (mobileDevice == null)
            {
                return HttpNotFound();
            }
            return View(mobileDevice);
        }

        // GET: MobileDevice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MobileDevice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mobile_device_id,mobile_number,access_token,optin_date,optout_date")] MobileDevice mobileDevice)
        {
            if (ModelState.IsValid)
            {
                db.MobileDevices.Add(mobileDevice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mobileDevice);
        }

        // GET: MobileDevice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileDevice mobileDevice = db.MobileDevices.Find(id);
            if (mobileDevice == null)
            {
                return HttpNotFound();
            }
            return View(mobileDevice);
        }

        // POST: MobileDevice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mobile_device_id,mobile_number,access_token,optin_date,optout_date")] MobileDevice mobileDevice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mobileDevice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mobileDevice);
        }

        // GET: MobileDevice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileDevice mobileDevice = db.MobileDevices.Find(id);
            if (mobileDevice == null)
            {
                return HttpNotFound();
            }
            return View(mobileDevice);
        }

        // POST: MobileDevice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MobileDevice mobileDevice = db.MobileDevices.Find(id);
            db.MobileDevices.Remove(mobileDevice);
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
