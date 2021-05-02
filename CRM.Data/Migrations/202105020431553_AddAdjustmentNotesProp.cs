namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdjustmentNotesProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "AdjustmentNotes_Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.Invoice", "AdjustmentNotes_Length", c => c.Int(nullable: false));
            DropColumn("dbo.Invoice", "AdjustmentNotes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoice", "AdjustmentNotes", c => c.String());
            DropColumn("dbo.Invoice", "AdjustmentNotes_Length");
            DropColumn("dbo.Invoice", "AdjustmentNotes_Capacity");
        }
    }
}
