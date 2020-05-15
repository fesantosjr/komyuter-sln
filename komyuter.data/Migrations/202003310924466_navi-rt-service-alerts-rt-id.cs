namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navirtservicealertsrtid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.navi_rt_service_alerts", "rt_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.navi_rt_service_alerts", "rt_id");
        }
    }
}
