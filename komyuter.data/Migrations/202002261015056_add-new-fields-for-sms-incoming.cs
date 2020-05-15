namespace komyuter.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewfieldsforsmsincoming : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.sms_incoming", "process_date", c => c.DateTime());
            AddColumn("dbo.sms_incoming", "process_remarks", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.sms_incoming", "process_remarks");
            DropColumn("dbo.sms_incoming", "process_date");
        }
    }
}
