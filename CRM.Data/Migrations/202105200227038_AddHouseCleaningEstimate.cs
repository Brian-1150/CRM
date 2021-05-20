namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHouseCleaningEstimate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HouseCleaningEstimate",
                c => new
                    {
                        EstimateID = c.Int(nullable: false, identity: true),
                        NumberOfBedrooms = c.Int(nullable: false),
                        NumberOfFullBath = c.Int(nullable: false),
                        NumberOfHalfBath = c.Int(nullable: false),
                        Basement = c.Boolean(nullable: false),
                        Notes = c.String(),
                        CustomerID = c.Int(nullable: false),
                        EstimatedCharge = c.Double(nullable: false),
                        EstimatedCostOfMaterials = c.Double(nullable: false),
                        EstimatedHours = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.EstimateID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HouseCleaningEstimate", "CustomerID", "dbo.Customer");
            DropIndex("dbo.HouseCleaningEstimate", new[] { "CustomerID" });
            DropTable("dbo.HouseCleaningEstimate");
        }
    }
}
