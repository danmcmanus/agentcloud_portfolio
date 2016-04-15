using Insure.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Insure.Web.Controllers
{
    public class CalculatorController : Controller
    {
        DataContext db = new DataContext();
        Company select = new Company() {Id=0,Name="Select" };

        public override string ToString()
        {

            return "";
        }
        public ActionResult Index()
        {
            List<string> ListItems = new List<string>();
            ListItems.Add(select.Name);
            var companies = db.Companies.ToList();
            foreach (var item in companies)
            {
                ListItems.Add(item.Name);
            }
            SelectList Companies = new SelectList(ListItems);
            ViewData["Companies"] = Companies;
            return View();
        }

        public JsonResult Policy(string Company)
        {
            DataContext db = new DataContext();
            List <string> policyList = new List<string>();
            switch (Company)
            {
                case "Select":
                    var select = new Company { Name="Select:"};
                    policyList.Add(select.Name);
                    break;
                case "WPS Insurance":
                    var wpsList = from p in db.Policies
                                  where p.Company.Name.Contains("WPS")
                                  select p;
                    foreach (Models.Policy p in wpsList)
                    {
                        policyList.Add(p.Name);
                    }
                    
                    break;
                case "United HealthCare":
                    var uhcList = from p in db.Policies
                                  where p.Company.Name.Contains("United HealthCare")
                                  select p;
                    foreach (Models.Policy p in uhcList)
                    {
                        policyList.Add(p.Name);
                    }
                    break;
                case "Blue Cross Blue Shield":
                    var bcbsList = from p in db.Policies
                                  where p.Company.Name.Contains("Blue Cross Blue Shield")
                                  select p;
                    foreach (Models.Policy p in bcbsList)
                    {
                        policyList.Add(p.Name);
                    }
                    break;
                case "Common Ground HealthCare":
                    var cghcList = from p in db.Policies
                                  where p.Company.Name.Contains("Common Ground Health")
                                  select p;
                    foreach (Models.Policy p in cghcList)
                    {
                        policyList.Add(p.Name);
                    }
                    break;

            }

            return Json(policyList);
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Calculator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calculator/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Calculator/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Calculator/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Calculator/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Calculator/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
