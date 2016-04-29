namespace Insure.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calculator",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Company_Id = c.Int(),
                        Policy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .ForeignKey("dbo.Policy", t => t.Policy_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Policy_Id);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Policy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Premium = c.Double(nullable: false),
                        Deductible = c.Double(nullable: false),
                        CoInsurance = c.Int(),
                        Out_Of_Pocket_Max = c.Int(nullable: false),
                        CoPay = c.Double(),
                        CompanyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        PolicyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        MasterRecordId = c.String(),
                        AccountId = c.String(),
                        LastName = c.String(maxLength: 80),
                        FirstName = c.String(maxLength: 40),
                        Salutation = c.String(),
                        Name = c.String(maxLength: 121),
                        OtherStreet = c.String(),
                        OtherCity = c.String(maxLength: 40),
                        OtherState = c.String(maxLength: 80),
                        OtherPostalCode = c.String(maxLength: 20),
                        OtherCountry = c.String(maxLength: 80),
                        OtherLatitude = c.Double(),
                        OtherLongitude = c.Double(),
                        MailingStreet = c.String(),
                        MailingCity = c.String(maxLength: 40),
                        MailingState = c.String(maxLength: 80),
                        MailingPostalCode = c.String(maxLength: 20),
                        MailingCountry = c.String(maxLength: 80),
                        MailingLatitude = c.Double(),
                        MailingLongitude = c.Double(),
                        Phone = c.String(),
                        Fax = c.String(),
                        MobilePhone = c.String(),
                        HomePhone = c.String(),
                        OtherPhone = c.String(),
                        AssistantPhone = c.String(),
                        ReportsToId = c.String(),
                        Email = c.String(),
                        Title = c.String(maxLength: 128),
                        Department = c.String(maxLength: 80),
                        AssistantName = c.String(maxLength: 40),
                        LeadSource = c.String(),
                        Birthdate = c.DateTimeOffset(precision: 7),
                        Description = c.String(),
                        OwnerId = c.String(),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedById = c.String(),
                        LastModifiedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        LastModifiedById = c.String(),
                        SystemModstamp = c.DateTimeOffset(nullable: false, precision: 7),
                        LastActivityDate = c.DateTimeOffset(precision: 7),
                        LastCURequestDate = c.DateTimeOffset(precision: 7),
                        LastCUUpdateDate = c.DateTimeOffset(precision: 7),
                        LastViewedDate = c.DateTimeOffset(precision: 7),
                        LastReferencedDate = c.DateTimeOffset(precision: 7),
                        EmailBouncedReason = c.String(maxLength: 255),
                        EmailBouncedDate = c.DateTimeOffset(precision: 7),
                        IsEmailBounced = c.Boolean(nullable: false),
                        PhotoUrl = c.String(),
                        Jigsaw = c.String(maxLength: 20),
                        JigsawContactId = c.String(maxLength: 20),
                        CleanStatus = c.String(),
                        Level__c = c.String(),
                        Languages__c = c.String(maxLength: 100),
                        Company_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "User_Id", "dbo.User");
            DropForeignKey("dbo.Contact", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.Calculator", "Policy_Id", "dbo.Policy");
            DropForeignKey("dbo.Calculator", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.Policy", "UserId", "dbo.User");
            DropForeignKey("dbo.Policy", "CompanyId", "dbo.Company");
            DropIndex("dbo.Contact", new[] { "User_Id" });
            DropIndex("dbo.Contact", new[] { "Company_Id" });
            DropIndex("dbo.Policy", new[] { "UserId" });
            DropIndex("dbo.Policy", new[] { "CompanyId" });
            DropIndex("dbo.Calculator", new[] { "Policy_Id" });
            DropIndex("dbo.Calculator", new[] { "Company_Id" });
            DropTable("dbo.Contact");
            DropTable("dbo.User");
            DropTable("dbo.Policy");
            DropTable("dbo.Company");
            DropTable("dbo.Calculator");
        }
    }
}
