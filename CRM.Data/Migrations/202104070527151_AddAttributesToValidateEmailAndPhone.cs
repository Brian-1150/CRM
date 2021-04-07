namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttributesToValidateEmailAndPhone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "FirstName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "FirstName", c => c.String());
        }
    }
}
