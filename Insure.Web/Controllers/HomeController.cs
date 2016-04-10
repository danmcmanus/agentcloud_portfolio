using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Insure.Web.Models;
using Insure.Web.ViewModels;

namespace Insure.Web.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<CompanyClientsGroup> data = from user in db.Policies
                                                   group user by user.Company into userGroup
                                                   select new CompanyClientsGroup()
                                                   {
                                                       CompanyUsers = userGroup.Key,
                                                       UserCount = userGroup.Count()
                                                   };
            return View(data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}