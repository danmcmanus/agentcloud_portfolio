using Insure.Web.Models.Salesforce1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Insure.Web.Models
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FilesContext>
    {
        protected override void Seed(FilesContext context)
        {
            var users = new List<Insure.Web.Models.AppUser>
            {
                new AppUser {FirstName="David",LastName="Gold",Age=27 },
                new AppUser {FirstName="Andrew",LastName="Llewellyn",Age=37 },
                new AppUser {FirstName="Michael",LastName="Terrill",Age=27 },
                new AppUser {FirstName="Debbie",LastName="Zwick",Age=58 },
                new AppUser {FirstName="Robert",LastName="Jones",Age=44 },
                new AppUser {FirstName="Laura",LastName="Norman",Age=51 },
                new AppUser {FirstName="Sally",LastName="Martins",Age=36 }
            };

            users.ForEach(u => context.AppUsers.Add(u));
            context.SaveChanges();

            var companies = new List<Company>
            {
                new Company {Id=001,Name="WPS Insurance",Website="https://www.wpsic.com" },
                new Company {Id=002,Name="United HealthCare",Website="http://uhc.com" },
                new Company {Id=003,Name="Common Ground HealthCare",Website="http://www.wecareforwisconsin.com" },
                new Company {Id=004,Name="Blue Cross Blue Shield",Website="http://www.bcbs.com" }
            };

            companies.ForEach(c => context.Companies.Add(c));
            context.SaveChanges();

            var policies = new List<Policy>
            {
                new Policy {AppUserId=1,CompanyId=001,Name="Aurora HMO 5500 HDHP",Premium=250,Deductible=5500,CoInsurance=20,Out_Of_Pocket_Max=11000 },
                new Policy {AppUserId=2,CompanyId=001,Name="Aurora 5500 HDHP POS",Premium=280,Deductible=5500,CoInsurance=20,Out_Of_Pocket_Max=11000 },
                new Policy {AppUserId=3,CompanyId=002,Name="Silver Compass 4500", Premium=385,Deductible=4500,CoPay=10,Out_Of_Pocket_Max=8000 },
                new Policy {AppUserId=4,CompanyId=002,Name="Gold Compass 1000",Premium=429,Deductible=1000,CoPay=30,Out_Of_Pocket_Max=2000 },
                new Policy {AppUserId=5,CompanyId=003,Name="Envision Aurora Bellin PPO - Silver 2400/80/Copay35",Premium=332,Deductible=2400,CoPay=35,Out_Of_Pocket_Max=6000 },
                new Policy {AppUserId=6,CompanyId=003,Name="Envision Aurora Bellin PPO - Gold 1000/90-PPO",Premium=410,Deductible=1000,CoPay=35,Out_Of_Pocket_Max=4000 },
                new Policy {AppUserId=7,CompanyId=004,Name="Anthem Bronze Blue Priority X WI 6050 25 - HMO",Premium=272,Deductible=6050,CoInsurance=25,Out_Of_Pocket_Max=12000 }
            };
            policies.ForEach(p => context.Policies.Add(p));
            context.SaveChanges();

            var contacts = new List<Contact>
            {
                new Contact {Id="1",FirstName="Max",LastName="Powers" },
                new Contact {Id="2",FirstName="Sean",LastName="Tarley" }
            };
            contacts.ForEach(c => context.Contacts.Add(c));
            context.SaveChanges();

        }
    }
}