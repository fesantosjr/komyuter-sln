using komyuter.core.GlobeClasses;
using komyuter.simulator.Classes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace komyuter.simulator
{
    public partial class frmSendTripUpdate : Form
    {
        public frmSendTripUpdate()
        {
            InitializeComponent();
        }

        private void frmSendTripUpdate_Load(object sender, EventArgs e)
        {
            //backgroundWorker.RunWorkerAsync();


            DateTime now = DateTime.UtcNow.AddHours(8);
            TimeSpan ts = new TimeSpan(now.Hour, now.Minute, 0);

            processTrip(ts);
            //for

            int i = 0;
            //// send updates for earlier trips

            ////Timer MyTimer = new Timer();
            timer.Interval = (60 * 1000); // 1 min
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
        }

        private void processTrip(TimeSpan now)
        {
            // load data from source file
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            "simultrips.txt");

            string tripData = File.ReadAllText(filePath);
            List<SimulTrips> trips = JsonConvert.DeserializeObject<List<SimulTrips>>(tripData);

            bool withUpdate = false;

            foreach (SimulTrips trip in trips)
            {
                if (trip.update_status == "")// && trip.trip_update_time <= now)
                {
                    string actualStartTime = "";

                    if (trip.trip_update_time != now)
                    {
                        actualStartTime = " " + trip.actual_start_time.ToString(@"hhmmss");
                    }

                    sendUpdateSMS(trip, actualStartTime);
                    trip.update_status = "success";
                    txtMonitor.AppendText(trip.trip_id + " " + trip.trip_update_time + " - update sent");
                    txtMonitor.AppendText(Environment.NewLine);

                    withUpdate = true;
                }
            }

            if (withUpdate)
            {
                string tripsStr = JsonConvert.SerializeObject(trips);
                System.IO.File.WriteAllText(@filePath, tripsStr);
            }
            else
            {
                txtMonitor.AppendText("nothing to update. " + now.ToString(@"hhmm"));
                txtMonitor.AppendText(Environment.NewLine);
            }
        }

        public void sendUpdateSMS(SimulTrips trip, string actualStartTime)
        {
            string message = "START " + trip.route_id.Substring(3, trip.route_id.Length - 3);

            if (trip.direction_id == 0)
            {
                if (trip.route_id == "ROULRT2")
                    message += " EB";
                else
                    message += " NB";
            }
            else
            {
                if (trip.route_id == "ROULRT2")
                    message += " WB";
                else
                    message += " SB";
            }

            message += " " + trip.start_time.ToString(@"hhmm");

            message += actualStartTime;

            _inboundSMSMessage sms = new _inboundSMSMessage();
            sms.dateTime = "Fri Nov 22 2013 12:12:13 GMT+0000 (UTC)";
            sms.message = message;
            sms.destinationAddress = "";
            sms.senderAddress = "tel:+639170000000";

            List<_inboundSMSMessage> smss = new List<_inboundSMSMessage>();
            smss.Add(sms);

            _inboundSMSMessageList smsList = new _inboundSMSMessageList();
            smsList.inboundSMSMessage = smss;
            smsList.numberOfMessagesInThisBatch = "1";

            globe_sms_mo sms_mo = new globe_sms_mo();
            sms_mo.inboundSMSMessageList = smsList;

            string jsonData = JsonConvert.SerializeObject(sms_mo);

            var client = new RestClient("https://komyutersms.azurewebsites.net/api/SMSIncoming");
            //var client = new RestClient("https://localhost:44358/api/SMSIncoming");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonData, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.UtcNow.AddHours(8);
            TimeSpan ts = new TimeSpan(now.Hour, now.Minute, 0);
            processTrip(ts);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
        }
    }
}
