using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using komyuter.core.DomainClasses;
using komyuter.data;

using komyuter.api_navi.Models;
using komyuter.core.Common;

namespace komyuter.api_navi.Controllers
{
    public class FrequencyController : ApiController
    {
        private KomyuterContext db = new KomyuterContext();

        // GET: api/Frequency/5
        [ResponseType(typeof(NaviFrequency))]
        public IHttpActionResult GetFrequencies(string route_id, string stop_id, string travel_datetime, int schedule_count)
        {
            DateTime phDateTime = DateTime.Parse(travel_datetime);
            string travelTime = phDateTime.ToString("HH:mm:ss");
            string day = Functions.GetDayParam(phDateTime);

            var routeIdParam = new SqlParameter("@route_id", route_id);
            var stopIdParam = new SqlParameter("@stop_id", stop_id);
            var travelDateParam = new SqlParameter("@travel_date", phDateTime.ToString("yyyyMMdd"));
            var travelTimeParam = new SqlParameter("@travel_time", travelTime);
            var dayParam = new SqlParameter("@day_param", day);
            var countParam = new SqlParameter("@list_count", schedule_count);

            var frequencies = db.Database
                .SqlQuery<NaviFrequency>("StopFrequenciesGetByRouteTravelTime @route_id, @stop_id, @travel_date, @travel_time, @day_param, @list_count",
                            routeIdParam, stopIdParam, travelDateParam, travelTimeParam, dayParam, countParam)
                .ToList();

            TimeSpan phTime = TimeSpan.Parse(travelTime);

            List<NaviFrequency> newFrequencies = new List<NaviFrequency>();

            int idCounter = 0;

            if (frequencies.Count > 0)
            {
                foreach (NaviFrequency freq in frequencies)
                {
                    if (freq.trip_headsign == freq.stop_name)
                        continue;

                    TimeSpan tsCounterStart = freq.start_time;
                    TimeSpan tsCounterArrival = freq.start_time + freq.arrival_time;

                    idCounter++;
                    newFrequencies.Add(new NaviFrequency(idCounter,
                                                        freq.trip_id,
                                                        tsCounterStart,
                                                        freq.arrival_time,
                                                        freq.trip_headsign,
                                                        freq.stop_name,
                                                        freq.direction_id,
                                                        freq.rt_delay));
                }
            }
            else
            {
                newFrequencies = db.Database
                  .SqlQuery<NaviFrequency>("StopFrequenciesGetOffHours @route_id, @stop_id",
                              new SqlParameter("@route_id", route_id), new SqlParameter("@stop_id", stop_id))
                  .ToList();
            }

            if (newFrequencies == null)
            {
                return NotFound();
            }

            return Ok(newFrequencies.OrderBy(x => x.arrival_time).ToList());
        }
    }
}