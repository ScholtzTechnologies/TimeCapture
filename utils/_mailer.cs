using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace TimeCapture.utils
{
    public class _mailer
    {
        public void SendMail(string sSubject, string sBody, string sTo, string sCC = "")
        {
            bool isValid = true;
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

            List<string> lAddressErrors = new List<string>();
            if (lTo.Length > 0 && !String.IsNullOrWhiteSpace(sTo))
            {
                foreach (string to in lTo)
                {
                    try
                    {
                        Mail.To.Add(new MailboxAddress(to.Split("@")[0].ToString().Replace(" ", ""), to));
                    }
                    catch
                    { lAddressErrors.Add(to); }
                }
                if (lAddressErrors.Count > 0)
                {
                    StringBuilder sbError = new StringBuilder();
                    sbError.AppendLine("Errors adding the following mails, please check or remove as required.");
                    foreach (string to in lAddressErrors)
                    {
                        sbError.AppendLine($" - {to}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please add a recipient for the mail.", "Mail error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (String.IsNullOrWhiteSpace(sSubject))
            {
                MessageBox.Show("Please add a subject for the mail.", "Mail error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }
            if (String.IsNullOrWhiteSpace(sBody))
            {
                MessageBox.Show("Please add a body for the mail.", "Mail error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
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
            if (isValid)
            {
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
            else
                throw new BadDataException("Mail fields", null, null);
        }
    }
}
