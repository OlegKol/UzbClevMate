using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.EntityFramwork.Context;

namespace UzClevMate.BL.UzClevMateUsers.Students.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public DateTime LastLogin { get; set; } = DateTime.UtcNow;

        public bool IsTeacher { get; set; }

        public string Culture { get; set; } = _Definitions.DefaultCulture;

    }

    public class StudentDbContext : BaseDbContext
    {
        public StudentDbContext()
            : base()
        { }

        public DbSet<Student> Students { get; set; }

    }
}