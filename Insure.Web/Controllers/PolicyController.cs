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
        List<Policy> policiesToCompare = new List<Policy>();
        private DataContext db = new DataContext();
        Policy policy = new Policy();
        public List<Policy> IncludeInComparison(int? id)
        {
            var pol = db.Policies.Find(id);
            policiesToCompare.Add(pol);
                
                
            return policiesToCompare;
        }
        
        public ActionResult Calculate(int? id )
        {
            List<Policy> policies = IncludeInComparison(id);
            return View(policies);
        }
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(policies.ToPagedList(pageNumber,pageSize));
        }

        ////Default Index() GET: Policy
        //public ActionResult Index()
        //{
        //    var policies = db.Policies.Include(p => p.Company).Include(p => p.User);
            
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


        [HttpPost]
        public ActionResult Convert(FormCollection collection)
        {
            // read parameters from the webpage
            string url = collection["TxtUrl"];

            string pdf_page_size = collection["DdlPageSize"];
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), pdf_page_size, true);

            string pdf_orientation = collection["DdlPageOrientation"];
            PdfPageOrientation pdfOrientation = (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation), pdf_orientation, true);

            int webPageWidth = 1024;
            try
            {
                webPageWidth = System.Convert.ToInt32(collection["TxtWidth"]);
            }
            catch { }

            int webPageHeight = 0;
            try
            {
                webPageHeight = System.Convert.ToInt32(collection["TxtHeight"]);
            }
            catch { }

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            byte[] pdf = doc.Save();

            // close pdf document
            doc.Close();

            // return resulted pdf document
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Document.pdf";
            return fileResult;
        }
    }
}
