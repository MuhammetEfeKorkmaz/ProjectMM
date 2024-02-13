using FullSharedCore.Utilities.EmailSender.Models;

namespace FullSharedCore.Utilities.EmailSender.Abstract
{
    public interface IMailSender
    {
        public string Sender(MailSenderModel model);
    }
}
