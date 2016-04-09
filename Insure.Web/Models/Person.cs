using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Insure.Web.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [DataType(DataType.PostalCode)]
        public string Zipcode { get; set; }
        [ScaffoldColumn(true)]
        public Policy Policy { get; set; }
        public virtual ICollection<Policy> PoliciesToCompare { get; set; }
    }
}