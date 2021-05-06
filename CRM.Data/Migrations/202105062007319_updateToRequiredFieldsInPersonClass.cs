namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateToRequiredFieldsInPersonClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Employee", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "LastName", c => c.String());
            AlterColumn("dbo.Customer", "LastName", c => c.String());
        }
    }
}
