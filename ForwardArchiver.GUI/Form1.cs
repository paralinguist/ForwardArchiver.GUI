using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.Exchange.WebServices.Data;

namespace ForwardArchiver.GUI
{
    public partial class FrmMain : Form
    {
        public String settings_file = "fwdarch.json";
        public  String category = "Purple Category";
        public  int max_returned = 999;
        public  int received_backups = 0;
        public  int sent_backups = 0;
        public  int auth_server_fails = 0;
        public  bool archive_complete = false;
        public  bool needs_repeat = false;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void archive_mail_Click(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
            service.Credentials = new WebCredentials(department_email.Text, password.Text);
            service.Url = new Uri(exchange_URL.Text);

            FolderId inbox = WellKnownFolderName.Inbox;
            FolderId sent = WellKnownFolderName.SentItems;

            bool login_success = false;

            try
            {
                service.ConvertIds(new AlternateId[] { new AlternateId(IdFormat.HexEntryId, "00", department_email.Text) }, IdFormat.HexEntryId);
                login_success = true;
            }
            catch
            {
                feedback_log.Text += "Sign in failed - please check your department email and password are correct.\r\n";
            }

            while (!archive_complete && login_success)
            {
                needs_repeat = false;
                FindItemsResults<Item> unarchived_mail = FindMail(inbox, service);
                if (unarchived_mail != null)
                {
                    received_backups = ForwardMail(unarchived_mail, service, "received");
                }
                else
                {
                    needs_repeat = true;
                }

                unarchived_mail = FindMail(sent, service);
                if (unarchived_mail != null)
                {
                    sent_backups = ForwardMail(unarchived_mail, service, "sent");
                }
                else
                {
                    needs_repeat = true;
                }

                feedback_log.Text += "Received mail backed up: " + received_backups + "\r\n";
                feedback_log.Text += "Sent mail backed up: " + sent_backups + "\r\n";
                feedback_log.Text += "Auth server fails: " + auth_server_fails + "\r\n";
                if (needs_repeat)
                {
                    archive_complete = false;
                    feedback_log.Text += "Possibly missed some mail last time - re-running.\r\n";
                }
                else
                {
                    archive_complete = true;
                }
            }
            feedback_log.Text += "Done\r\n";
        }

        private FindItemsResults<Item> FindMail(FolderId location, ExchangeService service)
        {
            FindItemsResults<Item> unarchived_mail = null;
            try
            {
                SearchFilter.IsNotEqualTo category_test = new SearchFilter.IsNotEqualTo(EmailMessageSchema.Categories, category);
                SearchFilter sf = new SearchFilter.SearchFilterCollection(LogicalOperator.And, category_test);
                ItemView view = new ItemView(max_returned);
                unarchived_mail = service.FindItems(location, sf, view);
            }
            catch
            {
                feedback_log.Text += "Auth server fail\r\n";
                auth_server_fails++;

            }
            return unarchived_mail;
        }

        private int ForwardMail(FindItemsResults<Item> unarchived_mail, ExchangeService service, String tag)
        {
            int successful_forwards = 0;
            foreach (Item item in unarchived_mail)
            {
                try
                {
                    EmailMessage message = EmailMessage.Bind(service, item.Id);
                    ResponseMessage responseMessage = message.CreateForward();
                    responseMessage.ToRecipients.Add(gmail.Text);
                    responseMessage.BodyPrefix = "Archived:" + tag;
                    responseMessage.Send();
                    message.Categories.Add(category);
                    if (!message.IsRead)
                    {
                        message.IsRead = false;
                    }
                    message.Update(ConflictResolutionMode.AutoResolve, true);
                    successful_forwards++;
                }
                catch
                {
                    feedback_log.Text += "Auth server fail\r\n";
                    auth_server_fails++;
                }
            }
            return successful_forwards;
        }

        //The below functions are stolen wholesale from MSDN examples
        private bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        private bool CertificateValidationCallBack(
                            object sender,
                            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                            System.Security.Cryptography.X509Certificates.X509Chain chain,
                            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) &&
                           (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid. 
                            continue;
                        }
                        else
                        {
                            if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                return false;
                            }
                        }
                    }
                }

                // When processing reaches this line, the only errors in the certificate chain are 
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange server installations, so return true.
                return true;
            }
            else
            {
                // In all other cases, return false.
                return false;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FwdArchSettings settings = FwdArchSettings.Load(settings_file);
            department_email.Text = settings.exchange_address;
            gmail.Text = settings.gmail_address;
            exchange_URL.Text = settings.ews_url.ToString();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            FwdArchSettings settings = FwdArchSettings.Load(settings_file);
            settings.exchange_address = department_email.Text;
            settings.gmail_address = gmail.Text;
            settings.ews_url = new Uri(exchange_URL.Text);
            settings.Save(settings_file);
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            if (password.Text.Length > 0)
            {
                archive_mail.Enabled = true;
            }
            else
            {
                archive_mail.Enabled = false;
            }
        }
    }

    class FwdArchSettings : AppSettings<FwdArchSettings>
    {
        public String gmail_address = "firstname.surname@ashdalesc.wa.edu.au";
        public String exchange_address = "firstname.surname@education.wa.edu.au";
        public Uri ews_url = new Uri(@"https://webmail.det.wa.edu.au/EWS/Exchange.asmx");
        public String category = "Purple Category";
        public int max_returned = 999;
    }

    //Settings based on this excellent SO answer:
    //http://stackoverflow.com/a/6541739
    public class AppSettings<T> where T : new()
    {
        public void Save(string fileName)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }

        public static void Save(T pSettings, string fileName)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
        }

        public static T Load(string fileName)
        {
            T t = new T();
            if (File.Exists(fileName))
                t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
            return t;
        }
    }
}
//JDI: Assault (Madcap)