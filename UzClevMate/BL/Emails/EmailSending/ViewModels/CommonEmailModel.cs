using System.Collections.Generic;

namespace UzClevMate.BL.Emails.EmailSending.ViewModels
{
    public class CommonEmailModel
    {
        public string HelloPhrase { get; set; }

        public string FooterPhrase { get; set; }

        public string EmailSenderName { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public string UnsubscribeLink { get; set; } = "EmailUnsubscribe/Index";

        public List<string> EmailParagraphs { get; set; } = new List<string>();
    }
}