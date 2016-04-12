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
using iTextSharp;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Web.UI;
using iTextSharp.text.html.simpleparser;

namespace Insure.Web.Controllers
{
    public class PolicyController : Controller
    {
        private DataContext db = new DataContext();

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

        public byte[] GetPDF(string pHTML)
        {
            byte[] bPDF = null;

            MemoryStream ms = new MemoryStream();
            TextReader txtReader = new StringReader(pHTML);

            // 1: create object of a itextsharp document class
            Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

            // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

            // 3: we create a worker parse the document
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            // 4: we open document and start the worker on the document
            doc.Open();
            htmlWorker.StartDocument();

            // 5: parse the html into the document
            htmlWorker.Parse(txtReader);

            // 6: close the document and the worker
            htmlWorker.EndDocument();
            htmlWorker.Close();
            doc.Close();

            bPDF = ms.ToArray();

            return bPDF;
        }

        public void DownloadPDF()
        {
            string HTMLContent = "Hello <b>World</b>";

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfile.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(GetPDF(HTMLContent));
            Response.End();
        }
    }
}
