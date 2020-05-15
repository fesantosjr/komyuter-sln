namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editrttripupdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.rt_trip_updates", "direction_id", c => c.Int(nullable: false));
            AddColumn("dbo.rt_trip_updates", "delay", c => c.Int(nullable: false));
            AddColumn("dbo.rt_trip_updates", "start_time", c => c.String(nullable: false));
            AlterColumn("dbo.rt_trip_updates", "start_date", c => c.String(nullable: false));
            DropColumn("dbo.rt_trip_updates", "vehicle_id");
            DropColumn("dbo.rt_trip_updates", "vehicle_label");
        }
        
        public override void Down()
        {
            AddColumn("dbo.rt_trip_updates", "vehicle_label", c => c.String());
            AddColumn("dbo.rt_trip_updates", "vehicle_id", c => c.String());
            AlterColumn("dbo.rt_trip_updates", "start_date", c => c.DateTime(nullable: false));
            DropColumn("dbo.rt_trip_updates", "start_time");
            DropColumn("dbo.rt_trip_updates", "delay");
            DropColumn("dbo.rt_trip_updates", "direction_id");
        }
    }
}
