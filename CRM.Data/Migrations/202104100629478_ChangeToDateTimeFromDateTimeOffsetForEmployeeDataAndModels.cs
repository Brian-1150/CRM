namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToDateTimeFromDateTimeOffsetForEmployeeDataAndModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee", "HireDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "HireDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
