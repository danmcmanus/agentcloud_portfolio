namespace Insure.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyPolicies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PolicyId = c.String(),
                        InsuranceCompanyId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InsuranceCompanies",
                c => new
                    {
                        InsuranceCompanyId = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(),
                        CompanyWebSite = c.String(),
                        CompanyPolicies_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InsuranceCompanyId)
                .ForeignKey("dbo.CompanyPolicies", t => t.CompanyPolicies_Id)
                .Index(t => t.CompanyPolicies_Id);
            
            CreateTable(
                "dbo.Policies",
                c => new
                    {
                        PolicyId = c.String(nullable: false, maxLength: 128),
                        PolicyName = c.String(),
                        MonthlyPremium = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deductible = c.Int(nullable: false),
                        IsCoinsurance = c.Boolean(nullable: false),
                        CoInsurancePercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCopay = c.Boolean(nullable: false),
                        PCPCopay = c.Int(),
                        ERCopay = c.Int(),
                        CompanyPolicy_Id = c.String(maxLength: 128),
                        InsuranceCompany_InsuranceCompanyId = c.String(maxLength: 128),
                        Person_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PolicyId)
                .ForeignKey("dbo.CompanyPolicies", t => t.CompanyPolicy_Id)
                .ForeignKey("dbo.InsuranceCompanies", t => t.InsuranceCompany_InsuranceCompanyId)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.CompanyPolicy_Id)
                .Index(t => t.InsuranceCompany_InsuranceCompanyId)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Zipcode = c.String(),
                        Policy_PolicyId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Policies", t => t.Policy_PolicyId)
                .Index(t => t.Policy_PolicyId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.People", "Policy_PolicyId", "dbo.Policies");
            DropForeignKey("dbo.Policies", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Policies", "InsuranceCompany_InsuranceCompanyId", "dbo.InsuranceCompanies");
            DropForeignKey("dbo.Policies", "CompanyPolicy_Id", "dbo.CompanyPolicies");
            DropForeignKey("dbo.InsuranceCompanies", "CompanyPolicies_Id", "dbo.CompanyPolicies");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.People", new[] { "Policy_PolicyId" });
            DropIndex("dbo.Policies", new[] { "Person_Id" });
            DropIndex("dbo.Policies", new[] { "InsuranceCompany_InsuranceCompanyId" });
            DropIndex("dbo.Policies", new[] { "CompanyPolicy_Id" });
            DropIndex("dbo.InsuranceCompanies", new[] { "CompanyPolicies_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.People");
            DropTable("dbo.Policies");
            DropTable("dbo.InsuranceCompanies");
            DropTable("dbo.CompanyPolicies");
        }
    }
}
