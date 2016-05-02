namespace Insure.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        PolicyId = c.Int(nullable: false),
                        FileId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RowId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Original = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 25),
                        Size = c.Long(nullable: false),
                        UploadTime = c.DateTime(nullable: false),
                        Data = c.Binary(),
                        Preview = c.Binary(),
                        Path = c.String(nullable: false, maxLength: 512),
                        Meta = c.Binary(maxLength: 512),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.Policies",
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
                        AppUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Calculators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Company_Id = c.Int(),
                        Policy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .ForeignKey("dbo.Policies", t => t.Policy_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Policy_Id);
            
            CreateTable(
                "dbo.Contacts",
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
                        AppUser_Id = c.Int(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Contacts", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Calculators", "Policy_Id", "dbo.Policies");
            DropForeignKey("dbo.Calculators", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Policies", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Policies", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.Files", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.Contacts", new[] { "Company_Id" });
            DropIndex("dbo.Contacts", new[] { "AppUser_Id" });
            DropIndex("dbo.Calculators", new[] { "Policy_Id" });
            DropIndex("dbo.Calculators", new[] { "Company_Id" });
            DropIndex("dbo.Policies", new[] { "AppUserId" });
            DropIndex("dbo.Policies", new[] { "CompanyId" });
            DropIndex("dbo.Files", new[] { "AppUser_Id" });
            DropTable("dbo.Contacts");
            DropTable("dbo.Calculators");
            DropTable("dbo.Companies");
            DropTable("dbo.Policies");
            DropTable("dbo.Files");
            DropTable("dbo.AppUsers");
        }
    }
}
