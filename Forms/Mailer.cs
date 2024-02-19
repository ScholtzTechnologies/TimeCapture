using TimeCapture.utils;
using Keys = System.Windows.Forms.Keys;

namespace TimeCapture.Forms
{
    public partial class Mailer : Form
    {
        public TimeCapture oTime { get; set; }
        public Access Access = new Access();
        public Dictionary<string, List<string>> dTables = new Dictionary<string, List<string>>();
        public Status oStatus { get; set; }
        public bool isInitialized { get; set; }

        public Mailer(TimeCapture timeCapture)
        {
            InitializeComponent();
            isInitialized = true;
            oTime = timeCapture;

            dgvTags.Enabled = false;
            dgvTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            rtbBody.KeyDown += RtbBody_KeyDown;
        }

        private void RtbBody_KeyDown(object? sender, KeyEventArgs e)
        {
            int selectionStart = rtbBody.SelectionStart;
            string sText = rtbBody.Text;
            string sSelected = rtbBody.SelectedText;
            if (String.IsNullOrWhiteSpace(sSelected))
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
                    rtbBody.Text = sText.Insert(selectionStart, "<div> </div>");
                else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.T)
                    rtbBody.Text = sText.Insert(selectionStart, "<table><tr><td> </td></tr></table>");
                else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.B)
                    rtbBody.Text = sText.Insert(selectionStart, "<b> </b>");
            }
            else
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
                    rtbBody.SelectedText = $"<div> {sSelected} </div>";
                else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.T)
                    rtbBody.SelectedText = sSelected.ToHTMLTable();
                else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.B)
                    rtbBody.SelectedText = $"<b> {sSelected} </b>";
            }
        }
       

        private void tglUseTags_CheckedChanged(object sender, EventArgs e)
        {
            if (tglUseTags.Checked)
                dgvTags.Enabled = true;
            else
                dgvTags.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string sTo = rtbTo.Text.ToString(),
                    sSubject = txtSubject.Text.ToString(),
                    sBody = rtbBody.Text.ToString().Replace("\n", "<br>"),
                    sCC = txtCC.Text.ToString();

            bool isSendSeperate = tglSeperateMails.Checked,
                isUseTags = tglUseTags.Checked;
            SendMail(sTo, sSubject, sBody, sCC, isSendSeperate, isUseTags);
            //new GraphMail().SendEmail(sSubject, sBody, sTo, sCC);
        }

        public async Task SendMail(string sTo, string sSubject, string sBody, string sCC, bool isSendSeperate, bool isUseTags)
        {
            await Task.Run(() =>
            {
                Loader.Start(this);

                if (isUseTags && dgvTags.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvTags.Rows)
                    {
                        string sName = row.GetDataGridViewStringValue("colTagName");
                        string sSQL = row.GetDataGridViewStringValue("colSQL");

                        if (!sSQL.ToLower().Contains("delete") && !sSQL.ToLower().Contains("insert"))
                        {
                            if (!sName.isNullOrEmpty() && !sSQL.isNullOrEmpty())
                            {
                                try
                                {
                                    DataSet dsResponse = Access.ExecuteQuery(sSQL);
                                    if (dsResponse.HasRows())
                                    {
                                        sBody = sBody.Replace(sName, dsResponse.Tables[0].Rows[0][0].ToString());
                                        sSubject = sSubject.Replace(sName, dsResponse.Tables[0].Rows[0][0].ToString());
                                    }
                                    else
                                        new Notifications().SendNotification($"No Results returned for tag: {sName}", NotificationType.Error, "Please ensure tag is correct");
                                }
                                catch (Exception exception)
                                {
                                    new Notifications().SendNotification("Send mail failed.", NotificationType.Error, $"Error: \n {exception}");
                                }
                            }
                        }
                        else
                        {
                            new Notifications().SendNotification($"Query for {sName} contains potential security risks.", NotificationType.Error);
                        }
                    }
                }

                try
                {
                    if (isSendSeperate)
                    {
                        string[] lTo;
                        if (sTo.Contains(","))
                            lTo = sTo.Split(",");
                        else
                            lTo = sTo.Split(";");

                        foreach (string to in lTo)
                        {
                            new _mailer().SendMail(sSubject, sBody, to, sCC);
                        }
                    }
                    else
                    {
                        new _mailer().SendMail(sSubject, sBody, sTo, sCC);
                    }
                    Loader.Stop(true);
                    new Notifications().SendNotification("Mail(s) sent!", NotificationType.Success);
                }
                catch
                {
                    Loader.Stop(false);
                    new Notifications().SendNotification("Failed to send mail(s)", NotificationType.Success);
                }
                return;
            });
        }

        private void lnkHTMLHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.AppendLine("Html Shortcuts are enabled:");
            sbText.AppendLine("- Ctrl + D adds a <div>");
            sbText.AppendLine("- Ctrl + B adds a <b> (Bold)");
            sbText.AppendLine("- Ctrl + T adds a <table> with one row");
            sbText.AppendLine("     When adding tables with selected rows, note that whole tables can be created. ");
            sbText.AppendLine("     By using: '1 Cell One 1 Cell Two 2 RowTwo 2 Row Two Cell two");
            sbText.AppendLine("     A table can be created with 2 cells and 2 rows, just by using numbers to seperate the rows");
            sbText.AppendLine("");
            sbText.AppendLine("The shortcuts can be used to create new tags or, can be used while selecting text to wrap that text");
            MessageBox.Show(sbText.ToString(), "HTML Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
