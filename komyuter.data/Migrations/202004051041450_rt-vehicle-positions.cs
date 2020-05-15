namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rtvehiclepositions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.rt_vehicle_positions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        route_id = c.String(nullable: false, maxLength: 35),
                        trip_id = c.String(nullable: false, maxLength: 35),
                        direction_id = c.Int(nullable: false),
                        start_date = c.String(nullable: false),
                        start_time = c.String(nullable: false),
                        latitude = c.Double(nullable: false),
                        longtitude = c.Double(nullable: false),
                        timestamp = c.Long(nullable: false),
                        stop_id = c.String(maxLength: 35),
                        current_stop_sequence = c.Int(),
                        vehicle_id = c.String(maxLength: 35),
                        vehicle_label = c.String(maxLength: 35),
                        vehicle_license_plate = c.String(maxLength: 8),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.rt_vehicle_positions");
        }
    }
}
