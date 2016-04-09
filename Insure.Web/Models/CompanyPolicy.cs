using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Insure.Web.Models
{
    public class CompanyPolicy
    {
        public string Id { get; set; }
        public string PolicyId { get; set; }
        public string InsuranceCompanyId { get; set; }


        public CompanyPolicy()
        {
            //InsuranceCompany = new InsuranceCompany();
            //Policy = new Policy();
            //Policy.PolicyId = "1";
            //Policy.PolicyName = "Sample Policy";
            //Policy.MonthlyPremium = 100;
            //Policy.Deductible = 1000;
            //Policy.IsCoinsurance= true;
            //Policy.IsCopay = false;
            //Policy.CoInsurancePercentage = 10;
            //Policy.PCPCopay = 0;
            //Policy.ERCopay = 0;
            //Policy.InsuranceCompanyId = "ABC";
            //Policy.InsuranceCompany = InsuranceCompany;


            //InsuranceCompany.InsuranceCompanyId = "ABC";
            //InsuranceCompany.CompanyName= "ABC Insurance Company";
            //InsuranceCompany.Policies = new List<Policy>();
            //InsuranceCompany.CompanyWebSite = "http://www.healthcare.gov";
            //InsuranceCompany.Policies.Add(Policy);

        }
    }
}