using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insure.Web.Models
{
    public class Calculator
    {
        public int Id { get; set; }
        public Policy Policy { get; set; }
        public Company Company { get; set; }

        public Calculator()
        {
            Insure.Web.Logic.Calculator calculation = new Logic.Calculator();
        }

    }
}