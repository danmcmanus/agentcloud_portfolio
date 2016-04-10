using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Insure.Web.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Insure.Web.ViewModels
{
    public class CompanyClientsGroup
    {
        public Company CompanyUsers { get; set; }
        public int UserCount { get; set; }
    }
}