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

using komyuter.web_navi.Models;

namespace komyuter.web_navi.Controllers
{
    public class RouteController : Controller
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: Route
        public ActionResult Index(string route_id, string trip_id, string stop_id, string trip_date, string trip_time, string direction_id)
        {
            NaviRoute naviRoute = new NaviRoute();

            naviRoute.navi_stops = db.Database
                .SqlQuery<NaviStop>("StopsGetByTripId @trip_id, @trip_date, @trip_time",
                            new SqlParameter("@trip_id", trip_id),
                            new SqlParameter("@trip_date", trip_date),
                            new SqlParameter("@trip_time", trip_time))
                .ToList();

            naviRoute.navi_shapes = db.Database
                .SqlQuery<Shapes>("ShapesGetByRoute @route_id",
                            new SqlParameter("@route_id", route_id))
                .ToList();

            naviRoute.TripPrevious = db.Database
                .SqlQuery<NaviTripHistory>("TripGetPreviousV2 @trip_id, @trip_date, @trip_time",
                            new SqlParameter("@trip_id", trip_id),
                            new SqlParameter("@trip_date", trip_date),
                            new SqlParameter("@trip_time", trip_time))
                .ToList();

            naviRoute.TripNext = db.Database
                .SqlQuery<NaviTripHistory>("TripGetNextV2 @trip_id, @trip_date, @trip_time",
                            new SqlParameter("@trip_id", trip_id),
                            new SqlParameter("@trip_date", trip_date),
                            new SqlParameter("@trip_time", trip_time))
                .ToList();

            naviRoute.ReverseTrip = db.Database
                .SqlQuery<NaviReverseTrip>("TripGetReverse @route_id, @trip_id, @stop_id, @travel_time",
                            new SqlParameter("@route_id", route_id),
                            new SqlParameter("@trip_id", trip_id),
                            new SqlParameter("@stop_id", stop_id),
                            new SqlParameter("@travel_time", trip_time))
                .ToList();

            naviRoute.SelectedRouteId = route_id;
            naviRoute.SelectedTripId = trip_id;
            naviRoute.SelectedStopId = stop_id;
            naviRoute.SelectedDirectionId = direction_id;
            naviRoute.SelectedTripDate = trip_date;
            naviRoute.SelectedTripTime = trip_time;

            return View(naviRoute);
        }

        // GET: Route/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = db.Routes.Find(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // GET: Route/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Route/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "route_id,agency_id,route_short_name,route_long_name,route_type,route_text_color,route_color,route_url,route_desc,default_zoom,center_lat,center_lon")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(routes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(routes);
        }

        // GET: Route/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = db.Routes.Find(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // POST: Route/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "route_id,agency_id,route_short_name,route_long_name,route_type,route_text_color,route_color,route_url,route_desc,default_zoom,center_lat,center_lon")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(routes);
        }

        // GET: Route/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = db.Routes.Find(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // POST: Route/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Routes routes = db.Routes.Find(id);
            db.Routes.Remove(routes);
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
