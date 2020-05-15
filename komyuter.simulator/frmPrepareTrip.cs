using komyuter.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using komyuter.simulator.Classes;
using Newtonsoft.Json;
using System.IO;

namespace komyuter.simulator
{
    public partial class frmPrepareTrip : Form
    {
        private static KomyuterContext db = new KomyuterContext();

        public frmPrepareTrip()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            backgroundWorker.RunWorkerAsync();
        }

        private void frmPrepareTrip_Load(object sender, EventArgs e)
        {
            tripDate.Value = DateTime.UtcNow.AddHours(8);
            tripStart.Value = DateTime.UtcNow.AddHours(8);
            tripEnd.Value = DateTime.UtcNow.AddHours(10);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(20, "Starting search...");

            // get value from
            DateTime dTripDate = Convert.ToDateTime(tripDate.Value);
            DateTime dTripStart = Convert.ToDateTime(tripStart.Value);
            DateTime dTripEnd = Convert.ToDateTime(tripEnd.Value);

            // get data from db
            SqlParameter paramStartDate = new SqlParameter();
            paramStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
            paramStartDate.ParameterName = "@start_date";
            paramStartDate.Value = new DateTime(dTripDate.Year, dTripDate.Month, dTripDate.Day, dTripStart.Hour, dTripStart.Minute, dTripStart.Second);

            SqlParameter paramEndDate = new SqlParameter();
            paramEndDate.SqlDbType = System.Data.SqlDbType.DateTime;
            paramEndDate.ParameterName = "@end_date";
            paramEndDate.Value = new DateTime(dTripDate.Year, dTripDate.Month, dTripDate.Day, dTripEnd.Hour, dTripEnd.Minute, dTripEnd.Second);

            //var routes = ["ROUPNR"];
            List<string> routes = new List<string>();

            if (!chkLRT1.Checked && !chkLRT2.Checked && !chkMRT3.Checked && !chkPNR.Checked)
            {
                routes.Add("ROULRT1");
                routes.Add("ROULRT2");
                routes.Add("ROUMRT3");
                routes.Add("ROUPNR");
            }
            else
            {
                if (chkLRT1.Checked) 
                    routes.Add("ROULRT1");

                if (chkLRT2.Checked)
                    routes.Add("ROULRT2");

                if (chkMRT3.Checked)
                    routes.Add("ROUMRT3");

                if (chkPNR.Checked)
                    routes.Add("ROUPNR");
            }

            List<SimulTrips> trips = db.Database
                .SqlQuery<SimulTrips>("SimulGetTrips @start_date, @end_date", paramStartDate, paramEndDate)
                .Where(x => routes.Contains(x.route_id))
                .ToList();

            worker.ReportProgress(40, "Search complete. Total records: " + trips.Count.ToString());

            if (trips.Count > 0)
            {
                string tripIdOld = "";
                int routeDelay = 0;

                foreach (SimulTrips trip in trips)
                {
                    string tripId = trip.trip_id;
                    int tripDelay = GenerateRandomDelaySeconds(tripIdOld != tripId);

                    if (tripIdOld != tripId)
                        routeDelay = 0;

                    routeDelay += tripDelay;

                    trip.delay = routeDelay;
                    trip.actual_start_time = trip.start_time.Add(TimeSpan.FromSeconds(routeDelay));
                    trip.trip_update_time = new TimeSpan(trip.actual_start_time.Hours, trip.actual_start_time.Minutes, 0);
                    trip.update_status = "";

                    tripIdOld = tripId;
                }


                worker.ReportProgress(60, "Delays generated");

                string tripsStr = JsonConvert.SerializeObject(trips);

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                                "simultrips.txt");
                System.IO.File.WriteAllText(@filePath, tripsStr);

                worker.ReportProgress(80, "File created.");
                worker.ReportProgress(100, "Path: " + filePath);
            }
        }

        private int GenerateRandomDelaySeconds(bool isFirstTrip)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            if (isFirstTrip)
                return (rnd.Next(1, 8) * 60);
            else
                return (rnd.Next(0, 2) * 60);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                txtMonitor.AppendText(e.UserState as string);
                txtMonitor.AppendText(Environment.NewLine);
            }
            this.progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtMonitor.AppendText("Done");
            txtMonitor.AppendText(Environment.NewLine);

            button1.Enabled = true;
            button2.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmSendTripUpdate frm = new frmSendTripUpdate();
            frm.Show();
        }
    }
}
