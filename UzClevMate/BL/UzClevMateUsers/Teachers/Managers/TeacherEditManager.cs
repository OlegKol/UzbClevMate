using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UzClevMate.BL.UzClevMateUsers.Students.Managers;
using UzClevMate.BL.UzClevMateUsers.Students.Models;
using UzClevMate.BL.UzClevMateUsers.Teachers.Models;

namespace UzClevMate.BL.UzClevMateUsers.Teachers.Managers
{
    public class TeacherEditManager
    {
        internal static void CreateTeacher(Teacher teacher)
        {
            //teacher.TeacherSettings = new TeacherSettings();
            //teacher.TeacherPlanData = new TeacherPlanData();

            using (var db = new TeacherDbContext())
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
            }

            //AddTariffData(teacher.Id, Tariff.demo, false);
            //AddYourserfAsStudent(teacher);
        }

        private static void AddYourserfAsStudent(Teacher teacher)
        {
            var student = StudentGetManager.GetByEmail(teacher.Email);
            if (student == null)
            {
                student = new Student
                {
                    Email = teacher.Email,
                    Name = teacher.Name,
                    UserId = teacher.UserId,

                    IsTeacher = true
                };

                StudentEditManager.CreateStudent(student);
            }

            var message = "Это тестовый запрос. Чтобы увидеть платформу глазами ученика при составлении учебной программы, добавьте себя на курс. Для индивидуальных проверочных работ это не требуется";
            //StudentRequestEditManager.CreateTestStudentRequest(student.Id, message, teacher.Id);
        }

        internal static void SetLastLogin(string userId)
        {
            using (var db = new TeacherDbContext())
            {
                var teacher = db.Teachers
                    .FirstOrDefault(x => x.UserId == userId);

                if (teacher != null)
                {
                    teacher.LastLogin = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }
    }
}