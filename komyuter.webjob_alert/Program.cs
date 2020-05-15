using komyuter.core.Common;
using komyuter.core.DomainClasses;
using komyuter.data;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitRealtime;
using static TransitRealtime.TranslatedString;

namespace komyuter.webjob_alert
{
    class Program
    {
        private static KomyuterContext db = new KomyuterContext();

        static void Main(string[] args)
        {

            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = System.Data.SqlDbType.DateTime;
            parameter.ParameterName = "@travel_date";
            parameter.Value = DateTime.UtcNow.AddHours(8);

            List<RTServiceAlerts> serviceAlerts = db.Database
                .SqlQuery<RTServiceAlerts>("RTServiceAlertsGetActive @travel_date"
                    , parameter)
                .ToList();

            FeedMessage feed = new FeedMessage();

            if (serviceAlerts.Count > 0)
            {

                foreach (RTServiceAlerts serviceAlert in serviceAlerts)
                {
                    EntitySelector entitySel = new EntitySelector();
                    entitySel.RouteId = serviceAlert.route_id;
                    entitySel.StopId = serviceAlert.stop_id;

                    Alert alert = new Alert();
                    alert.HeaderText = Functions.GenerateTranslatedString(serviceAlert.description);
                    alert.DescriptionText = Functions.GenerateTranslatedString(serviceAlert.header);
                    alert.InformedEntities.Add(entitySel);
                    //alert.Url = GenerateTranslatedString(serviceAlert.id.ToString());

                    TimeRange tr = new TimeRange();
                    tr.Start = Functions.ToEpoch(serviceAlert.start_date.AddHours(-8));
                    tr.End = Functions.ToEpoch(serviceAlert.end_date.AddHours(-8));

                    alert.ActivePeriods.Add(tr);

                    FeedEntity entity = new FeedEntity();
                    entity.Alert = alert;
                    entity.Id = serviceAlert.id.ToString();

                    feed.Entities.Add(entity);
                }

                byte[] objSerialized = Functions.ProtoSerialize(feed);

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                string filename = "alert.pb";


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

            //byte[] objSerialized = Functions.ProtoSerialize(feed);

            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            //string filename = "alert.pb";
            //Functions.CreatePBFile(storageAccount, filename, objSerialized);
        }
    }
}
