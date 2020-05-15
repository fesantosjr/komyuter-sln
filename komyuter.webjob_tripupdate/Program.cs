using komyuter.core.Common;
using komyuter.core.DomainClasses;
using komyuter.data;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitRealtime;

namespace komyuter.webjob_tripupdate
{
    class Program
    {
        private static KomyuterContext db = new KomyuterContext();

        static void Main(string[] args)
        {
            DateTime phDate = DateTime.UtcNow.AddHours(8);

            List<RTTripUpdates> tripUpdates = db.Database
                .SqlQuery<RTTripUpdates>("RTTripUpdatesGetActive @travel_date"
                    , new SqlParameter("@travel_date", phDate.ToString("yyyyMMdd")))
                .ToList();

            FeedMessage feed = new FeedMessage();

            if (tripUpdates.Count > 0)
            {
                foreach (RTTripUpdates tripUpdate in tripUpdates)
                {
                    TripDescriptor td = new TripDescriptor();
                    td.TripId = tripUpdate.trip_id;
                    td.RouteId = tripUpdate.route_id;
                    td.DirectionId = (uint)tripUpdate.direction_id;
                    td.StartDate = tripUpdate.start_date;
                    td.StartTime = tripUpdate.start_time;

                    TripUpdate tp = new TripUpdate();
                    tp.Delay = tripUpdate.delay;
                    tp.Trip = td;

                    FeedEntity entity = new FeedEntity();
                    entity.TripUpdate = tp;
                    entity.Id = tripUpdate.id.ToString();

                    feed.Entities.Add(entity);
                }

                byte[] objSerialized = Functions.ProtoSerialize(feed);

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                string filename = "tripupdate.pb";


                // Create a CloudFileClient object for credentialed access to Azure Files.
                CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

                // Get a reference to the file share we created previously.
                CloudFileShare share = fileClient.GetShareReference("gtfsrt");
                share.CreateIfNotExists();

                var rootDir = share.GetRootDirectoryReference();
                using (var stream = new MemoryStream(objSerialized, writable: false))
                {
                    rootDir.GetFileReference(filename).UploadFromStream(stream);//.UploadFromByteArray(feed,);
                }
            }


           // Functions.CreatePBFile(storageAccount, filename, objSerialized);
        }
    }
}
