using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Insure.Web.Models
{
    public class Policy
    {
        public string PolicyId { get; set; }
        [DisplayName("Policy")]
        public string PolicyName { get; set; }
        [DisplayName("Monthly Premium")]
        [DataType(DataType.Currency)]
        public decimal MonthlyPremium { get; set; }
        public int Deductible { get; set; }
        public bool IsCoinsurance { get; set; }
        public decimal CoInsurancePercentage { get;set; }
        public bool IsCopay { get; set; }
        public int? PCPCopay { get; set; }
        public int? ERCopay { get; set; }
        public CompanyPolicy CompanyPolicy { get; set; }




        public Policy()
        {

        }
    }
}