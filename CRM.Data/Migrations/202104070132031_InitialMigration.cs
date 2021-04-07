namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "InitialDateOfService", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Customer", "StreetAddress", c => c.String());
            AddColumn("dbo.Customer", "City", c => c.String());
            AddColumn("dbo.Customer", "StateOfPerson", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "StateOfPerson");
            DropColumn("dbo.Customer", "City");
            DropColumn("dbo.Customer", "StreetAddress");
            DropColumn("dbo.Customer", "InitialDateOfService");
        }
    }
}
