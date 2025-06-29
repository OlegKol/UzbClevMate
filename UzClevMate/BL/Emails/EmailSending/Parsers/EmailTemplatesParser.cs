using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.Emails.EmailSending.ViewModels;

namespace UzClevMate.BL.Emails.EmailSending.Parsers
{
    public class EmailTemplatesParser
    {
        internal static string ParseEmail(List<string> paragraphs, string name, string userId, bool isForTeacher)
        {
            var folderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/_Common/Emails/EmailSending/Images");
            var image = GetRandomImage(folderPath);

            var emailModel = new CommonEmailModel
            {
                ImageUrl = image,
                UserId = userId,
                Name = $"Приветствуем, {name}",
                EmailParagraphs = paragraphs
            };

            if (isForTeacher)
            {
                emailModel.UnsubscribeLink = "TeacherNotification/Index";
            }
            else
            {
                emailModel.UnsubscribeLink = "StudentNotification/Index";
            }

            return ParseEmailBody(emailModel);
        }

        public static string GetRandomImage()
        {
            try
            {
                var folderPath = HttpContext.Current.Server.MapPath("~/BL/Emails/EmailSending/Images/Images");
                var fileNames = Directory.GetFiles(folderPath);
                var file = fileNames.ToList().GetRandomFromList();
                return Path.GetFileName(file);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetRandomImage(string folderPath)
        {
            try
            {
                var fileNames = Directory.GetFiles(folderPath);
                var file = fileNames.ToList().GetRandomFromList();
                return Path.GetFileName(file);
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static string ParseEmailBody(CommonEmailModel model)
        {
            var template = EmailResources.EmailResources.email_template;

            var tableContent = GetTableContent(model);
            var body = Regex.Replace(template, "#tablecontent", tableContent);
            body = Regex.Replace(body, "#name", model.Name);
            body = Regex.Replace(body, "#image", model.ImageUrl);
            body = Regex.Replace(body, "#userid", model.UserId);
            body = Regex.Replace(body, "#unsubscribelink", model.UnsubscribeLink);

            return body;
        }

        private static string GetTableContent(CommonEmailModel model)
        {
            var stringBuilder = new StringBuilder();
            foreach (var emailParagraph in model.EmailParagraphs)
            {
                var line = $"<tr><td align=\"left\" style=\"padding-bottom:20px;\">{emailParagraph}</td></tr>";
                stringBuilder.AppendLine(line);
            }
            var tableContent = stringBuilder.ToString();
            return tableContent;
        }
    }
}