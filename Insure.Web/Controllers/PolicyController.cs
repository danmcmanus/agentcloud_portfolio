using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Insure.Web.Models;
using Insure.Web.Helpers;

namespace Insure.Web.Controllers
{
    public class PolicyController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Policy
        public ActionResult Index()
        {
            var policies = db.Policies.Include(p => p.Company).Include(p => p.User);
            
            return View(policies.ToList());
        }

        // GET: Policy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Policy policy = db.Policies.Find(id);
            if (policy == null)
            {
                return HttpNotFound();
            }
            return View(policy);
        }

        // GET: Policy/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");  //FirstName => FullName
            return View();
        }

        // POST: Policy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Premium,Deductible,CoInsurance,CoPay,InsuranceType,CompanyId,UserId")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                db.Policies.Add(policy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", policy.CompanyId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", policy.UserId);
            return View(policy);
        }

        // GET: Policy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Policy policy = db.Policies.Find(id);
            if (policy == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", policy.CompanyId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", policy.UserId);
            return View(policy);
        }

        // POST: Policy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Premium,Deductible,CoInsurance,CoPay,InsuranceType,CompanyId,UserId")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(policy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", policy.CompanyId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", policy.UserId);
            return View(policy);
        }

        // GET: Policy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Policy policy = db.Policies.Find(id);
            if (policy == null)
            {
                return HttpNotFound();
            }
            return View(policy);
        }

        // POST: Policy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Policy policy = db.Policies.Find(id);
            db.Policies.Remove(policy);
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
