using FullSharedCore.Utilities.EmailSender.Abstract;
using FullSharedCore.Utilities.EmailSender.Models;
using System.Net.Mail;

namespace FullSharedCore.Utilities.EmailSender.Concrete
{
    public class MailSender : IMailSender
    {
        private readonly List<MailSenderBaseModel> baseModels;
        public MailSender(List<MailSenderBaseModel> _baseModels)
        {
            baseModels = _baseModels;
        }
        public string Sender(MailSenderModel model)
        {
            try
            {
                var baseModel= baseModels.FirstOrDefault(x => x.MailTipi == model.MailTipi);


                SmtpClient smtpClient = new SmtpClient(baseModel.Host, baseModel.Port);
                smtpClient.Credentials = new System.Net.NetworkCredential(baseModel.UserName, baseModel.Password);
                // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = baseModel.EnableSsl;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(baseModel.From, baseModel.FromDisplayName);
                foreach (var item in baseModel.Tos)
                    mail.To.Add(new MailAddress(item));
                foreach (var item in baseModel.CCs)
                    mail.CC.Add(new MailAddress(item));
                foreach (var item in baseModel.BCCs)
                    mail.Bcc.Add(new MailAddress(item));


              
                mail.IsBodyHtml = baseModel.IsBodyHtml;
                mail.Body = model.Body;
                mail.Subject = model.Subject;
                mail.Priority = model.MailPriority;
                foreach (var item in model.Attachments)
                    mail.Attachments.Add(new Attachment(item));



                smtpClient.Send(mail);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
    }
}
