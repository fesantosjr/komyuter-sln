namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class longitudeupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.navi_rt_vehicle_positions", "longitude", c => c.Double(nullable: false));
            AddColumn("dbo.rt_trip_updates", "mobile_number", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.rt_vehicle_positions", "longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.navi_rt_trip_updates", "trip_id", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("dbo.navi_rt_trip_updates", "route_id", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("dbo.navi_rt_trip_updates", "start_date", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.navi_rt_trip_updates", "start_time", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.rt_trip_updates", "trip_id", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("dbo.rt_trip_updates", "route_id", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("dbo.rt_trip_updates", "start_date", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.rt_trip_updates", "start_time", c => c.String(nullable: false, maxLength: 8));
            DropColumn("dbo.navi_rt_vehicle_positions", "longtitude");
            DropColumn("dbo.rt_vehicle_positions", "longtitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.rt_vehicle_positions", "longtitude", c => c.Double(nullable: false));
            AddColumn("dbo.navi_rt_vehicle_positions", "longtitude", c => c.Double(nullable: false));
            AlterColumn("dbo.rt_trip_updates", "start_time", c => c.String(nullable: false));
            AlterColumn("dbo.rt_trip_updates", "start_date", c => c.String(nullable: false));
            AlterColumn("dbo.rt_trip_updates", "route_id", c => c.String(nullable: false));
            AlterColumn("dbo.rt_trip_updates", "trip_id", c => c.String(nullable: false));
            AlterColumn("dbo.navi_rt_trip_updates", "start_time", c => c.String(nullable: false));
            AlterColumn("dbo.navi_rt_trip_updates", "start_date", c => c.String(nullable: false));
            AlterColumn("dbo.navi_rt_trip_updates", "route_id", c => c.String(nullable: false));
            AlterColumn("dbo.navi_rt_trip_updates", "trip_id", c => c.String(nullable: false));
            DropColumn("dbo.rt_vehicle_positions", "longitude");
            DropColumn("dbo.rt_trip_updates", "mobile_number");
            DropColumn("dbo.navi_rt_vehicle_positions", "longitude");
        }
    }
}
