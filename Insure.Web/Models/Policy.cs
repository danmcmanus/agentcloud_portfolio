using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Insure.Web.Helpers;

namespace Insure.Web.Models
{
    public class Policy
    {
        public int Id { get; set; }
        [DisplayName("Policy Name")]
        public string Name { get; set; }

        [DisplayName("Monthly Premium")]
        [DataType(DataType.Currency)]
        public double Premium { get; set; }

        [DisplayName("Annual Deductible")]
        [DataType(DataType.Currency)]
        public double Deductible { get; set; }

        [DisplayName("Co-Insurance Amount")]
        [DisplayFormat(NullDisplayText ="-")]
        public int? CoInsurance { get; set; }

        [DisplayName("Co-Pay Amount")]
        [DisplayFormat(NullDisplayText = "")]
        [DataType(DataType.Currency)]
        public double? CoPay { get; set; }
        [DisplayName("Select Company")]                               
        public int CompanyId { get; set; }
        [DisplayName("Select User")]
        public int UserId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }

    }
}