namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navirtvehiclepositions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.navi_rt_vehicle_positions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        rt_id = c.Int(nullable: false),
                        route_id = c.String(nullable: false, maxLength: 35),
                        trip_id = c.String(nullable: false, maxLength: 35),
                        direction_id = c.Int(nullable: false),
                        start_date = c.String(nullable: false, maxLength: 8),
                        start_time = c.String(nullable: false, maxLength: 8),
                        latitude = c.Double(nullable: false),
                        longtitude = c.Double(nullable: false),
                        timestamp = c.Long(nullable: false),
                        stop_id = c.String(maxLength: 35),
                        current_stop_sequence = c.Int(),
                        vehicle_id = c.String(maxLength: 35),
                        vehicle_label = c.String(maxLength: 35),
                        vehicle_license_plate = c.String(maxLength: 35),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.navi_rt_vehicle_positions");
        }
    }
}
