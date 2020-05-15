namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rtvehiclepositionsupdatestartdatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.rt_vehicle_positions", "start_date", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.rt_vehicle_positions", "start_time", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.rt_vehicle_positions", "vehicle_license_plate", c => c.String(maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.rt_vehicle_positions", "vehicle_license_plate", c => c.String(maxLength: 8));
            AlterColumn("dbo.rt_vehicle_positions", "start_time", c => c.String(nullable: false));
            AlterColumn("dbo.rt_vehicle_positions", "start_date", c => c.String(nullable: false));
        }
    }
}
