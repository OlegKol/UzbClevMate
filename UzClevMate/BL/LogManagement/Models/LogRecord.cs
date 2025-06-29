using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using UzClevMate._Common.Extensions;

namespace UzClevMate.BL.LogManagement.Models
{
    public class LogRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public LogMethodNameEnum MethodName { get; set; } = LogMethodNameEnum.common;

        public string LogMethodNameEnumStr => AttributeExtensions.GetDisplayAttribute(MethodName);

        public DateTime Date { get; set; } = DateTime.Now;

        public LogTypeEnum Logtype { get; set; } = LogTypeEnum.normal;

        public bool IsError => Logtype == LogTypeEnum.error;

        public bool IsCritical { get; set; }

        [NotMapped]
        public List<LogString> LogStrings { get; set; } = new List<LogString>();

        public string Log { get; set; }

        public string DateStr => Date.ToString(_Definitions.DefaultDateFormat + " HH:mm");

        public string LogtypeStr => Logtype.ToString();

        public LogRecord()
        {

        }

        public LogRecord(LogMethodNameEnum name, bool isCritical = false)
        {
            MethodName = name;
            IsCritical = isCritical;
        }

        public LogRecord(string userId, LogMethodNameEnum name = LogMethodNameEnum.common,
            bool isCritical = false)
        {
            MethodName = name;
            UserId = userId;
            IsCritical = isCritical;
        }

        public void ConvertLogToJsonString()
        {
            Log = JsonConvert.SerializeObject(LogStrings);
        }
    }

    public class LogRecordDbContext : DbContext
    {
        public LogRecordDbContext()
            : base("LogConnection")
        {
        }

        public DbSet<LogRecord> Logs { get; set; }
    }
}