namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navirttripupdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.navi_rt_trip_updates",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        rt_id = c.Int(nullable: false),
                        trip_id = c.String(nullable: false),
                        route_id = c.String(nullable: false),
                        direction_id = c.Int(nullable: false),
                        delay = c.Double(nullable: false),
                        start_date = c.String(nullable: false),
                        start_time = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.navi_rt_trip_updates");
        }
    }
}
