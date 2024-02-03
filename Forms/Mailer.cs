using TimeCapture.utils;
using Keys = System.Windows.Forms.Keys;

namespace TimeCapture.Forms
{
    public partial class Mailer : Form
    {
        public TimeCapture oTime { get; set; }
        public Access Access = new Access();
        public Dictionary<string, List<string>> dTables = new Dictionary<string, List<string>>();
        public List<ProgressBar> progressBars = new List<ProgressBar>();
        public Status oStatus { get; set; }
        public bool isInitialized { get; set; }

        public Mailer(TimeCapture timeCapture)
        {
            InitializeComponent();
            isInitialized = true;
            oTime = timeCapture;

            SetTagTableComboBoxes();
            dgvTags.Enabled = false;
            dgvTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTags.CellValueChanged += DgvTags_CellValueChanged;

            progressBars.Add(this.roundedProgressBar1);
            progressBars.Add(this.roundedProgressBar2);
            progressBars.Add(this.roundedProgressBar3);
            progressBars.Add(this.roundedProgressBar4);

            foreach (var oBar in progressBars)
            {
                oBar.Maximum = 1;
            }

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
        

        private void DgvTags_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (dgvTags.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn &&
                    e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                // Get the TimeID value from the clicked row
                string sTable = dgvTags.Rows[e.RowIndex].GetDataGridViewStringValue("colTableName");

                colColumnName.DataSource = dTables[sTable];
            }
        }

        private void tglUseTags_CheckedChanged(object sender, EventArgs e)
        {
            if (tglUseTags.Checked)
                dgvTags.Enabled = true;
            else
                dgvTags.Enabled = false;
        }

        public void SetTagTableComboBoxes()
        {
            DataSet dsTables = Access.GetTablesAndColumns();
            List<string> lTables = new List<string>();
            List<string> lColumns = new List<string>();

            if (dsTables.HasRows())
            {
                foreach (DataRow drTable in dsTables.Tables[0].Rows)
                {
                    string sTableName = drTable.GetDataRowStringValue("Table");
                    lTables.Add(sTableName);
                    lColumns = drTable.GetDataRowStringValue("Columns").Split(',').ToList<string>();
                    dTables.Add(sTableName, lColumns);
                }

                colTableName.DataSource = lTables;
                colColumnName.DataSource = dTables.Values.FirstOrDefault();
            }
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
        }

        public async Task SendMail(string sTo, string sSubject, string sBody, string sCC, bool isSendSeperate, bool isUseTags)
        {
            await Task.Run(() =>
            {
                StartLoading();
                

                if (isUseTags && dgvTags.Rows.Count > 0)
                {
                    //foreach (DataGridViewRow row in dgvTags.Rows)
                    //{
                    //    string sTable = row.GetDataGridViewStringValue("colTableName");
                    //    string sColumn = row.GetDataGridViewStringValue("colColumnName");
                    //    string Response = Access.ExecuteQuery($"SELECT {sColumn} from {sTable}");
                    //}
                    MessageBox.Show("Tags have not been fully implemented yet");
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
                    StopLoading(true);
                    MessageBox.Show("Mail(s) sent!", "Mail Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    StopLoading(false);
                    MessageBox.Show("Failed to send mail(s)", "Mail Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            });
        }

        public async Task StartLoading()
        {
            oStatus = Status.Busy;

            await Task.Run(() =>
            {
                int i = 0;
                ProgressBar bar1 = progressBars[0],
                       bar2 = progressBars[1],
                       bar3 = progressBars[2],
                       bar4 = progressBars[3];

                while (oStatus == Status.Busy)
                {

                    for (i = 0; i < progressBars.Count; i++)
                    {
                        try
                        {
                            progressBars[i - 1].Invoke((MethodInvoker)delegate
                            {
                                progressBars[i - 1].Value = 0;
                            });
                        }
                        catch
                        {
                            progressBars.LastOrDefault().Invoke((MethodInvoker)delegate
                            {
                                progressBars.LastOrDefault().Value = 0;
                            });
                        }

                        progressBars[i].Invoke((MethodInvoker)delegate
                        {
                            progressBars[i].Value = 1;
                        });
                        Thread.Sleep(750);
                    }
                }

                if (oStatus == Status.Idle)
                {
                    foreach (var oBar in progressBars)
                    {
                        oBar.Invoke((MethodInvoker)delegate
                        {
                            oBar.Value = 1;
                        });
                    }

                    Thread.Sleep(1000);

                    foreach (var oBar in progressBars)
                    {
                        oBar.Invoke((MethodInvoker)delegate
                        {
                            oBar.Value = 0;
                        });
                    }
                }
                else
                {
                    foreach (var oBar in progressBars)
                    {
                        oBar.Invoke((MethodInvoker)delegate
                        {
                            oBar.Value = 0;
                        });
                    }
                }
            });
        }

        public void StopLoading(bool isSuccess)
        {
            oStatus = isSuccess ? Status.Idle : Status.Error;
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
