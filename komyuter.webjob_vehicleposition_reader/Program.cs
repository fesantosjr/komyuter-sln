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

namespace komyuter.webjob_vehicleposition_reader
{
    class Program
    {
        private static KomyuterContext db = new KomyuterContext();

        static void Main(string[] args)
        {
            string storageAccountKey = ConfigurationManager.AppSettings.Get("StorageAccountKey");
            string path = ConfigurationManager.AppSettings.Get("VehiclePositionPBPath");

            var req = WebRequest.Create(path + storageAccountKey);
            FeedMessage feed = Serializer.Deserialize<FeedMessage>(req.GetResponse().GetResponseStream());

            if (feed.Entities.Count > 0)
            {
                bool saveRecord = false;
                foreach (FeedEntity entity in feed.Entities)
                {
                    int rt_id = Convert.ToInt32(entity.Id);

                    // check if rt_id is already in the db
                    List<NaviRTVehiclePositions> naviVPs = db.Database
                        .SqlQuery<NaviRTVehiclePositions>("NaviRTVehiclePositionsGetByRTID @rt_id",
                            new SqlParameter("@rt_id", rt_id))
                        .ToList();

                    if (naviVPs.Count == 0)
                    {
                        NaviRTVehiclePositions naviVP = new NaviRTVehiclePositions();
                        naviVP.rt_id = rt_id;

                        naviVP.route_id = entity.Vehicle.Trip.RouteId;
                        naviVP.trip_id = entity.Vehicle.Trip.TripId;
                        naviVP.direction_id = (int)entity.Vehicle.Trip.DirectionId;
                        naviVP.start_date = entity.Vehicle.Trip.StartDate;
                        naviVP.start_time = entity.Vehicle.Trip.StartTime;

                        naviVP.latitude = entity.Vehicle.Position.Latitude;
                        naviVP.longitude = entity.Vehicle.Position.Longitude;
                        naviVP.timestamp = (long)entity.Vehicle.Timestamp;

                        naviVP.stop_id = entity.Vehicle.StopId;
                        naviVP.current_stop_sequence = (int)entity.Vehicle.CurrentStopSequence;

                        naviVP.vehicle_id = entity.Vehicle.Vehicle.Id;
                        naviVP.vehicle_label = entity.Vehicle.Vehicle.Label;
                        naviVP.vehicle_license_plate = entity.Vehicle.Vehicle.LicensePlate;

                        db.NaviRTVehiclePositions.Add(naviVP);
                        saveRecord = true;
                    }
                }

                if (saveRecord)
                    db.SaveChanges();

            }
        }
    }
}
