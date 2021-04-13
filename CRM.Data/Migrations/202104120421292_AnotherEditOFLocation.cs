namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotherEditOFLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarEvent", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarEvent", "Location");
        }
    }
}
