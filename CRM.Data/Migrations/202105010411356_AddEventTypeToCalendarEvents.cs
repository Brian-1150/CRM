namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventTypeToCalendarEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarEvent", "TypeOfEvent", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarEvent", "TypeOfEvent");
        }
    }
}
