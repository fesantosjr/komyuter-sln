namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfrequencies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.frequencies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        trip_id = c.String(nullable: false, maxLength: 35),
                        start_time = c.Time(nullable: false, precision: 7),
                        end_time = c.Time(nullable: false, precision: 7),
                        headway_secs = c.Int(nullable: false),
                        exact_times = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.frequencies");
        }
    }
}
