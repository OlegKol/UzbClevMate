using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNet.Identity;
using MimeKit;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.Emails.EmailSending.Models;

namespace UzClevMate.BL.Emails.EmailSending.Managers
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var from = ConfigurationManager.AppSettings["EmailFrom"];
            var password = ConfigurationManager.AppSettings["EmailPassword"];
            var userName = ConfigurationManager.AppSettings["EmailUserName"];
            var port = ConfigurationManager.AppSettings["EmailPort"].ToInt32();
            var host = ConfigurationManager.AppSettings["EmailHost"];
            var enableSsl = ConfigurationManager.AppSettings["EmailEnableSsl"].ToBool();

            var credentials = new NetworkCredential(from, password);
            var parameters = new EmailServerParameters(host, port, enableSsl, credentials);
            var mailbox = new MailboxAddress(userName, from);

            var (success, emailLog) = Send(message, mailbox, parameters);

            return Task.FromResult(0);
        }

        private (bool success, string log) Send(IdentityMessage mailData,
            MailboxAddress from,
            EmailServerParameters serverParameters)
        {
            using (var stream = new MemoryStream())
            {
                var fullLog = new StringBuilder();
                var logger = new ProtocolLogger(stream);
                try
                {
                    var message = new MimeMessage();
                    message.From.Add(from);
                    message.To.Add(new MailboxAddress(mailData.Destination, mailData.Destination));

                    message.Subject = mailData.Subject;

                    var multipart = new Multipart("mixed");
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = mailData.Body;

                    multipart.Add(bodyBuilder.ToMessageBody());

                    message.Body = multipart;

                    using (var client = new SmtpClient(logger))
                    {
                        var secureSocketOptions = serverParameters.EnableSsl
                                ? SecureSocketOptions.SslOnConnect
                                : SecureSocketOptions.None; //serverParameters.EnableSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.None;

                        client.Connect(serverParameters.Host, serverParameters.Port, secureSocketOptions);
                        client.Authenticate(serverParameters.Credentials);

                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception e)
                {
                    fullLog.Append(e.ToString());

                    var streamArray = stream.ToArray();
                    fullLog.Append(Encoding.UTF8.GetString(streamArray, 0, streamArray.Length));
                    return (false, fullLog.ToString());
                }

                var array = stream.ToArray();
                fullLog.Append(Encoding.UTF8.GetString(array, 0, array.Length));
                return (true, fullLog.ToString());
            }
        }
    }
}