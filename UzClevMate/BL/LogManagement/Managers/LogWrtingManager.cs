using System;
using System.Collections.Generic;
using System.Linq;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.LogManagement.Models;

namespace UzClevMate.BL.LogManagement.Managers
{
    public class LogWrtingManager
    {
        public static List<LogRecord> LogQueue = new List<LogRecord>();

        public static void WriteLogs()
        {
            try
            {
                var recordsToLog = LogQueue.Take(300);
                if (!recordsToLog.HasElements())
                {
                    return;
                }
                using (var db = new LogRecordDbContext())
                {
                    db.Logs.AddRange(recordsToLog);
                    db.SaveChanges();
                    LogQueue.RemoveRange(0, recordsToLog.Count());
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                //TODO: send warning email to admin Debug.WriteLine(e.Message);
            }
        }
    }
}