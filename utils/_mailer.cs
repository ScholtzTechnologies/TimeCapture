using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace TimeCapture.utils
{
    public class _mailer
    {
        public void SendMail(string sSubject, string sBody, string sTo, string sCC = "")
        {
            MimeMessage Mail = new MimeMessage();

            Mail.From.Add(new MailboxAddress(_configuration.GetConfigValue("FromName"), _configuration.GetConfigValue("FromEmail")));

            string[] lTo;
            if (sTo.Contains(","))
                lTo = sTo.Split(",");
            else
                lTo = sTo.Split(";");

            string[] lCC;
            if (sCC.Contains(","))
                lCC = sCC.Split(",");
            else
                lCC = sCC.Split(";");

            if (lTo.Length > 0 && !String.IsNullOrWhiteSpace(sTo))
            {
                foreach (string to in lTo)
                {
                    Mail.To.Add(new MailboxAddress(to.Split("@")[0].ToString(), to));
                }
            }
            else
            {
                MessageBox.Show("Please add a a recipient for the mail.", "Mail error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (lCC.Length > 0 && !String.IsNullOrWhiteSpace(sCC))
            {
                foreach (string cc in lCC)
                {
                    Mail.Cc.Add(new MailboxAddress(cc.Split("@")[0].ToString(), cc));
                }
            }

            Mail.Subject = sSubject;
            Mail.Bcc.Add(new MailboxAddress("TimeCapture", "scholtz.web@gmail.com"));

            BodyBuilder bodyBuilder = new BodyBuilder();
            string sHtmlBody = sBody.EncaseMailBody();
            bodyBuilder.HtmlBody = sHtmlBody;
            Mail.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com", 587, false);
                string username = _configuration.GetConfigValue("FromEmail");
                string password = _configuration.GetConfigValue("MailPassword");
                client.Authenticate(username, password);
                client.Send(Mail);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
