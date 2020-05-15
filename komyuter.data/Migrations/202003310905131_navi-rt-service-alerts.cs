namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navirtservicealerts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.navi_rt_service_alerts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        route_id = c.String(nullable: false, maxLength: 20),
                        stop_id = c.String(maxLength: 20),
                        header = c.String(nullable: false, maxLength: 255),
                        description = c.String(nullable: false, maxLength: 500),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.navi_rt_service_alerts");
        }
    }
}
