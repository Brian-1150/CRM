namespace CRM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarEvent",
                c => new
                    {
                        CalEventID = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Start = c.DateTimeOffset(nullable: false, precision: 7),
                        End = c.DateTimeOffset(nullable: false, precision: 7),
                        Title = c.String(),
                        Details = c.String(),
                        ColorOfEvent = c.Int(nullable: false),
                        Employee_EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.CalEventID)
                .ForeignKey("dbo.Employee", t => t.Employee_EmployeeID)
                .Index(t => t.Employee_EmployeeID);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        CalendarEventID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        PayCheckID = c.Int(),
                        InvoiceID = c.Int(),
                        EmployeePay = c.Double(nullable: false),
                        CustomerCharge = c.Double(nullable: false),
                        JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CalendarEventID)
                .ForeignKey("dbo.CalendarEvent", t => t.CalendarEventID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Invoice", t => t.InvoiceID)
                .ForeignKey("dbo.PayCheck", t => t.PayCheckID)
                .Index(t => t.CalendarEventID)
                .Index(t => t.CustomerID)
                .Index(t => t.EmployeeID)
                .Index(t => t.PayCheckID)
                .Index(t => t.InvoiceID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        InitialDateOfContact = c.DateTimeOffset(nullable: false, precision: 7),
                        StatusOfCustomer = c.Int(nullable: false),
                        IsOnDoNotContactList = c.Boolean(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        StateOfPerson = c.Int(nullable: false),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        InvoiceAmount = c.Double(nullable: false),
                        AdjustmentNotes = c.String(),
                        Paid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Current = c.Boolean(nullable: false),
                        HireDate = c.DateTimeOffset(nullable: false, precision: 7),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        StateOfPerson = c.Int(nullable: false),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.PayCheck",
                c => new
                    {
                        PayCheckID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        PayCheckAmount = c.Double(nullable: false),
                        AdjustmentNotes_Capacity = c.Int(nullable: false),
                        AdjustmentNotes_Length = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PayCheckID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.JobDeleted",
                c => new
                    {
                        DeletedJobID = c.Int(nullable: false, identity: true),
                        JobID = c.Int(nullable: false),
                        CalendarEventID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        EmployeePay = c.Double(nullable: false),
                        CustomerCharge = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DeletedJobID);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Job", "PayCheckID", "dbo.PayCheck");
            DropForeignKey("dbo.Job", "InvoiceID", "dbo.Invoice");
            DropForeignKey("dbo.Job", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.PayCheck", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.CalendarEvent", "Employee_EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Job", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Invoice", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Job", "CalendarEventID", "dbo.CalendarEvent");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.PayCheck", new[] { "EmployeeID" });
            DropIndex("dbo.Invoice", new[] { "CustomerID" });
            DropIndex("dbo.Job", new[] { "InvoiceID" });
            DropIndex("dbo.Job", new[] { "PayCheckID" });
            DropIndex("dbo.Job", new[] { "EmployeeID" });
            DropIndex("dbo.Job", new[] { "CustomerID" });
            DropIndex("dbo.Job", new[] { "CalendarEventID" });
            DropIndex("dbo.CalendarEvent", new[] { "Employee_EmployeeID" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.JobDeleted");
            DropTable("dbo.PayCheck");
            DropTable("dbo.Employee");
            DropTable("dbo.Invoice");
            DropTable("dbo.Customer");
            DropTable("dbo.Job");
            DropTable("dbo.CalendarEvent");
        }
    }
}
