using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using UzClevMate.BL.LogManagement.Models;

namespace UzClevMate.MvcLogic._Common.Controllers
{
    public class _BaseController : Controller
    {
        public LogRecord LogRecord { get; set; } = new LogRecord();

        public _BaseController()
        {
            var userId = User?.Identity?.GetUserId();
            if (userId != null)
            {
                LogRecord.UserId = userId;
            }
        }
    }
}