using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Backload;
using Backload.Contracts.Context;
using Backload.Contracts.FileHandler;
using Backload.Helper;

namespace Insure.Web.Controllers
{
    public class FileHandlerController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post | HttpVerbs.Put | HttpVerbs.Delete | HttpVerbs.Options)]
        public async Task<ActionResult> FileHandler()
        {
            try
            {
                IFileHandler handler = Backload.FileHandler.Create();
                handler.Init(HttpContext.Request);

                IBackloadResult result = await handler.Execute(false);
                return ResultCreator.Create(result);
            }
            catch 
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError); 
            }
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}