using System.Net.Mail;

namespace FullSharedCore.Utilities.EmailSender.Models
{
    public class MailSenderConfigModel
    {
        public string From { get; set; } 
        public List<string> Tos { get; set; }
        public List<string> CCs { get; set; }
        public List<string> BCCs { get; set; }
        public List<string> Attachments { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public bool EnableSsl { get; set; }
        public bool IsBodyHtml { get; set; }
        public MailPriority MailPriority_ { get; set; }
         
    }
}
