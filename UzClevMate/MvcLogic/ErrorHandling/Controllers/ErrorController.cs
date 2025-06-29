using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UzClevMate.MvcLogic._Common.Controllers;

namespace UzClevMate.MvcLogic.ErrorHandling.Controllers
{
    public class ErrorController : _BaseController
    {
        public ActionResult Index()
        {
            return View("~/MvcLogic/ErrorHandling/Views/Error.cshtml");
        }
    }
}