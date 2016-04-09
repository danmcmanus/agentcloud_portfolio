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
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Monthly Premium")]
        [DataType(DataType.Currency)]
        public double Premium { get; set; }

        [DisplayName("Annual Deductible")]
        [DataType(DataType.Currency)]
        public double Deductible { get; set; }

        [DisplayName("Co-Insurance Amount")]
        [DataType(DataType.Currency)]
        public decimal? CoInsurance { get; set; }

        [DisplayName("Co-Pay Amount")]
        [DataType(DataType.Currency)]
        public double? CoPay { get; set; }                                  
        public int CompanyId { get; set; }
        public int UserId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }

    }
}