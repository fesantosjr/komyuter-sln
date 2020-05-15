namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stoptimeremovesecondsfield : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.stop_times", "arrival_time_seconds");
            DropColumn("dbo.stop_times", "departure_time_seconds");
        }
        
        public override void Down()
        {
            AddColumn("dbo.stop_times", "departure_time_seconds", c => c.Int(nullable: false));
            AddColumn("dbo.stop_times", "arrival_time_seconds", c => c.Int(nullable: false));
        }
    }
}
