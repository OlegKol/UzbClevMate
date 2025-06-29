using System.Data.Entity;

namespace UzClevMate.BL.EntityFramwork.Context
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
            : base("DefaultConnection")
        {

        }
    }
}