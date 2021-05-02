namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStringBuilderBackToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "AdjustmentNotes", c => c.String());
            AddColumn("dbo.PayCheck", "AdjustmentNotes", c => c.String());
            DropColumn("dbo.Invoice", "AdjustmentNotes_Capacity");
            DropColumn("dbo.Invoice", "AdjustmentNotes_Length");
            DropColumn("dbo.PayCheck", "AdjustmentNotes_Capacity");
            DropColumn("dbo.PayCheck", "AdjustmentNotes_Length");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PayCheck", "AdjustmentNotes_Length", c => c.Int(nullable: false));
            AddColumn("dbo.PayCheck", "AdjustmentNotes_Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.Invoice", "AdjustmentNotes_Length", c => c.Int(nullable: false));
            AddColumn("dbo.Invoice", "AdjustmentNotes_Capacity", c => c.Int(nullable: false));
            DropColumn("dbo.PayCheck", "AdjustmentNotes");
            DropColumn("dbo.Invoice", "AdjustmentNotes");
        }
    }
}
