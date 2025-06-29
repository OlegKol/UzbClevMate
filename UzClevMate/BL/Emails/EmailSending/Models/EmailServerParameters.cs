using System.Net;

namespace UzClevMate.BL.Emails.EmailSending.Models
{
    public class EmailServerParameters
    {
        public string Host { get; }
        public int Port { get; }
        public bool EnableSsl { get; }
        public NetworkCredential Credentials { get; }

        public EmailServerParameters(string host, int port, bool enableSsl, NetworkCredential credentials)
        {
            Host = host;
            Port = port;
            EnableSsl = enableSsl;
            Credentials = credentials;
        }
    }
}