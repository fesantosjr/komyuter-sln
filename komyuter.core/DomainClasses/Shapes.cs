using System;

namespace komyuter.core.DomainClasses
{
    public class Shapes
    {
        public int id { get; set; }
        public string shape_id { get; set; }
        public double shape_pt_lat { get; set; }
        public double shape_pt_lon { get; set; }
        public int shape_pt_sequence { get; set; }
        public float shape_dist_traveled { get; set; }
    }
}
