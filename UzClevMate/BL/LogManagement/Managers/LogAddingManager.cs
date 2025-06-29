using System;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.LogManagement.Models;

namespace UzClevMate.BL.LogManagement.Managers
{
    public static class LogAddingManager
    {
        private static readonly object locker = new object();

        public static void AddNewLog(this string log, string userId = null)
        {
            var logRecord = new LogRecord();
            if (userId != null)
            {
                logRecord.UserId = userId;
            }
            logRecord.Log = log;
            logRecord.Logtype = LogTypeEnum.normal;

            lock (locker)
            {
                LogWrtingManager.LogQueue.Add(logRecord);
            }
        }

        public static void AddDataToLog(this LogRecord log, string message)
        {
            log.LogStrings.Add(new LogString(message));
        }

        public static void AddExceptionToLog(this LogRecord log, string message)
        {
            log.Logtype = LogTypeEnum.error;
            log.LogStrings.Add(new LogString(message));
        }

        public static void AddLogToDb(this LogRecord log)
        {
            if (!log.LogStrings.HasElements())
            {
                return;
            }

            log.ConvertLogToJsonString();
            lock (locker)
            {
                LogWrtingManager.LogQueue.Add(log);
                if (log.IsCritical && log.Logtype == LogTypeEnum.error)
                {
                    //TODO: send warning email to admin Debug.WriteLine(e.Message);
                    //EmailManager.SendCommonTechnicalServiceMail("Critical error occured", $"Take a closer look. Data: {log.Log}");
                }
            }
        }

        public static void AddException(this Exception ex)
        {
            var logRecord = new LogRecord();
            logRecord.Log = ex.ToString();
            logRecord.Logtype = LogTypeEnum.error;

            lock (locker)
            {
                LogWrtingManager.LogQueue.Add(logRecord);
            }
        }
    }
}