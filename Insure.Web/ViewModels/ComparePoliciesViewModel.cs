using Insure.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Insure.Web.ViewModels
{
    public class ComparePoliciesViewModel
    {
        public Policy Policy { get; set; }
        public Company Company { get; set; }

    }
}