using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UzClevMate.BL.UzClevMateUsers.Students.Models;

namespace UzClevMate.BL.UzClevMateUsers.Students.Managers
{
    public class StudentGetManager
    {
        internal static Student GetByEmail(string email)
        {
            using (var db = new StudentDbContext())
            {
                return db.Students.FirstOrDefault(x => x.Email == email);
            }
        }
    }
}