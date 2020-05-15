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

namespace komyuter.web_navi.Controllers
{
    public class StopController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: Stop
        public ActionResult Index()
        {
            return View();// db.Stops.ToList());
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
