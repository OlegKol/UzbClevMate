using System;
using UzClevMate._Common.Extensions;

namespace UzClevMate.BL.LogManagement.Models
{
    public class LogString
    {
        public DateTime Date { get; set; } = DateTime.Now;

        public string DateStr => Date.ToString(_Definitions.DefaultDateFormat + " HH:mm:ss");

        public string Data { get; set; }

        public LogString()
        {

        }
        public LogString(string data)
        {
            Data = data;
        }
    }
}