using komyuter.core.DomainClasses;
using komyuter.data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using komyuter.core.Common;
using Newtonsoft.Json.Linq;
using komyuter.core.GlobeClasses;
using Newtonsoft.Json;
using System.Configuration;

namespace komyuter.webjob_tracker
{
    class Program
    {
        private static KomyuterContext db = new KomyuterContext();

        static void Main(string[] args)
        {
            DateTime phDate = DateTime.UtcNow.AddHours(8);

            // get data from rt trip update ---------------------------------------------------------------------------
            // --------------------------------------------------------------------------------------------------------
            List<RTTripUpdates> tripUpdates = db.Database
                .SqlQuery<RTTripUpdates>("RTTripUpdatesGetForTracking @travel_date, @travel_time",
                    new SqlParameter("@travel_date", phDate.ToString("yyyyMMdd")),
                    new SqlParameter("@travel_time", phDate.ToString("HH:mm:ss")))
                .ToList();

            //bool saveRecord = false;
            int savedRecord = 0;
            foreach (RTTripUpdates tripUpdate in tripUpdates)
            {
                string[] mobileArr = tripUpdate.mobile_number.Split('@');
                string mobileNumber = mobileArr[0];
                string accessToken = mobileArr[1];

                if (mobileNumber != "" && accessToken != "")
                {
                    if (mobileNumber == "9170000000")
                    {
                        savedRecord += doTestCall(tripUpdate, phDate);
                    }
                    else
                    {
                        savedRecord += doGlobeCall(mobileNumber, accessToken, tripUpdate);
                    }
                }
            }

            if (savedRecord > 0)
                db.SaveChanges();  

        }

        static int doGlobeCall(string mobileNumber, string accessToken, RTTripUpdates tripUpdate)
        {
            // call globe api
            JToken jsonReturn = GlobeLabs.LocateDevice(mobileNumber, accessToken);

            string processResult = "";
            if (((JProperty)jsonReturn.First).Name == "error")
            {
                processResult = ((JProperty)jsonReturn.First).Value.ToString();
                return 0;
            }
            else if (((JProperty)jsonReturn.First).Name == "terminalLocationList")
            {
                globe_lbs lbs = JsonConvert.DeserializeObject<globe_lbs>(jsonReturn.ToString());
                double latitude = Convert.ToDouble(lbs.terminalLocationList.terminalLocation.currentLocation.latitude);
                double longitude = Convert.ToDouble(lbs.terminalLocationList.terminalLocation.currentLocation.longitude);

                ShapeHolder sh = DetermineClosestPoint(tripUpdate.trip_id, latitude, longitude);
                //ShapeHolder sh = DetermineClosestPoint(tripUpdate.trip_id, 14.58793, 121.05693);

                if (sh.shape_pt_lat > 0)
                {
                    // save data to rt vehicle position
                    RTVehiclePositions vp = new RTVehiclePositions();

                    vp.route_id = tripUpdate.route_id;
                    vp.trip_id = tripUpdate.trip_id;
                    vp.direction_id = tripUpdate.direction_id;
                    vp.start_date = tripUpdate.start_date;
                    vp.start_time = tripUpdate.start_time;
                    vp.latitude = sh.shape_pt_lat;
                    vp.longitude = sh.shape_pt_lon;
                    vp.timestamp = (long)Functions.ToEpoch(DateTime.UtcNow);

                    db.RTVehiclePositions.Add(vp);
                    return 1;
                }
            }

            return 0;
        }

        static int doTestCall(RTTripUpdates tripUpdate, DateTime phDate)
        {
            TimeSpan startTime = TimeSpan.Parse(tripUpdate.start_time).Add(TimeSpan.FromSeconds(tripUpdate.delay));
            TimeSpan elapseTime = new TimeSpan(phDate.Hour, phDate.Minute, phDate.Second) - startTime;



            List<SimulVehiclePositions> svp = db.Database
                .SqlQuery<SimulVehiclePositions>("SimulVehiclePositionGet @route_id, @direction_id, @elapse_time",
                    new SqlParameter("@route_id", tripUpdate.route_id),
                    new SqlParameter("@direction_id", tripUpdate.direction_id),
                    new SqlParameter("@elapse_time", elapseTime))
                .ToList();

            if (svp.Count > 0)
            {
                RTVehiclePositions vp = new RTVehiclePositions();
                vp.route_id = tripUpdate.route_id;
                vp.trip_id = tripUpdate.trip_id;
                vp.direction_id = tripUpdate.direction_id;
                vp.start_date = tripUpdate.start_date;
                vp.start_time = tripUpdate.start_time;
                vp.latitude = svp[0].pos_lat;
                vp.longitude = svp[0].pos_lon;
                vp.timestamp = (long)Functions.ToEpoch(DateTime.UtcNow);

                db.RTVehiclePositions.Add(vp);
                return 1;
            }

            return 0;
        }

        static ShapeHolder DetermineClosestPoint(string trip_id, double test_lat, double test_lon)
        {
            double positionTolerance = Convert.ToDouble(ConfigurationManager.AppSettings.Get("VehiclePositionTolerance"));
            // get shapes
            List<Shapes> shapes = db.Database
                .SqlQuery<Shapes>("ShapesGetByTripId @trip_id",
                    new SqlParameter("@trip_id", trip_id))
                .ToList();

            List<ShapeHolder> shapeHolders = new List<ShapeHolder>();

            foreach (Shapes shape in shapes)
            {
                double dist = CalculateClosestPoint(test_lat, test_lon, shape.shape_pt_lat, shape.shape_pt_lon);
                if (dist <= positionTolerance)
                    shapeHolders.Add(new ShapeHolder(shape.shape_pt_lat, shape.shape_pt_lon, dist));
            }

            ShapeHolder shapeHolder = new ShapeHolder(0, 0, 0);

            if (shapeHolders.Count > 0)
                shapeHolder  = shapeHolders.OrderBy(o => o.shape_distance).First();
            
            return shapeHolder;

        }

        static double CalculateClosestPoint(double test_lat, double test_lon, double data_lat, double data_lon)
        {
            double test_radlat = Math.PI * test_lat / 180;
            double data_radlat = Math.PI * data_lat / 180;
            double theta = test_lon - data_lon;
            double radtheta = Math.PI * theta / 180;
            double dist = Math.Sin(test_radlat) * Math.Sin(data_radlat) + Math.Cos(test_radlat) * Math.Cos(data_radlat) * Math.Cos(radtheta);
            if (dist > 1)
                dist = 1;
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344;
            return dist;
        }

    }

    public class ShapeHolder
    {
        public ShapeHolder(double _shape_pt_lat, double _shape_pt_lon, double _shape_distance)
        {
            this.shape_pt_lat = _shape_pt_lat;
            this.shape_pt_lon = _shape_pt_lon;
            this.shape_distance = _shape_distance;
        }

        public double shape_pt_lat { get; set; }
        public double shape_pt_lon { get; set; }
        public double shape_distance { get; set; }
    }
}  

//1. Subscribers need to text INFO to 21582044 (Cross-telco: 225652044 )
//2. Upon Receipt of the 'Welcome Message', the subscriber needs to reply YES
//3. After The subscriber replies(Yes), the ACCESS_TOKEN and the subscriber's mobile number will be posted to you Redirect URI
