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
    public class CompanyPoliciesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CompanyPolicies
        public ActionResult Index()
        {
            return View(db.CompanyPolicies.ToList());
        }

        // GET: CompanyPolicies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyPolicy companyPolicy = db.CompanyPolicies.Find(id);
            if (companyPolicy == null)
            {
                return HttpNotFound();
            }
            return View(companyPolicy);
        }

        // GET: CompanyPolicies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyPolicies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PolicyId,InsuranceCompanyId")] CompanyPolicy companyPolicy)
        {
            if (ModelState.IsValid)
            {
                db.CompanyPolicies.Add(companyPolicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(companyPolicy);
        }

        // GET: CompanyPolicies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyPolicy companyPolicy = db.CompanyPolicies.Find(id);
            if (companyPolicy == null)
            {
                return HttpNotFound();
            }
            return View(companyPolicy);
        }

        // POST: CompanyPolicies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PolicyId,InsuranceCompanyId")] CompanyPolicy companyPolicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyPolicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyPolicy);
        }

        // GET: CompanyPolicies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyPolicy companyPolicy = db.CompanyPolicies.Find(id);
            if (companyPolicy == null)
            {
                return HttpNotFound();
            }
            return View(companyPolicy);
        }

        // POST: CompanyPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CompanyPolicy companyPolicy = db.CompanyPolicies.Find(id);
            db.CompanyPolicies.Remove(companyPolicy);
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
