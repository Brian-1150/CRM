namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobDeleted", "DateAsString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobDeleted", "DateAsString");
        }
    }
}
