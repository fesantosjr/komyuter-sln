namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stopshapesfixprecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.shapes", "shape_pt_lat", c => c.Double(nullable: false));
            AlterColumn("dbo.shapes", "shape_pt_lon", c => c.Double(nullable: false));
            AlterColumn("dbo.stops", "stop_lat", c => c.Double(nullable: false));
            AlterColumn("dbo.stops", "stop_lon", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.stops", "stop_lon", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.stops", "stop_lat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.shapes", "shape_pt_lon", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.shapes", "shape_pt_lat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
