using System;
using System.Collections.Generic;
using System.Linq;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.LogManagement.Models;

namespace UzClevMate.MvcLogic.AdminLogic.Logs.Managers
{
    public class LogGettingManager
    {
        internal static List<LogRecord> GetLogs(string userId, DateTime dateStart, DateTime dateEnd,
            bool onlyErrors, LogMethodNameEnum methodName = LogMethodNameEnum.common)
        {
            var result = new List<LogRecord>();

            using (var db = new LogRecordDbContext())
            {
                IQueryable<LogRecord> query = db.Logs
                    .Where(l => l.Date >= dateStart && l.Date <= dateEnd);

                if (userId.HasValue())
                {
                    query = query.Where(l => l.UserId == userId);
                }
                if (onlyErrors)
                {
                    query = query.Where(l => l.Logtype == LogTypeEnum.error);
                }
                if (methodName != LogMethodNameEnum.common)
                {
                    query = query.Where(l => l.MethodName == methodName);
                }

                query = query.Take(200);

                result = query.ToList();
            }

            return result;
        }
    }
}