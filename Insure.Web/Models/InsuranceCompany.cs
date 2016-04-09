using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Insure.Web.Models
{
    public class InsuranceCompany
    {
        public string InsuranceCompanyId { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("Company Website")]
        [DataType(DataType.Url)]
        public string CompanyWebSite { get; set; }
        public ICollection<Policy> Policies { get; set; }
        public CompanyPolicy CompanyPolicies { get; set; }

    }
}