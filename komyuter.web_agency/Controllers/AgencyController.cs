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
    public class AgencyController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: Agency
        public ActionResult Index()
        {
            //using (var context = new DatabaseContext())
            //{
            //    var clientIdParameter = new SqlParameter("@ClientId", 4);

            //    var result = context.Database
            //        .SqlQuery<ResultForCampaign>("GetResultsForCampaign @ClientId", clientIdParameter)
            //        .ToList();
            //}

            return View(db.Agency.ToList());
        }

        // GET: Agency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "agency_id,agency_name,agency_url,agency_timezone,agency_lang,agency_phone")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                var checkRecord = db.Agency.Find(agency.agency_id);

                if (checkRecord != null)
                {
                    ModelState.AddModelError(string.Empty, "This record already exists.");
                    return View(agency);
                }
                else
                {
                    db.Agency.Add(agency);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(agency);
        }

        // GET: Agency/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "agency_id,agency_name,agency_url,agency_timezone,agency_lang,agency_phone")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agency);
        }

        // GET: Agency/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Agency agency = db.Agency.Find(id);
            db.Agency.Remove(agency);
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
