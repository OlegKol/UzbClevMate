using System.Configuration;
using System.Linq;

namespace UzClevMate._Common.Extensions
{
    public static class RoleExtensions
    {
        public static bool IsAdmin(this string email)
        {
            return ConfigurationManager.AppSettings["Admins"].Split().ToList().Any(x => x.ToLower() == email);
        }

        public static bool IsExpert(this string email)
        {
            return ConfigurationManager.AppSettings["Experts"].Split().ToList().Any(x => x.ToLower() == email);
        }
    }
}