namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rttripupdateupdatedelaydatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.navi_rt_trip_updates", "delay", c => c.Int(nullable: false));
            AlterColumn("dbo.rt_trip_updates", "delay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.rt_trip_updates", "delay", c => c.Double(nullable: false));
            AlterColumn("dbo.navi_rt_trip_updates", "delay", c => c.Double(nullable: false));
        }
    }
}
