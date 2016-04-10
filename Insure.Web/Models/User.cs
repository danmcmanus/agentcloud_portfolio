using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Insure.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public int Age { get; set; }
        public int PolicyId { get; set; }

        public virtual ICollection<Policy> Policies { get; set; }
        [DisplayName("Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
            
        }


    }
}