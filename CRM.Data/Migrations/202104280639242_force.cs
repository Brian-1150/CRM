namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class force : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CalendarEvent", "Customer_CustomerID", "dbo.Customer");
            DropIndex("dbo.CalendarEvent", new[] { "Customer_CustomerID" });
            DropColumn("dbo.CalendarEvent", "Customer_CustomerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CalendarEvent", "Customer_CustomerID", c => c.Int());
            CreateIndex("dbo.CalendarEvent", "Customer_CustomerID");
            AddForeignKey("dbo.CalendarEvent", "Customer_CustomerID", "dbo.Customer", "CustomerID");
        }
    }
}
