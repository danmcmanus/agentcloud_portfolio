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
using PagedList;
using System.IO;
using System.Web.UI;
using SelectPdf;
using Insure.Web.Logic;
using Insure.Web.ViewModels;


namespace Insure.Web.Controllers
{
    public class PolicyController : Controller
    {

        private FilesContext db = new FilesContext();


        
        public ActionResult Index(string SortOrder, string currentFilter, string searchstring, int? page)
        {
            ViewBag.CurrentSort = SortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.PremiumSortParm = SortOrder == "Premium" ? "prem_desc" : "Premium";

            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = currentFilter;
            }
            ViewBag.CurrentFilter = searchstring;

            var policies = from p in db.Policies
                           select p;
            if (!String.IsNullOrEmpty(searchstring))
            {
                policies = policies.Where(p => p.Company.Name.Contains(searchstring));
            }
            switch (SortOrder)
            {
                case "name_desc":
                    policies = policies.OrderByDescending(p => p.Company.Name);
                    break;
                case "prem_desc":
                    policies = policies.OrderByDescending(p => p.Premium);
                    break;
                case "Premium":
                    policies = policies.OrderBy(p => p.Premium);
                    break;
                default:
                    policies = policies.OrderBy(p => p.Company.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(policies.ToPagedList(pageNumber,pageSize));
        }

        ////Default Index() GET: Policy
        //public ActionResult Index()
        //{
        //    var policies = db.Policies.Include(p => p.Company).Include(p => p.AppUser);
            
        //    return View(policies.ToList());
        //}

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
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "FullName");  //FirstName => FullName
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
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "FullName", policy.AppUserId);



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
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "FullName", policy.AppUserId);
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
            ViewBag.UserId = new SelectList(db.AppUsers, "Id", "FullName", policy.AppUserId);
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
        [HttpPost]
        public ActionResult Convert(FormCollection collection)
        {
            PdfHelper.ConversionUrl = "http://localhost:22032/Policy";
            var conversion = PdfHelper.Convert(collection);
            return conversion;
        }

        //[HttpPost]
        //public ActionResult Convert(FormCollection collection)
        //{

        //    HtmlToPdf converter = new HtmlToPdf();

        //    converter.Options.PdfPageSize = PdfPageSize.A4;
        //    converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
        //    converter.Options.WebPageWidth = 1202;
        //    converter.Options.WebPageHeight = 0;
        //    PdfDocument doc = converter.ConvertUrl("http://localhost:22032/Policy");

        //    byte[] pdf = doc.Save();
        //    doc.Close();

        //    FileResult fileResult = new FileContentResult(pdf, "application/pdf");
        //    fileResult.FileDownloadName = "Document.pdf";
        //    return fileResult;
        //}
    }
}
