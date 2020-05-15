using komyuter.core.Common;
using komyuter.core.DomainClasses;
using komyuter.data;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitRealtime;

namespace komyuter.webjob_vehicleposition
{
    class Program
    {
        private static KomyuterContext db = new KomyuterContext();

        static void Main(string[] args)
        {
            int tsOffset = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TimeStampOffset"));

            long timestamp_from = (long)Functions.ToEpoch(DateTime.UtcNow.AddMinutes(tsOffset));
            long timestamp_to = (long)Functions.ToEpoch(DateTime.UtcNow);

            List<RTVehiclePositions> vehiclePositions = db.Database
                .SqlQuery<RTVehiclePositions>("RTVehiclePositionsGetActive @timestamp_from, @timestamp_to"
                    , new SqlParameter("@timestamp_from", timestamp_from)
                    , new SqlParameter("@timestamp_to", timestamp_to))
                .ToList();

            FeedMessage feed = new FeedMessage();

            if (vehiclePositions.Count > 0)
            {
                foreach (RTVehiclePositions vehiclePosition in vehiclePositions)
                {
                    TripDescriptor td = new TripDescriptor();
                    td.TripId = vehiclePosition.trip_id;
                    td.RouteId = vehiclePosition.route_id;
                    td.DirectionId = (uint)vehiclePosition.direction_id;
                    td.StartDate = vehiclePosition.start_date;
                    td.StartTime = vehiclePosition.start_time;

                    Position pos = new Position();
                    pos.Latitude = (float)vehiclePosition.latitude;
                    pos.Longitude = (float)vehiclePosition.longitude;

                    VehicleDescriptor v = new VehicleDescriptor();
                    v.Id = vehiclePosition.vehicle_id;
                    v.Label = vehiclePosition.vehicle_label;
                    v.LicensePlate = vehiclePosition.vehicle_license_plate;

                    VehiclePosition vp = new VehiclePosition();
                    vp.Position = pos;
                    vp.Trip = td;
                    vp.Vehicle = v;
                    vp.CurrentStopSequence = (uint)vehiclePosition.current_stop_sequence;
                    vp.StopId = vehiclePosition.stop_id;
                    vp.Timestamp = (ulong)vehiclePosition.timestamp;

                    FeedEntity entity = new FeedEntity();
                    entity.Id = vehiclePosition.id.ToString();
                    entity.Vehicle = vp;

                    feed.Entities.Add(entity);
                }

                byte[] objSerialized = Functions.ProtoSerialize(feed);

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                string filename = "vehicleposition.pb";

                // Create a CloudFileClient object for credentialed access to Azure Files.
                CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

                // Get a reference to the file share we created previously.
                CloudFileShare share = fileClient.GetShareReference("gtfsrt");
                share.CreateIfNotExists();

                var rootDir = share.GetRootDirectoryReference();
                using (var stream = new MemoryStream(objSerialized, writable: false))
                {
                    rootDir.GetFileReference(filename).UploadFromStream(stream);
                }
            }
        }
    }
}
