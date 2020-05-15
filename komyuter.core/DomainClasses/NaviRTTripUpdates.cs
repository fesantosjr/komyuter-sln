using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
	public class NaviRTTripUpdates
	{
		public int id { get; set; }
		public int rt_id { get; set; }
		public string trip_id { get; set; }
		public string route_id { get; set; }
		public int direction_id { get; set; }
		public int delay { get; set; }
		public string start_date { get; set; }
		public string start_time { get; set; }

	}
}
