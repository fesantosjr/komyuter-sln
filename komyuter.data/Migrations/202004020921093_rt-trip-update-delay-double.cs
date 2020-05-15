namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rttripupdatedelaydouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.rt_trip_updates", "delay", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.rt_trip_updates", "delay", c => c.Int(nullable: false));
        }
    }
}
