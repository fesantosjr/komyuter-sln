namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class simulvehiclepositions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.simul_vehicle_positions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        route_id = c.String(nullable: false, maxLength: 35),
                        direction_id = c.Int(nullable: false),
                        arrival_time = c.Time(nullable: false, precision: 7),
                        pos_lat = c.Double(nullable: false),
                        pos_lon = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.simul_vehicle_positions");
        }
    }
}
