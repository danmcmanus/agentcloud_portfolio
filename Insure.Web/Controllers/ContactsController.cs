using Salesforce.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Insure.Web.Models.Salesforce1;
using Insure.Web.Salesforce1;
using Insure.Web.Models;

namespace Insure.Web.Controllers
{
    public class ContactsController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Convert(User user)
        {
            return View();
        }
        // Note: the SOQL Field list, and Binding Property list have subtle differences as custom properties may be mapped with the JsonProperty attribute to remove __c
        const string _ContactsPostBinding = "Id,Salutation,FirstName,LastName,MailingStreet,MailingCity,MailingState,MailingPostalCode,MailingCountry,Phone,Email";
        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Contact> contacts =
                            await client.QueryAsync<Contact>("SELECT Id, Salutation, FirstName, LastName, MailingCity, MailingState, MailingCountry From Contact");

                        return contacts.Records;
                    }
                    );
                foreach (var item in selectedContacts)
                {
                    db.Contacts.Add(item);
                }
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts);
        }

        public async Task<ActionResult> Details(string id)
        {
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Contact> contacts =
                            await client.QueryAsync<Contact>("SELECT Id, Salutation, FirstName, LastName, MailingStreet, MailingCity, MailingState, MailingPostalCode, MailingCountry, Phone, Email From Contact Where Id = '" + id + "'");
                        return contacts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Salesforce Contacts Details";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts.FirstOrDefault());
        }

        public async Task<ActionResult> Edit(string id)
        {
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Contact> contacts =
                            await client.QueryAsync<Contact>("SELECT Id, Salutation, FirstName, LastName, MailingStreet, MailingCity, MailingState, MailingPostalCode, MailingCountry, Phone, Email From Contact Where Id= '" + id + "'");
                        return contacts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedContacts.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = _ContactsPostBinding)] Contact contact)
        {
            SuccessResponse success = new SuccessResponse();
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.UpdateAsync("Contact", contact.Id, contact);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Contact";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (success.Success == "true")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(contact);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Contact> selectedContacts = Enumerable.Empty<Contact>();
            try
            {
                selectedContacts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                async (client) =>
                {
                    // Query the properties you'll display for the user to confirm they wish to delete this Contact
                    QueryResult<Contact> contacts =
                        await client.QueryAsync<Contact>(string.Format("SELECT Id, FirstName, LastName, MailingCity, MailingState, MailingCountry From Contact Where Id='{0}'", id));
                    return contacts.Records;
                }
                );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (selectedContacts.Count() == 0)
            {
                return View();
            }
            else
            {
                return View(selectedContacts.FirstOrDefault());
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            bool success = false;
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.DeleteAsync("Contact", id);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Delete Salesforce Contacts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = _ContactsPostBinding)] Contact contact)
        {
            String id = String.Empty;
            try
            {
                id = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        return await client.CreateAsync("Contact", contact);
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Create Salesforce Contact";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (this.ViewBag.ErrorMessage == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(contact);
            }
        }
    }
}