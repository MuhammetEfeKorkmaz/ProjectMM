using System.Net.Mail;

namespace FullSharedCore.Utilities.EmailSender.Models
{
    public class MailSenderModel
    {
        public List<string> Attachments { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }


        /// <summary>
        /// 1=>Developer, 2=>Firma Yetkilisi, 3=>Son Kullanıcı
        /// </summary>
        public int MailTipi { get; set; } 
        public MailPriority MailPriority { 
            get 
            {
                if (MailTipi==1 | MailTipi == 2)
                    return MailPriority.High;
                return MailPriority.Normal;
            } }
    }
}
