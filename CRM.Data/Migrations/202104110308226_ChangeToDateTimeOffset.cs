namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToDateTimeOffset : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CalendarEvent");
            AlterColumn("dbo.CalendarEvent", "CalEventID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CalendarEvent", "End", c => c.DateTimeOffset(precision: 7));
            AddPrimaryKey("dbo.CalendarEvent", "CalEventID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CalendarEvent");
            AlterColumn("dbo.CalendarEvent", "End", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.CalendarEvent", "CalEventID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.CalendarEvent", "CalEventID");
        }
    }
}
