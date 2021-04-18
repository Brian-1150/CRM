namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToStringBuilder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayCheck", "AdjustmentNotes_Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.PayCheck", "AdjustmentNotes_Length", c => c.Int(nullable: false));
            DropColumn("dbo.PayCheck", "AdjustmentNotes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PayCheck", "AdjustmentNotes", c => c.String());
            DropColumn("dbo.PayCheck", "AdjustmentNotes_Length");
            DropColumn("dbo.PayCheck", "AdjustmentNotes_Capacity");
        }
    }
}
