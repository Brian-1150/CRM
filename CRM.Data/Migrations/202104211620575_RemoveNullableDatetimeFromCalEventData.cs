namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNullableDatetimeFromCalEventData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CalendarEvent", "End", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CalendarEvent", "End", c => c.DateTimeOffset(precision: 7));
        }
    }
}
