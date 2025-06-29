using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UzClevMate.BL.UzClevMateUsers._Common.Models
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public override ICollection<IdentityUserClaim> Claims => base.Claims;

        [JsonIgnore]
        public override ICollection<IdentityUserLogin> Logins => base.Logins;

        [JsonIgnore]
        public override ICollection<IdentityUserRole> Roles => base.Roles;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}