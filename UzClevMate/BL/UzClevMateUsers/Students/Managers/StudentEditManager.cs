using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UzClevMate.BL.UzClevMateUsers.Students.Models;

namespace UzClevMate.BL.UzClevMateUsers.Students.Managers
{
    public class StudentEditManager
    {
        internal static void CreateStudent(Student student)
        {
            //student.StudentSettings = new StudentSettings();
            using (var db = new StudentDbContext())
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
        }

        internal static void SetLastLogin(string userId)
        {
            using (var db = new StudentDbContext())
            {
                var student = db.Students.FirstOrDefault(x => x.UserId == userId);
                if (student != null)
                {
                    student.LastLogin = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }
    }
}