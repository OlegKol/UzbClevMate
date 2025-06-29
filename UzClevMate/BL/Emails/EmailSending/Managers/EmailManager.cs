using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using UzClevMate._Common.Extensions;

namespace UzClevMate.BL.Emails.EmailSending.Managers
{
    public class EmailManager
    {
        private static List<string> GetTechnicalServiceEmails()
        {
            if (Debugger.IsAttached)
            {
                return new List<string>()
                {
                    _Definitions.DEFAULT_MAIL
                };
            }

            return !ConfigurationManager.AppSettings["TechnicalService"].HasValue()
                ? new List<string>() { _Definitions.DEFAULT_MAIL }
                : ConfigurationManager.AppSettings["TechnicalService"].Split().ToList();
        }

        private static List<string> GetFinancialServiceEmails()
        {
            if (Debugger.IsAttached)
            {
                return new List<string>()
                {
                    _Definitions.DEFAULT_MAIL
                };
            }
            return !ConfigurationManager.AppSettings["FinancialService"].HasValue()
                ? new List<string>() { _Definitions.DEFAULT_MAIL }
                : ConfigurationManager.AppSettings["FinancialService"].Split().ToList();
        }

        private static List<string> GetCustomerServiceEmails()
        {
            if (Debugger.IsAttached)
            {
                return new List<string>()
                {
                    _Definitions.DEFAULT_MAIL
                };
            }
            return !ConfigurationManager.AppSettings["CustomerService"].HasValue()
                ? new List<string>() { _Definitions.DEFAULT_MAIL }
                : ConfigurationManager.AppSettings["CustomerService"].Split().ToList();
        }

        internal static void SendCommonTechnicalServiceMail(string subject, string body)
        {
            var emails = GetTechnicalServiceEmails();
            var message = new IdentityMessage()
            {
                Subject = subject,
                Body = body
            };
            SendMails(message, emails);
        }

        internal static void SendCommonCustomerServiceMail(string subject, string body)
        {
            var emails = GetCustomerServiceEmails();
            var message = new IdentityMessage()
            {
                Subject = subject,
                Body = body
            };
            SendMails(message, emails);
        }

        internal static void SendCommonFinancialServiceMail(string subject, string body)
        {
            var emails = GetFinancialServiceEmails();
            var message = new IdentityMessage()
            {
                Subject = subject,
                Body = body
            };
            SendMails(message, emails);
        }

        private static void SendMails(IdentityMessage message, List<string> emails)
        {
            foreach (var email in emails)
            {
                message.Destination = email;
                new EmailService().SendAsync(message);
            }
        }
    }
}