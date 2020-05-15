using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.core.DomainClasses
{
    public class SimulVehiclePositions
    {
		public int id { get; set; }
		public string route_id { get; set; }
		public int direction_id { get; set; }
		public TimeSpan arrival_time { get; set; }
		public double pos_lat { get; set; }
		public double pos_lon { get; set; }
	}
}
