namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLocationProp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CalendarEvent", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CalendarEvent", "Location", c => c.String());
        }
    }
}
