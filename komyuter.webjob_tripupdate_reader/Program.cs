using komyuter.core.DomainClasses;
using komyuter.data;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TransitRealtime;

namespace komyuter.webjob_tripupdate_reader
{
    class Program
    {
        private static KomyuterContext db = new KomyuterContext();

        static void Main(string[] args)
        {
            string storageAccountKey = ConfigurationManager.AppSettings.Get("StorageAccountKey");
            string path = ConfigurationManager.AppSettings.Get("TripUpdatePBPath");

            var req = WebRequest.Create(path + storageAccountKey);
            FeedMessage feed = Serializer.Deserialize<FeedMessage>(req.GetResponse().GetResponseStream());

            if (feed.Entities.Count > 0)
            {
                bool saveRecord = false;
                foreach (FeedEntity entity in feed.Entities)
                {
                    int rt_id = Convert.ToInt32(entity.Id);

                    // check if rt_id is already in the db
                    List<NaviRTTripUpdates> naviTUs = db.Database
                        .SqlQuery<NaviRTTripUpdates>("NaviRTTripUpdatesGetByRTID @rt_id",
                            new SqlParameter("@rt_id", rt_id))
                        .ToList();

                    if (naviTUs.Count == 0)
                    {
                        NaviRTTripUpdates naviTU = new NaviRTTripUpdates();
                        naviTU.rt_id = rt_id;
                        naviTU.delay = entity.TripUpdate.Delay;
                        naviTU.trip_id = entity.TripUpdate.Trip.TripId;
                        naviTU.route_id = entity.TripUpdate.Trip.RouteId;
                        naviTU.direction_id = (int)entity.TripUpdate.Trip.DirectionId;
                        naviTU.start_date = entity.TripUpdate.Trip.StartDate;
                        naviTU.start_time = entity.TripUpdate.Trip.StartTime;

                        db.NaviRTTripUpdates.Add(naviTU);
                        saveRecord = true;
                    }
                }

                if (saveRecord)
                    db.SaveChanges();

            }
        }
    }
}
