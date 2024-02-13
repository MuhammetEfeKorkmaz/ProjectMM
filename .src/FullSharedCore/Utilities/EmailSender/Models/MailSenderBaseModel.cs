namespace FullSharedCore.Utilities.EmailSender.Models
{
    public class MailSenderBaseModel
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
         
        public string FromDisplayName { get; set; }
        public string From { get; set; }
        public List<string> Tos { get; set; }
        public List<string> CCs { get; set; }
        public List<string> BCCs { get; set; }
      
        public bool EnableSsl { get; set; }
        public bool IsBodyHtml { get; set; }
        public int MailTipi { get; set; } // 1=>Developer, 2=>Firma Yetkilisi, 3=>Son Kullanıcı

    }
}
