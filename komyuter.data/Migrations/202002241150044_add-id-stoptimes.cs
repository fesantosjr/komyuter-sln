namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addidstoptimes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.agency",
                c => new
                    {
                        agency_id = c.String(nullable: false, maxLength: 11),
                        agency_name = c.String(nullable: false, maxLength: 255),
                        agency_url = c.String(nullable: false, maxLength: 255),
                        agency_timezone = c.String(nullable: false, maxLength: 50),
                        agency_lang = c.String(maxLength: 2),
                        agency_phone = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.agency_id);
            
            CreateTable(
                "dbo.calendar",
                c => new
                    {
                        service_id = c.String(nullable: false, maxLength: 15),
                        monday = c.Boolean(nullable: false),
                        tuesday = c.Boolean(nullable: false),
                        wednesday = c.Boolean(nullable: false),
                        thursday = c.Boolean(nullable: false),
                        friday = c.Boolean(nullable: false),
                        saturday = c.Boolean(nullable: false),
                        sunday = c.Boolean(nullable: false),
                        start_date = c.String(nullable: false, maxLength: 8),
                        end_date = c.String(nullable: false, maxLength: 8),
                    })
                .PrimaryKey(t => t.service_id);
            
            CreateTable(
                "dbo.mobile_device",
                c => new
                    {
                        mobile_device_id = c.Int(nullable: false, identity: true),
                        mobile_number = c.String(nullable: false, maxLength: 25),
                        access_token = c.String(nullable: false, maxLength: 255),
                        optin_date = c.DateTime(nullable: false),
                        optout_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.mobile_device_id);
            
            CreateTable(
                "dbo.routes",
                c => new
                    {
                        route_id = c.String(nullable: false, maxLength: 15),
                        agency_id = c.String(maxLength: 11),
                        route_short_name = c.String(maxLength: 50),
                        route_long_name = c.String(maxLength: 255),
                        route_type = c.Int(nullable: false),
                        route_text_color = c.String(maxLength: 255),
                        route_color = c.String(maxLength: 255),
                        route_url = c.String(maxLength: 255),
                        route_desc = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.route_id);
            
            CreateTable(
                "dbo.shapes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        shape_id = c.String(nullable: false, maxLength: 30),
                        shape_pt_lat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        shape_pt_lon = c.Decimal(nullable: false, precision: 18, scale: 2),
                        shape_pt_sequence = c.Int(nullable: false),
                        shape_dist_traveled = c.Single(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sms_incoming",
                c => new
                    {
                        sms_id = c.Int(nullable: false, identity: true),
                        message = c.String(nullable: false, maxLength: 500),
                        receive_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.sms_id);
            
            CreateTable(
                "dbo.stops",
                c => new
                    {
                        stop_id = c.String(nullable: false, maxLength: 20),
                        stop_code = c.String(maxLength: 50),
                        stop_name = c.String(maxLength: 255),
                        stop_desc = c.String(maxLength: 255),
                        stop_lat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        stop_lon = c.Decimal(nullable: false, precision: 18, scale: 2),
                        zone_id = c.Int(),
                        stop_url = c.String(maxLength: 255),
                        location_type = c.Int(),
                        parent_station = c.Int(),
                        wheelchair_boarding = c.Int(),
                    })
                .PrimaryKey(t => t.stop_id);
            
            CreateTable(
                "dbo.stop_times",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        trip_id = c.String(nullable: false, maxLength: 35),
                        arrival_time = c.Time(nullable: false, precision: 7),
                        arrival_time_seconds = c.Int(nullable: false),
                        departure_time = c.Time(nullable: false, precision: 7),
                        departure_time_seconds = c.Int(nullable: false),
                        stop_id = c.String(nullable: false, maxLength: 20),
                        stop_sequence = c.Int(nullable: false),
                        stop_headsign = c.String(maxLength: 50),
                        pickup_type = c.Int(),
                        drop_off_type = c.Int(),
                        shape_dist_traveled = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lbs_log",
                c => new
                    {
                        log_id = c.Int(nullable: false, identity: true),
                        device_id = c.Int(nullable: false),
                        log = c.String(nullable: false, maxLength: 500),
                        date_created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.log_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.lbs_log");
            DropTable("dbo.stop_times");
            DropTable("dbo.stops");
            DropTable("dbo.sms_incoming");
            DropTable("dbo.shapes");
            DropTable("dbo.routes");
            DropTable("dbo.mobile_device");
            DropTable("dbo.calendar");
            DropTable("dbo.agency");
        }
    }
}
