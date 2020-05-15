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

namespace komyuter.webjob_alert_reader
{
    class Program
    {
        private static KomyuterContext db = new KomyuterContext();

        static void Main(string[] args)
        {
            string storageAccountKey = ConfigurationManager.AppSettings.Get("StorageAccountKey");
            string path = ConfigurationManager.AppSettings.Get("AlertPBPath");
            
            var req = WebRequest.Create(path + storageAccountKey);
            FeedMessage feed = Serializer.Deserialize<FeedMessage>(req.GetResponse().GetResponseStream());

            if (feed.Entities.Count > 0)
            {
                bool saveRecord = false;
                foreach (FeedEntity entity in feed.Entities)
                {
                    int rt_id = Convert.ToInt32(entity.Id);

                    // check if rt_id is already in the db
                    List<NaviRTServiceAlerts> naviAlerts = db.Database
                        .SqlQuery<NaviRTServiceAlerts>("NaviRTServiceAlertsGetByRTID @rt_id",
                            new SqlParameter("@rt_id", rt_id))
                        .ToList();

                    if (naviAlerts.Count == 0)
                    {
                        NaviRTServiceAlerts naviAlert = new NaviRTServiceAlerts();
                        naviAlert.rt_id = rt_id;
                        naviAlert.header = entity.Alert.HeaderText.Translations[0].Text;
                        naviAlert.description = entity.Alert.DescriptionText.Translations[0].Text;
                        naviAlert.route_id = entity.Alert.InformedEntities[0].RouteId;
                        naviAlert.stop_id = entity.Alert.InformedEntities[0].StopId;
                        naviAlert.start_date = FromUnixTime((long)entity.Alert.ActivePeriods[0].Start);
                        naviAlert.end_date = FromUnixTime((long)entity.Alert.ActivePeriods[0].End);

                        db.NaviRTServiceAlerts.Add(naviAlert);
                        saveRecord = true;
                    }
                }

                if (saveRecord)
                    db.SaveChanges();

            }

            //int c = feed.Entities.Count;
            ////Assert.AreEqual(c, 1);
            //var entity = feed.Entities[0];
            ////Assert.AreEqual(entity.Id, "1");
            ////Assert.IsTrue(entity.Vehicle != null);
            //int i = 0;

            //var x = ProtoSerialize(feed);
        }

        public static DateTime FromUnixTime(long unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }

        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
