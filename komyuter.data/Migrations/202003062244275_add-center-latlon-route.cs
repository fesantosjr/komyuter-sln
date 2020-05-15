namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcenterlatlonroute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.routes", "default_zoom", c => c.Int());
            AddColumn("dbo.routes", "center_lat", c => c.Double());
            AddColumn("dbo.routes", "center_lon", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.routes", "center_lon");
            DropColumn("dbo.routes", "center_lat");
            DropColumn("dbo.routes", "default_zoom");
        }
    }
}
