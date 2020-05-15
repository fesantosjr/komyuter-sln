using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace komyuter.web_agency.Controllers
{
    [Authorize]
    public class NaviHomeController : Controller
    {
        // GET: NaviHome
        public ActionResult Index()
        {
            return View();
        }
    }
}