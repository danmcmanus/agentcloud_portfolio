using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Insure.Web.Models;

namespace Insure.Web.Controllers
{
    public class InsuranceCompaniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InsuranceCompanies
        public ActionResult Index()
        {

            return View(db.InsuranceCompanies.ToList());
        }

        public ActionResult policiesOffered()
        {
            db.InsuranceCompanies.Include(i => i.Policies);
            var offerings = db.InsuranceCompanies.Select(i => i.Policies);
 


            return View(offerings);
        }

        // GET: InsuranceCompanies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsuranceCompany insuranceCompany = db.InsuranceCompanies.Find(id);
            if (insuranceCompany == null)
            {
                return HttpNotFound();
            }
            return View(insuranceCompany);
        }

        // GET: InsuranceCompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsuranceCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InsuranceCompanyId,CompanyName,CompanyWebSite")] InsuranceCompany insuranceCompany)
        {
            if (ModelState.IsValid)
            {
                db.InsuranceCompanies.Add(insuranceCompany);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuranceCompany);
        }

        // GET: InsuranceCompanies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsuranceCompany insuranceCompany = db.InsuranceCompanies.Find(id);
            if (insuranceCompany == null)
            {
                return HttpNotFound();
            }
            return View(insuranceCompany);
        }

        // POST: InsuranceCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InsuranceCompanyId,CompanyName,CompanyWebSite")] InsuranceCompany insuranceCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuranceCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuranceCompany);
        }

        // GET: InsuranceCompanies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsuranceCompany insuranceCompany = db.InsuranceCompanies.Find(id);
            if (insuranceCompany == null)
            {
                return HttpNotFound();
            }
            return View(insuranceCompany);
        }

        // POST: InsuranceCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            InsuranceCompany insuranceCompany = db.InsuranceCompanies.Find(id);
            db.InsuranceCompanies.Remove(insuranceCompany);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
