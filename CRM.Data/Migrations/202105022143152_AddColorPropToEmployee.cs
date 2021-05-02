namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColorPropToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "ColorOfEmployee", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "ColorOfEmployee");
        }
    }
}
