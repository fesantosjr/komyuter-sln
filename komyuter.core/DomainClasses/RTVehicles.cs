using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
    public class RTVehicles
	{
		public int id { get; set; }
		public string trip_id { get; set; }
		public string route_id { get; set; }
		public DateTime start_date { get; set; }
		public double vehicle_lat { get; set; }
		public double vehicle_lon { get; set; }
		public int current_stop_sequence { get; set; }
		public DateTime timestamp  { get; set; }
		public string stop_id { get; set; }
		public string vehicle_id { get; set; }
		public string vehicle_label { get; set; }
		/*
trip
{
	trip_id
	start_date
	schedule_relationship
	route_id
}
position
{
	latitude
	longitude
}
current_stop_sequence
timestamp
stop_id
vehicle
{
	id
	label
}
         */
	}
}
