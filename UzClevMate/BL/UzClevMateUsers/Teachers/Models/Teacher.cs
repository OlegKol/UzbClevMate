using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.EntityFramwork.Context;

namespace UzClevMate.BL.UzClevMateUsers.Teachers.Models
{
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string TeacherCode { get; set; } = GeneralExtensions.GetGuid();

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public DateTime LastLogin { get; set; } = DateTime.UtcNow;

        public string Culture { get; set; } = _Definitions.DefaultCulture;
    }

    public class TeacherDbContext : BaseDbContext
    {
        public TeacherDbContext()
            : base()
        { }

        public DbSet<Teacher> Teachers { get; set; }
    }
}