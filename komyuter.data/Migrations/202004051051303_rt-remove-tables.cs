namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rtremovetables : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.rt_vehicles");
            DropTable("dbo.rt_trip_updates_stoptimes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.rt_trip_updates_stoptimes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        rt_trip_update_id = c.Int(nullable: false),
                        stop_sequence = c.Int(nullable: false),
                        arrival_delay = c.Int(),
                        stop_id = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.rt_vehicles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        trip_id = c.String(nullable: false),
                        route_id = c.String(nullable: false),
                        start_date = c.DateTime(nullable: false),
                        vehicle_lat = c.Double(),
                        vehicle_lon = c.Double(),
                        current_stop_sequence = c.Int(nullable: false),
                        timestamp = c.DateTime(nullable: false),
                        stop_id = c.String(),
                        vehicle_id = c.String(),
                        vehicle_label = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}
