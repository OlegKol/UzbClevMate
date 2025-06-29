using System;
using System.Web;
using System.Web.Mvc;

namespace UzClevMate.MvcLogic._Common.Extensions
{
    public static class CookieExtensions
    {
        public static string GetCookieValue(this Controller controller, string cookieName)
        {
            var cookie = controller.Request.Cookies[cookieName];
            return cookie != null 
                ? cookie.Value 
                : null;
        }

        public static void SetCookieValue(this Controller controller, string value, string cookieName)
        {
            var cookie = new HttpCookie(cookieName, value)
            {
                Expires = DateTime.Now.AddYears(1)
            };

            controller.Response.Cookies.Add(cookie);
        }
    }
}
