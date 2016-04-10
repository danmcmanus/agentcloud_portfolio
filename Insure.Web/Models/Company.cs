using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Insure.Web.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [DisplayName("Company Name")]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }

        public virtual ICollection<Policy> Policies { get; set; }
    }
}