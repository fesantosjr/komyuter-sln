namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtripstable2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.trips",
                c => new
                    {
                        trip_id = c.String(nullable: false, maxLength: 35),
                        route_id = c.String(nullable: false, maxLength: 35),
                        service_id = c.String(nullable: false, maxLength: 35),
                        trip_headsign = c.String(maxLength: 255),
                        trip_short_name = c.String(maxLength: 50),
                        direction_id = c.Int(),
                        block_id = c.String(),
                        shape_id = c.String(nullable: false, maxLength: 35),
                        wheelchair_accessible = c.Int(),
                        bikes_allowed = c.Int(),
                    })
                .PrimaryKey(t => t.trip_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.trips");
        }
    }
}
