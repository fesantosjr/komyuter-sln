using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
	public class NaviRTVehiclePositions
	{
		public int id { get; set; }
		public int rt_id { get; set; }

		public string route_id { get; set; }
		public string trip_id { get; set; }
		public int direction_id { get; set; }
		public string start_date { get; set; }
		public string start_time { get; set; }

		public double latitude { get; set; }
		public double longitude { get; set; }
		public long timestamp { get; set; }

		public string stop_id { get; set; }
		public int current_stop_sequence { get; set; }

		public string vehicle_id { get; set; }
		public string vehicle_label { get; set; }
		public string vehicle_license_plate { get; set; }
	}
}
