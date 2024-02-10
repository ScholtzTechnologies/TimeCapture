using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace TimeCapture.utils
{
    public class _mailer
    {
        public void SendMail(string sSubject, string sBody, string sTo, string sCC = "")
        {
            bool isValid = true;
            MimeMessage Mail = new MimeMessage();

            Mail.From.Add(new MailboxAddress(_configuration.GetConfigValue("FromName"), _configuration.GetConfigValue("FromEmail")));

            string[] lTo = null, lCC = null;

            if (!sTo.isNullOrEmpty())
            {
                if (sTo.Contains(","))
                {
                    lTo = sTo.Split(",");
                }
                else if (sTo.Contains(",") && sTo.Contains(";"))
                {
                    MessageBox.Show("Please use either ',' or ';' in the To field, not both.");
                    isValid = false;
                }
                else
                {
                    lTo = sTo.Split(";");
                }

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


            if (!String.IsNullOrWhiteSpace(sCC))
            {
                if (sCC.Contains(","))
                {
                    lCC = sCC.Split(",");
                }
                else if (sCC.Contains(",") && sCC.Contains(";"))
                {
                    MessageBox.Show("Please use either ',' or ';' in the CC field, not both.");
                    isValid = false;
                }
                else
                {
                    lCC = sCC.Split(";");
                }

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


    public class GraphMail
    {
        public void SendEmail(string sSubject, string sBody, string sTo, string sCC = "")
        {
            string GraphMailTenantId = "b2cc3901-d499-4ea3-aff2-26625de5addd", GraphMailClientId = "d167ec17-69ee-42a8-9627-778a77f1d515",
                GraphMailClientSecret = "QBb8Q~pN~6Te5N.8PZED440ZhLlMy53jsq~aEbyD", GraphMailUserId = "DemoCG@codeplex.co.za",
                GraphMailScope = "https://graph.microsoft.com/.default", GraphMailUrl = "https://login.microsoftonline.com/[[tenantId]]/v2.0";

            string[] scopes = new string[] { GraphMailScope };
            List<Recipient> oToRecipients = new List<Recipient>();

            Recipient recipient = new Recipient()
            {
                EmailAddress = new Microsoft.Graph.EmailAddress() { Address = sTo, Name = "Test" }
            };
            oToRecipients.Add(recipient);

            var message = new Microsoft.Graph.Message
            {
                Subject = sSubject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = sBody.EncaseMailBody()
                },
                ToRecipients = oToRecipients,
            };

            IConfidentialClientApplication confidentialClient = ConfidentialClientApplicationBuilder
                .Create(GraphMailClientId)
                .WithClientSecret(GraphMailClientSecret)
                .WithAuthority(new Uri(GraphMailUrl.Replace("[[tenantId]]", GraphMailTenantId)))
                .Build();

            // Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
            var oAuthResult = confidentialClient
                                .AcquireTokenForClient(scopes)
                                .ExecuteAsync().Result;

            var oToken = oAuthResult.AccessToken;

            // Build the Microsoft Graph client. As the authentication provider, set an async lambda
            // which uses the MSAL client to obtain an app-only access token to Microsoft Graph,
            // and inserts this access token in the Authorization header of each API request.
            GraphServiceClient oGraphServiceClient =
                new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                    // Add the access token in the Authorization header of the API request.
                    requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", oToken);
                })
                );

            oGraphServiceClient.Users[GraphMailUserId]
                               .SendMail(message, false)
                               .Request()
                               .PostAsync()
                               .Wait();
        }
    }
}
