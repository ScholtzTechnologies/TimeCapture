using TimeCapture.DB;
using TimeCapture.Forms;
using TimeCapture.utils;
using TimeCapture.Forms.Shared;

namespace TimeCapture
{
    public partial class TimeCapture : Form
    {
        #region Properties
        public DataTable tblTime { get; set; }
        public List<Time> lTime = new List<Time>();
        public List<CSVImport.Types> lTypes = new List<CSVImport.Types>();
        public List<CSVImport.Clients> lClients = new List<CSVImport.Clients>();
        public List<CSVImport.Tickets> lTickets = new List<CSVImport.Tickets>();
        public List<CSVImport.Settings> Settings = new List<CSVImport.Settings>();
        public DB.Access Access = new DB.Access();
        public static string root = Directory.GetCurrentDirectory();
        public static string SettingsCsv = Path.Combine(root, "Data", "Settings.csv");
        public System.Drawing.Point lblTypeLocal { get; set; }
        public System.Drawing.Point drpTypeLocal { get; set; }
        public System.Drawing.Size drpTypeSize { get; set; }
        public System.Drawing.Point lblDescLocal { get; set; }
        public System.Drawing.Point txtDescLocal { get; set; }
        public System.Drawing.Size txtDescSize { get; set; }
        #region priorTime
        public string ptName { get; set; }
        public int ptTicketNumber { get; set; }
        public string ptStart { get; set; }
        public DateTime dtStart { get; set; }
        public string ptTimeType { get; set; }
        public string ptTicketType { get; set; }
        public string ptDesc { get; set; }
        public int TimeID { get; set; }
        public bool isInitialized { get; set; }
        #endregion PriorTime

        public Selenium.TimeTaker.TimeCapture timeCapture = new();
        public _Spinner Spinner { get; set; }
        #endregion Properties
        
        public TimeCapture()
        {
            InitializeComponent();

            string response;
            new Access().TestConnection(out response);
            responseMessage.Text = response;

            CreateTable();
            getTypes();
            getTickets();

            lblTypeLocal = lblType.Location;
            drpTypeLocal = drpType.Location;
            drpTypeSize = drpType.Size;

            lblDescLocal = lblDesc.Location;
            txtDescLocal = txtDesc.Location;
            txtDescSize = txtDesc.Size;

            CheckHidden();

            var lTypesColumn = lTypes.Select(x => x.Name).ToList();
            drpType.DataSource = lTypesColumn;

            txtStartTime.Enabled = false;
            responseMessage.Enabled = false;

            lblStop.Enabled = false;

            TimeID = -1;
            isInitialized = true;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            string path = Path.Combine(CSVImport.root, "TimeCapture_log_sql.txt");
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            dataGridView1.RowsAdded += PaintRows;
        }

        #region CheckBoxes
        private void rbNonCharge_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNonCharge.Checked)
            {
                rbCharge.Checked = false;
                rbSupport.Checked = false;
            }
        }
        private void rbCharge_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCharge.Checked)
            {
                rbNonCharge.Checked = false;
                rbSupport.Checked = false;
            }
        }
        private void rbSupport_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSupport.Checked)
            {
                rbNonCharge.Checked = false;
                rbCharge.Checked = false;
            }
        }
        #endregion

        #region Buttons
        private void btnAddTicket_Click(object sender, EventArgs e)
        {
            Forms.AddTicket addTicket = new Forms.AddTicket(this);
            addTicket.Show();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Forms.Settings fSettings = new Forms.Settings(this);
            fSettings.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (Access.GetSettingValue(1))
            {
                TimeCapture timeCapture = new TimeCapture();
                ConfirmClose confirmClose = new ConfirmClose(this);
                timeCapture.Dispose();
                confirmClose.Show();
                confirmClose.BringToFront();
            }
            else
            {
                Dispose();
            }
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            exportProgress.Value = 15;
            string last;
            StoreTime(out last);
            exportProgress.Value = 100;
            if (!string.IsNullOrEmpty(last))
            {
                sendToast("Your time for " + DateTime.Now.ToString("dd MMM yyyy") + " has been exported | " + last);
            }
            else
            {
                sendToast("No time to capture");
            }
        }

        private void lblPlay_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please fill in Name");
            }
            else if (String.IsNullOrWhiteSpace(txtTicketNo.Text) && !Access.GetSettingValue(3))
            {
                MessageBox.Show("Please fill in the Ticket Number, if unkown, just put 0");
            }
            else if (lblPlay.Enabled)
            {
                txtCurrent.Text = txtName.Text;
                txtStartTime.Text = DateTime.Now.ToString("HH:mm");

                ptName = txtName.Text;

                if (Access.GetSettingValue(3))
                {
                    ptTicketNumber = 0;
                }
                else
                {
                    int inputNumber;
                    bool isNumber = int.TryParse(txtTicketNo.Text, out inputNumber);
                    if (isNumber)
                    {
                        ptTicketNumber = Convert.ToInt32(txtTicketNo.Text);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtTicketNo.SelectedValue.ToString()))
                        {
                            ptTicketNumber = Convert.ToInt32(txtTicketNo.SelectedValue);
                        }
                        else
                        {
                            MessageBox.Show("Please either use the dropdown or provide a valid number");
                        }
                    }
                }
                ptStart = DateTime.Now.ToString("HH:mm");
                dtStart = DateTime.Now;
                ptTimeType = drpType.SelectedItem.ToString();
                if (String.IsNullOrEmpty(txtDesc.Text))
                {
                    ptDesc = "";
                }
                else
                {
                    ptDesc = txtDesc.Text;
                }

                if (rbCharge.Checked)
                {
                    ptTicketType = "Chargeable";
                }
                else if (rbNonCharge.Checked)
                {
                    ptTicketType = "Non-Chargeable";
                }
                else if (rbSupport.Checked)
                {
                    ptTicketType = "Support";
                }
                else
                {
                    ptTicketType = "Non-Chargeable";
                }
                lblStop.Enabled = true;
                lblPlay.Enabled = false;
            }
        }

        private void lblStop_Click(object sender, EventArgs e)
        {
            if (lblStop.Enabled)
            {
                TimeSpan preTotal = DateTime.Now.Subtract(dtStart);
                string Total = preTotal.ToString().Substring(0, 5);

                Time time = new Time();
                time.TimeID = -1;
                time.Item = ptName;
                time.TicketNo = ptTicketNumber;
                time.Start = ptStart;
                time.End = DateTime.Now.ToString("HH:mm");
                time.Total = Total;
                time.TimeType = ptTimeType;
                time.Description = ptDesc;
                time.Type = ptTicketType;
                time.Date = DateTime.Now.ToString("dd MMM yyyy");
                lTime.Add(time);

                DataRow row = tblTime.NewRow();
                row["tName"] = ptName;
                row["TicketNumber"] = ptTicketNumber;
                row["StartTime"] = ptStart;
                row["EndTime"] = DateTime.Now.ToString("HH:mm");
                row["Total"] = Total;
                row["TimeType"] = ptTimeType;
                row["Description"] = ptDesc;
                row["TicketType"] = ptTicketType;
                row["Date"] = DateTime.Now.ToString("dd MMM yyyy");
                tblTime.Rows.Add(row);

                dataGridView1.Rows.Add(TimeID, ptName, ptTicketNumber, ptStart, DateTime.Now.ToString("HH:mm"),
                    Total, ptTimeType, ptDesc, ptTicketType, DateTime.Now.ToString("dd MMM yyyy"), "Delete", "Continue");
                lblStop.Enabled = false;
                lblPlay.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lTime.Clear();
            dataGridView1.Rows.Clear();
            sendToast("All recently captured time cleared");
            exportProgress.Value = 0;
        }

        private void btnCaptureTime_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Would you like to export the current time first?", "", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                ShowSpinner();
                this.Hide();
                try
                {
                    btnExport_Click(sender, e);
                    timeCapture.CaptureTime(BrowserType.Chrome);
                    sendToast("Time has been captured");
                    new _logger().Log(LogType.Info, "Time captured");
                }
                catch (Exception ex)
                {
                    new _logger().Log(LogType.Error, "Time for " + DateTime.Now.ToString("dd MMM yyyy") + " captured");
                    MessageBox.Show("Failed to capture time, please try again");
                    DialogResult error = MessageBox.Show("Failed to capture time, see exception message?", "", MessageBoxButtons.YesNo);
                    if (error == DialogResult.Yes)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                ShowSpinner();
                this.Hide();
                try
                {
                    timeCapture.CaptureTime(BrowserType.Chrome);
                    new _logger().Log(LogType.Info, "Time for " + DateTime.Now.ToString("dd MMM yyyy") + " captured");
                }
                catch (Exception ex)
                {
                    new _logger().Log(LogType.Error, "Time for " + DateTime.Now.ToString("dd MMM yyyy") + " captured");
                    MessageBox.Show("Failed to capture time, please try again");
                    DialogResult error = MessageBox.Show("Failed to capture time, see exception message?", "", MessageBoxButtons.YesNo);
                    if (error == DialogResult.Yes)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            HideSpinner();
            this.Show();
        }

        private void btnDelTicket_Click(object sender, EventArgs e)
        {
            DeleteTicket frmDelete = new(this);
            frmDelete.Show();
        }

        #endregion Buttons

        #region Actions

        public void CheckHidden()
        {
            if (Access.GetSettingValue(3))                                      // TNo hidden
            {
                label1.Hide();
                txtTicketNo.Hide();
                dataGridView1.Columns["TicketNumber"].Visible = false;
                lblTypeLocal = lblType.Location;
                drpTypeLocal = drpType.Location;
                drpTypeSize = drpType.Size;
                lblType.Location = label1.Location;
                drpType.Location = txtTicketNo.Location;
                drpType.Size = txtTicketNo.Size;
                if (!Access.GetSettingValue(2))                                // TNo hidden and Desc not hidden
                {
                    lblDesc.Location = lblTypeLocal;
                    txtDesc.Location = drpTypeLocal;
                }
            }
            else                                                               // TNo not hidden
            {
                label1.Show();
                txtTicketNo.Show();
                dataGridView1.Columns["TicketNumber"].Visible = true;
                lblType.Location = lblTypeLocal;
                drpType.Location = drpTypeLocal;
                drpType.Size = drpTypeSize;
                lblDesc.Location = lblDescLocal;
                txtDesc.Location = txtDescLocal;
            }

            if (Access.GetSettingValue(2))                                  // Desc Hidden
            {
                lblDesc.Hide();
                txtDesc.Hide();
                dataGridView1.Columns["Description"].Visible = false;
            }
            else                                                            // Desc not hidden
            {
                lblDesc.Show();
                txtDesc.Show();
                dataGridView1.Columns["Description"].Visible = true;
                if (Access.GetSettingValue(3))
                {
                    lblDesc.Location = lblTypeLocal;
                    txtDesc.Location = drpTypeLocal;
                }
            }

            if (Access.GetSettingValue(7))
            {
                btnCaptureTime.Visible = true;
            }
            else
            {
                btnCaptureTime.Visible = false;
            }
        }

        public void CreateTable()
        {
            tblTime = new DataTable();
            tblTime.Columns.Add("tName");
            tblTime.Columns.Add("TicketNumber");
            tblTime.Columns.Add("StartTime");
            tblTime.Columns.Add("EndTime");
            tblTime.Columns.Add("Total");
            tblTime.Columns.Add("TimeType");
            tblTime.Columns.Add("Description");
            tblTime.Columns.Add("TicketType");
            tblTime.Columns.Add("Date");
        }

        public void WriteDataToCsv()
        {
            CSVImport.CreateFolder();
            string filePath = Path.Combine(root, "csvImport", "ZZZImport_CSV.csv");
            exportProgress.Value = 25;

            var stream = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                exportProgress.Value = 50;
                
                if (stream.Length == 0)
                {
                    csv.WriteHeader<CSVImport>();
                    csv.NextRecord();
                }

                foreach (var time in lTime)
                {
                    csv.WriteField(time.Item);
                    csv.WriteField(time.TicketNo);
                    csv.WriteField(time.Start);
                    csv.WriteField(time.End);
                    csv.WriteField(time.Total);
                    csv.WriteField(time.TimeType);
                    csv.WriteField(time.Description);
                    csv.WriteField(time.Type);
                    csv.WriteField(time.Date);
                    csv.NextRecord();
                }
                
                exportProgress.Value = 75;
            }
            stream.Close();
            lTime.Clear();
        }

        public void StoreTime(out string last)
        {
            last = null;
            int i = 0;
            int Count = lTime.Count;
            foreach (var time in lTime)
            {
                i++;
                Access.SaveTime(time.TimeID, time.Item, time.TicketNo, time.Start, time.End,
                    time.Total, time.TimeType, time.Description, time.Type, time.Date);
                if (i == Count)
                {
                    last = "End " + '@' + " " + time.End;
                }
            }
            lTime.Clear();
        }

        public void getTypes()
        {
            DataSet dsTypes = new Access().GetTicketTypes();

            if (dsTypes.HasRows())
            {
                foreach (DataRow row in dsTypes.Tables[0].Rows)
                {
                    CSVImport.Types csTypes = new CSVImport.Types();
                    csTypes.ID = row.GetDataRowIntValue("ID");
                    csTypes.Name = row.GetDataRowStringValue("Name");
                    lTypes.Add(csTypes);
                }
            }
        }

        public void sendToast(string msg)
        {
            if (Access.GetSettingValue(4))
            {
                Uri uri = new Uri(Path.Combine(root, "TimeIcon.png"));
                var doc = new XmlDocument();

                ToastContentBuilder toastContent = new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddAppLogoOverride(uri)
                    .AddText(msg)
                    .AddHeader("0", "Time Capture", "");
            }
            else
            {
                MessageBox.Show(msg);
                responseMessage.Text = DateTime.Now.ToString("[hh:mm]") + " " + msg;
            }
        }

        public void addTicket(string Name, string TicketNumber)
        {
            string filePath = Path.Combine(root, "Data", "Tickets.csv");
            using (var stream = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField(Name);
                csv.WriteField(TicketNumber);
            }
            getTickets();
            txtTicketNo.DataSource = lTickets;
            txtTicketNo.DisplayMember = "Name";
            txtTicketNo.ValueMember = "ID";
            sendToast("Ticket " + Name + " | " + TicketNumber + " has been added");
        }

        public void getTickets()
        {
            lTickets.Clear();
            txtTicketNo.DataSource = null;
            DataSet dsTickets = new Access().GetTickets();

            if (dsTickets.HasRows())
            {
                foreach (DataRow row in dsTickets.Tables[0].Rows)
                {
                    CSVImport.Tickets csvTicket = new CSVImport.Tickets();
                    csvTicket.ID = row.GetDataRowIntValue("ID");
                    csvTicket.Name = row.GetDataRowStringValue("Name");
                    lTickets.Add(csvTicket);
                }
            }

            txtTicketNo.DataSource = lTickets;
            txtTicketNo.DisplayMember = "Name";
            txtTicketNo.ValueMember = "ID";
        }

        public void DeleteTime(int rowIndex, int iTimeID)
        {
            dataGridView1.Rows.Remove(dataGridView1.Rows[rowIndex]);
            if (iTimeID != -1)
            {
                DialogResult result = MessageBox.Show("Would you like to delete the record entirely?", "", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    new Access().DeleteTime(iTimeID);
                }
            }
        }

        public void ShowSpinner()
        {
            Spinner = new();
            Spinner.Show();
        }

        public void HideSpinner()
        {
            Spinner.Hide();
        }

        public void ShowInputDialog(string sContext, out bool OK, out string sValue)
        {
            // Create a custom input box form
            Form inputBoxForm = new Form();
            inputBoxForm.Text = "";
            inputBoxForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBoxForm.StartPosition = FormStartPosition.CenterParent;
            inputBoxForm.Width = 240;
            inputBoxForm.Height = 150;

            // Create the label and text box for input
            Label label = new Label();
            label.Text = sContext;
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(10, 10);

            TextBox textBox = new TextBox();
            textBox.Location = new System.Drawing.Point(10, 30);
            textBox.Size = new System.Drawing.Size(200, 20);

            // Create the OK and Cancel buttons
            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new System.Drawing.Point(10, 60);
            okButton.Height = 25;
            okButton.Click += (sender, e) => inputBoxForm.Close();

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(90, 60);
            cancelButton.Height = 25;
            cancelButton.Click += (sender, e) => inputBoxForm.Close();

            // Add the controls to the form
            inputBoxForm.Controls.AddRange(new Control[] { label, textBox, okButton, cancelButton });

            // Show the input box form as a dialog
            if (inputBoxForm.ShowDialog() == DialogResult.OK)
            {
                OK = true;
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    sValue = textBox.Text.ToString();
                }
                else
                {
                    sValue = "";
                }
            }
            else
            {
                OK = false;
                sValue = "";
            }
        }

        public void ShowDateInputDialog(string sContext, out bool OK, out string sValue)
        {
            // Create a custom input box form
            Form inputBoxForm = new Form();
            inputBoxForm.Text = "";
            inputBoxForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBoxForm.StartPosition = FormStartPosition.CenterParent;
            inputBoxForm.Width = 240;
            inputBoxForm.Height = 150;

            // Create the label and text box for input
            Label label = new Label();
            label.Text = sContext;
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(10, 10);

            DateTimePicker textBox = new DateTimePicker();
            textBox.Format = DateTimePickerFormat.Custom;
            textBox.CustomFormat = "dd MMM yyyy";
            textBox.Location = new System.Drawing.Point(10, 30);
            textBox.Size = new System.Drawing.Size(200, 20);

            // Create the OK and Cancel buttons
            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new System.Drawing.Point(10, 60);
            okButton.Height = 25;
            okButton.Click += (sender, e) => inputBoxForm.Close();

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(90, 60);
            cancelButton.Height = 25;
            cancelButton.Click += (sender, e) => inputBoxForm.Close();

            // Add the controls to the form
            inputBoxForm.Controls.AddRange(new Control[] { label, textBox, okButton, cancelButton });

            // Show the input box form as a dialog
            if (inputBoxForm.ShowDialog() == DialogResult.OK)
            {
                OK = true;
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    sValue = textBox.Text.ToString();
                }
                else
                {
                    sValue = "";
                }
            }
            else
            {
                OK = false;
                sValue = "";
            }
        }

        public void ShowDateRangeInputDialog(string sContext, out bool OK, out string Start, out string End)
        {
            // Create a custom input box form
            Form inputBoxForm = new Form();
            inputBoxForm.Text = "";
            inputBoxForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBoxForm.StartPosition = FormStartPosition.CenterParent;
            inputBoxForm.Width = 240;
            inputBoxForm.Height = 200;

            // Create the label and text box for input
            Label label = new Label();
            label.Text = sContext;
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(10, 10);

            DateTimePicker textBox = new DateTimePicker();
            textBox.Format = DateTimePickerFormat.Custom;
            textBox.CustomFormat = "dd MMM yyyy";
            textBox.Location = new System.Drawing.Point(10, 30);
            textBox.Size = new System.Drawing.Size(200, 20);

            DateTimePicker txtEnd = new DateTimePicker();
            txtEnd.Format = DateTimePickerFormat.Custom;
            txtEnd.CustomFormat = "dd MMM yyyy";
            txtEnd.Location = new System.Drawing.Point(10, 60);
            txtEnd.Size = new System.Drawing.Size(200, 20);

            // Create the OK and Cancel buttons
            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new System.Drawing.Point(10, 90);
            okButton.Height = 25;
            okButton.Click += (sender, e) => inputBoxForm.Close();

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(90, 90);
            cancelButton.Height = 25;
            cancelButton.Click += (sender, e) => inputBoxForm.Close();

            // Add the controls to the form
            inputBoxForm.Controls.AddRange(new Control[] { label, textBox, txtEnd, okButton, cancelButton });

            // Show the input box form as a dialog
            if (inputBoxForm.ShowDialog() == DialogResult.OK)
            {
                OK = true;
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    Start = textBox.Text.ToString();
                    End = txtEnd.Text.ToString();
                }
                else
                {
                    Start = "";
                    End = "";
                }
            }
            else
            {
                OK = false;
                Start = "";
                End = "";
            }
        }

        #endregion Actions

        #region NavMenu

        private void csvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = openFileDialog1;
            openFileDialog1.Filter = "Excel Files|*.csv";
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    string fileName = dialog.FileName;

                    var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

                    using (var writer = new StreamReader(stream))
                    using (var csv = new CsvReader(writer, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<CSVImport.CSV>().ToList();

                        int i = 0;
                        var Count = records.Count();
                        foreach (var user in records)
                        {
                            i++;
                            Lists.Time time = new Lists.Time();
                            time.TimeID = TimeID;
                            time.Item = user.Item;
                            time.TicketNo = user.TicketNo;
                            time.Start = user.Start;
                            time.End = user.End;
                            time.Total = user.Total;
                            time.TimeType = user.TimeType;
                            time.Type = user.Type;
                            time.Description = user.Description;
                            time.Date = user.Date;
                            lTime.Add(time);

                            int progress = (int)((double)i / Count * 100);
                            exportProgress.Value = progress;
                            dataGridView1.Rows.Add(TimeID, user.Item, user.TicketNo, user.Start,
                                user.End, user.Total, user.Type, user.TimeType, user.Date, "Delete", "Continue");
                        }
                    }
                }
                catch
                {
                    sendToast("Please make sure a valid csv with the correct columns was submitted");
                }
            }
        }

        private void notesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Notes notes = new Forms.Notes();
            notes.Show();
        }

                private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsTime = new Access().getTime(1);
            if (dsTime.HasRows())
            {
                foreach (DataRow time in dsTime.Tables[0].Rows)
                {
                    dataGridView1.Rows.Add(time.GetDataRowIntValue("TimeID"), time.GetDataRowStringValue("Item"), time.GetDataRowStringValue("TicketNo"), time.GetDataRowStringValue("Start"), 
                        time.GetDataRowStringValue("End"), time.GetDataRowStringValue("Total"), time.GetDataRowStringValue("TimeType"), 
                        time.GetDataRowStringValue("Description"), time.GetDataRowStringValue("TicketType"), time.GetDataRowStringValue("Date"), "Delete", "Continue");
                }
            }
        }

        private void uncapturedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsTime = new Access().getTime(2);
            if (dsTime.HasRows())
            {
                foreach (DataRow time in dsTime.Tables[0].Rows)
                {
                    dataGridView1.Rows.Add(time.GetDataRowIntValue("TimeID"), time.GetDataRowStringValue("Item"), time.GetDataRowStringValue("TicketNo"), time.GetDataRowStringValue("Start"),
                        time.GetDataRowStringValue("End"), time.GetDataRowStringValue("Total"), time.GetDataRowStringValue("TimeType"),
                        time.GetDataRowStringValue("Description"), time.GetDataRowStringValue("TicketType"), time.GetDataRowStringValue("Date"), "Delete", "Continue");
                }
            }
        }

        private void capturedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsTime = new Access().getTime(3);
            if (dsTime.HasRows())
            {
                foreach (DataRow time in dsTime.Tables[0].Rows)
                {
                    dataGridView1.Rows.Add(time.GetDataRowIntValue("TimeID"), time.GetDataRowStringValue("Item"), time.GetDataRowStringValue("TicketNo"), time.GetDataRowStringValue("Start"),
                        time.GetDataRowStringValue("End"), time.GetDataRowStringValue("Total"), time.GetDataRowStringValue("TimeType"),
                        time.GetDataRowStringValue("Description"), time.GetDataRowStringValue("TicketType"), time.GetDataRowStringValue("Date"), "Delete", "Continue");
                }
            }
        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sContext = "Please provide a ticket number to view";
            bool OKResult;
            string sValue;
            ShowInputDialog(sContext, out OKResult, out sValue);

            if (OKResult)
            {
                ShowSpinner();
                TicketViewer ticketViewer = new(sValue);
                ticketViewer.Show();
                HideSpinner();
            }
        }

        private void byDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool OK;
            string sDate;
            ShowDateInputDialog("Please provide a date", out OK, out sDate);
            if (OK && !string.IsNullOrEmpty(sDate))
            {
                DataSet dsTime = new Access().getTimeByDay(sDate);
                if (dsTime.HasRows())
                {
                    foreach (DataRow time in dsTime.Tables[0].Rows)
                    {
                        dataGridView1.Rows.Add(time.GetDataRowIntValue("TimeID"), time.GetDataRowStringValue("Item"), time.GetDataRowStringValue("TicketNo"), time.GetDataRowStringValue("Start"),
                            time.GetDataRowStringValue("End"), time.GetDataRowStringValue("Total"), time.GetDataRowStringValue("TimeType"),
                            time.GetDataRowStringValue("Description"), time.GetDataRowStringValue("TicketType"), time.GetDataRowStringValue("Date"), "Delete", "Continue");
                    }
                }
            }
        }

        private void byDateRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool OK;
            string sStart;
            string sEnd;
            ShowDateRangeInputDialog("Please provide a start and End Date", out OK, out sStart, out sEnd);
            if (OK && !string.IsNullOrEmpty(sStart))
            {
                DataSet dsTime = new Access().GetTimeByDateRange(sStart, sEnd);
                if (dsTime.HasRows())
                {
                    foreach (DataRow time in dsTime.Tables[0].Rows)
                    {
                        dataGridView1.Rows.Add(time.GetDataRowIntValue("TimeID"), time.GetDataRowStringValue("Item"), time.GetDataRowStringValue("TicketNo"), time.GetDataRowStringValue("Start"),
                            time.GetDataRowStringValue("End"), time.GetDataRowStringValue("Total"), time.GetDataRowStringValue("TimeType"),
                            time.GetDataRowStringValue("Description"), time.GetDataRowStringValue("TicketType"), time.GetDataRowStringValue("Date"), "Delete", "Continue");
                    }
                }
            }
        }

        private void fromDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool OK;
            string sStart;
            string sEnd;
            ShowDateInputDialog("Please provide a date", out OK, out sStart);
            if (OK && !string.IsNullOrEmpty(sStart))
            {
                DataSet dsTime = new Access().GetTimeByDateRange(sStart, null);
                if (dsTime.HasRows())
                {
                    foreach (DataRow time in dsTime.Tables[0].Rows)
                    {
                        dataGridView1.Rows.Add(time.GetDataRowIntValue("TimeID"), time.GetDataRowStringValue("Item"), time.GetDataRowStringValue("TicketNo"), time.GetDataRowStringValue("Start"),
                            time.GetDataRowStringValue("End"), time.GetDataRowStringValue("Total"), time.GetDataRowStringValue("TimeType"),
                            time.GetDataRowStringValue("Description"), time.GetDataRowStringValue("TicketType"), time.GetDataRowStringValue("Date"), "Delete", "Continue");
                    }
                }
            }
        }

        #endregion Nav

        #region DataGridView

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isInitialized)
            {
                // Get the edited values from the DataGridView row
                int selectedTimeID = dataGridView1.CurrentRow.GetDataGridViewIntValue("iTimeID");
                string selectedStart = dataGridView1.CurrentRow.GetDataGridViewStringValue("StartTime");
                string selectedEnd = dataGridView1.CurrentRow.GetDataGridViewStringValue("EndTime");
                string editedPtName = dataGridView1.CurrentRow.GetDataGridViewStringValue("tName");
                string editedPtTicketNumber = dataGridView1.CurrentRow.GetDataGridViewStringValue("TicketNumber");
                string editedPtStart = dataGridView1.CurrentRow.GetDataGridViewStringValue("StartTime");
                string editedPtEnd = dataGridView1.CurrentRow.GetDataGridViewStringValue("EndTime");
                string editedTotal = dataGridView1.CurrentRow.GetDataGridViewStringValue("Total");
                string editedPtTimeType = dataGridView1.CurrentRow.GetDataGridViewStringValue("TimeType");
                string editedPtDesc = dataGridView1.CurrentRow.GetDataGridViewStringValue("Description");
                string editedPtTicketType = dataGridView1.CurrentRow.GetDataGridViewStringValue("TicketType");
                string editedPtDate = dataGridView1.CurrentRow.GetDataGridViewStringValue("Date");

                // Find the item in the list with the matching TimeID
                Time itemToUpdate = lTime.FirstOrDefault(t => t.Start == selectedStart || t.End == selectedEnd);

                if (itemToUpdate != null)
                {
                    // Update the item properties with the edited values
                    itemToUpdate.Item = editedPtName;
                    itemToUpdate.TicketNo = Convert.ToInt32(editedPtTicketNumber);
                    if (itemToUpdate.Start != editedPtStart || itemToUpdate.End != editedPtEnd)
                    {
                        try
                        {
                            TimeSpan preTotal = Convert.ToDateTime(editedPtEnd).Subtract(Convert.ToDateTime(editedPtStart));
                            string Total = preTotal.ToString().Substring(0, 5);
                            itemToUpdate.Total = Total;
                            dataGridView1.CurrentRow.Cells["Total"].Value = Total;
                        }
                        catch
                        {
                            itemToUpdate.Total = editedTotal;
                        }
                    }
                    else
                    {
                        itemToUpdate.Total = editedTotal;
                    }
                    itemToUpdate.Start = editedPtStart;
                    itemToUpdate.End = editedPtEnd;
                    itemToUpdate.TimeType = editedPtTimeType;
                    itemToUpdate.Description = editedPtDesc;
                    itemToUpdate.Type = editedPtTicketType;
                    itemToUpdate.Date = editedPtDate;
                }
                else if (selectedTimeID != -1)
                {
                    Time time = new Time();
                    time.TimeID = selectedTimeID;
                    time.Item = editedPtName;
                    try
                    {
                        time.TicketNo = Convert.ToInt32(editedPtTicketNumber);
                    }
                    catch
                    {
                        time.TicketNo = -1;
                    }
                    try
                    {
                        TimeSpan preTotal = Convert.ToDateTime(editedPtEnd).Subtract(Convert.ToDateTime(editedPtStart));
                        string Total = preTotal.ToString().Substring(0, 5);
                        time.Total = Total;
                        dataGridView1.CurrentRow.Cells["Total"].Value = Total;
                    }
                    catch
                    {
                        time.Total = editedTotal;
                    }

                    time.Start = editedPtStart;
                    time.End = editedPtEnd;
                    time.TimeType = editedPtTimeType;
                    time.Description = editedPtDesc;
                    time.Type = editedPtTicketType;
                    time.Date = editedPtDate;

                    if (time.TicketNo == -1)
                    {
                        sendToast("Please provide a valid ticket number");
                    }
                    else
                    {
                        new Access().UpdateTime(time);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0 && e.ColumnIndex == 10)
            {
                // Get the TimeID value from the clicked row
                int timeID = dataGridView1.Rows[e.RowIndex].GetDataGridViewIntValue("iTimeID");

                // Call the public void method that accepts the TimeID value

                DeleteTime(e.RowIndex, timeID);
                sendToast("Time Deleted");
            }
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn
                && e.RowIndex >= 0 && e.ColumnIndex == 11)
            {
                txtCurrent.Text = dataGridView1.Rows[e.RowIndex].GetDataGridViewStringValue("tName");
                txtStartTime.Text = DateTime.Now.ToString("HH:mm");

                ptName = dataGridView1.Rows[e.RowIndex].GetDataGridViewStringValue("tName");

                if (Access.GetSettingValue(3))
                {
                    ptTicketNumber = 0;
                }
                else
                {
                    ptTicketNumber = dataGridView1.Rows[e.RowIndex].GetDataGridViewIntValue("TicketNumber");
                }
                ptStart = DateTime.Now.ToString("HH:mm");
                dtStart = DateTime.Now;
                ptTimeType = dataGridView1.Rows[e.RowIndex].GetDataGridViewStringValue("TimeType");
                ptDesc = dataGridView1.Rows[e.RowIndex].GetDataGridViewStringValue("Description");
                ptTicketType = dataGridView1.Rows[e.RowIndex].GetDataGridViewStringValue("TicketType");

                lblStop.Enabled = true;
                lblPlay.Enabled = false;
            }
        }

        private void txtCurrent_TextChanged(object sender, EventArgs e)
        {
            if (txtCurrent.Text != null)
            {
                ptName = txtCurrent.Text.ToString();
            }
            else
            {
                txtCurrent.Text = ptName;
            }
        }

        private void UpdCurrent_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please fill in Name");
            }
            else if (String.IsNullOrWhiteSpace(txtTicketNo.Text) && !Access.GetSettingValue(3))
            {
                MessageBox.Show("Please fill in the Ticket Number, if unkown, just put 0");
            }
            else
            {
                txtCurrent.Text = txtName.Text;
                ptName = txtName.Text;

                if (Access.GetSettingValue(3))
                {
                    ptTicketNumber = 0;
                }
                else
                {
                    int inputNumber;
                    bool isNumber = int.TryParse(txtTicketNo.Text, out inputNumber);
                    if (isNumber)
                    {
                        ptTicketNumber = Convert.ToInt32(txtTicketNo.Text);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtTicketNo.SelectedValue.ToString()))
                        {
                            ptTicketNumber = Convert.ToInt32(txtTicketNo.SelectedValue);
                        }
                        else
                        {
                            MessageBox.Show("Please either use the dropdown or provide a valid number");
                        }
                    }
                }
                ptTimeType = drpType.SelectedItem.ToString();
                if (String.IsNullOrEmpty(txtDesc.Text))
                {
                    ptDesc = "";
                }
                else
                {
                    ptDesc = txtDesc.Text;
                }

                if (rbCharge.Checked)
                {
                    ptTicketType = "Chargeable";
                }
                else if (rbNonCharge.Checked)
                {
                    ptTicketType = "Non-Chargeable";
                }
                else if (rbSupport.Checked)
                {
                    ptTicketType = "Support";
                }
                else
                {
                    ptTicketType = "Non-Chargeable";
                }
                lblStop.Enabled = true;
                lblPlay.Enabled = false;
            }
        }

        #endregion DataGridView

        #region DarkMode

        private void toggleSwitch1_CheckedChanged_1(object sender, EventArgs e)
        {
            ShowSpinner();
            this.Hide();
            Color bgDark = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            Color bgDarkSecondary = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            Color controlDark = Color.SlateGray;

            if (toggleSwitch1.Checked)
            {
                // Set the text color of all objects to white
                foreach (Control control in this.Controls)
                {
                    control.BackColor = bgDark;
                    control.ForeColor = Color.White;

                    if (control is Button button)
                    {
                        button.FlatStyle = FlatStyle.Flat; // Set the FlatStyle to Flat
                        button.BackColor = bgDarkSecondary;
                        button.FlatAppearance.BorderColor = bgDark; // Set the border color to the same as the background color
                    }

                    if (control is ComboBox comboBox)
                    {
                        comboBox.FlatStyle = FlatStyle.Flat;
                        comboBox.BackColor = bgDark;
                        comboBox.ForeColor = Color.White;
                        comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                        if (comboBox.Name == txtTicketNo.Name)
                        {
                            comboBox.DrawItem += (s, eventArgs) =>
                            {
                                eventArgs.DrawBackground();
                                eventArgs.Graphics.DrawString(lTickets[eventArgs.Index].Name.ToString(), eventArgs.Font, Brushes.White, eventArgs.Bounds);
                            };
                        }
                        else if (comboBox.Name == drpType.Name)
                        {
                            comboBox.DrawItem += (s, eventArgs) =>
                            {
                                eventArgs.DrawBackground();
                                eventArgs.Graphics.DrawString(comboBox.Items[eventArgs.Index].ToString(), eventArgs.Font, Brushes.White, eventArgs.Bounds);
                            };
                        }
                    }

                    if (control is RichTextBox richTextBox)
                    {
                        richTextBox.BorderStyle = BorderStyle.FixedSingle;
                        richTextBox.BackColor = bgDark;
                        richTextBox.ForeColor = Color.White;
                    }

                    if (control is TextBox textBox)
                    {
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                        textBox.BackColor = bgDark;
                        textBox.ForeColor = Color.White;
                    }
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = bgDark;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }

                foreach (DataGridViewButtonCell buttonCell in dataGridView1.Rows.Cast<DataGridViewRow>()
                                .SelectMany(row => row.Cells.OfType<DataGridViewButtonCell>()))
                {
                    buttonCell.Style.BackColor = bgDark;
                    buttonCell.Style.ForeColor = Color.White;
                }

                this.BackColor = bgDark;

                dataGridView1.BackgroundColor = bgDarkSecondary;
                dataGridView1.GridColor = Color.Black;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Adjust the heading text color if needed
                dataGridView1.EnableHeadersVisualStyles = false; // Disable the default visual styles for the headers

                containerControl1.BackColor = bgDarkSecondary;

                Heading.BackColor = bgDarkSecondary;

                menuStrip1.BackColor = bgDark;
                menuStrip1.ForeColor = Color.White;

                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView1.CellPainting += DarkBorders;
            }
            else
            {
                foreach (Control control in this.Controls)
                {
                    control.BackColor = Color.White;
                    control.ForeColor = Color.Black;

                    if (control is Button button)
                    {
                        button.FlatStyle = FlatStyle.Flat; // Set the FlatStyle to Flat
                        button.BackColor = Color.White;
                        button.FlatAppearance.BorderColor = Color.Gray; // Set the border color to the same as the background color
                    }

                    if (control is ComboBox comboBox)
                    {
                        comboBox.FlatStyle = FlatStyle.Standard;
                        comboBox.BackColor = Color.White;
                        comboBox.ForeColor = Color.Black;
                        comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                        if (comboBox.Name == txtTicketNo.Name)
                        {
                            comboBox.DrawItem += (s, eventArgs) =>
                            {
                                eventArgs.DrawBackground();
                                eventArgs.Graphics.DrawString(lTickets[eventArgs.Index].Name.ToString(), eventArgs.Font, Brushes.Black, eventArgs.Bounds);
                            };
                        }
                        else if (comboBox.Name == drpType.Name)
                        {
                            comboBox.DrawItem += (s, eventArgs) =>
                            {
                                eventArgs.DrawBackground();
                                eventArgs.Graphics.DrawString(comboBox.Items[eventArgs.Index].ToString(), eventArgs.Font, Brushes.Black, eventArgs.Bounds);
                            };
                        }
                    }

                    if (control is RichTextBox richTextBox)
                    {
                        richTextBox.BorderStyle = BorderStyle.FixedSingle;
                        richTextBox.BackColor = Color.White;
                        richTextBox.ForeColor = Color.Black;
                    }

                    if (control is TextBox textBox)
                    {
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                        textBox.BackColor = Color.White;
                        textBox.ForeColor = Color.Black;
                    }

                    this.BackColor = Color.White;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }

                foreach (DataGridViewButtonCell buttonCell in dataGridView1.Rows.Cast<DataGridViewRow>()
                                .SelectMany(row => row.Cells.OfType<DataGridViewButtonCell>()))
                {
                    buttonCell.Style.BackColor = Color.White;
                    buttonCell.Style.ForeColor = Color.Black;
                }

                this.BackColor = Color.White;

                dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
                dataGridView1.GridColor = Color.Gray;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // Adjust the heading text color if needed
                dataGridView1.EnableHeadersVisualStyles = false; // Disable the default visual styles for the headers

                containerControl1.BackColor = System.Drawing.SystemColors.ControlLight;

                Heading.BackColor = System.Drawing.SystemColors.ControlLight;

                menuStrip1.BackColor = Color.White;
                menuStrip1.ForeColor = Color.Black;

                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView1.CellPainting += LightBorders;
            }
            this.Show();
            HideSpinner();
        }

        private void DarkBorders(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                using (Pen borderPen = new Pen(Color.Black, 2))
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(borderPen, rect);
                }

                e.Handled = true;
            }
        }

        private void LightBorders(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                using (Pen borderPen = new Pen(Color.Gray, 2))
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(borderPen, rect);
                }

                e.Handled = true;
            }
        }

        private void PaintRows(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (toggleSwitch1.Checked)
            {
                for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
            }
            else
            {
                for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        public void generic_DarkMode(Form form, out bool isDarkMode)
        {
            ShowSpinner();
            this.Hide();
            Color bgDark = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            Color bgDarkSecondary = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            Color controlDark = Color.SlateGray;

            if (toggleSwitch1.Checked)
            {
                isDarkMode = true;
                // Set the text color of all objects to white
                foreach (Control control in form.Controls)
                {
                    control.BackColor = bgDark;
                    control.ForeColor = Color.White;

                    if (control is Button button)
                    {
                        button.FlatStyle = FlatStyle.Flat; // Set the FlatStyle to Flat
                        button.BackColor = bgDarkSecondary;
                        button.FlatAppearance.BorderColor = bgDark; // Set the border color to the same as the background color
                    }

                    if (control is ComboBox comboBox)
                    {
                        comboBox.FlatStyle = FlatStyle.Flat;
                        comboBox.BackColor = bgDark;
                        comboBox.ForeColor = Color.White;
                        comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                        if (comboBox.Name == txtTicketNo.Name)
                        {
                            comboBox.DrawItem += (s, eventArgs) =>
                            {
                                eventArgs.DrawBackground();
                                eventArgs.Graphics.DrawString(lTickets[eventArgs.Index].Name.ToString(), eventArgs.Font, Brushes.White, eventArgs.Bounds);
                            };
                        }
                        else if (comboBox.Name == drpType.Name)
                        {
                            comboBox.DrawItem += (s, eventArgs) =>
                            {
                                eventArgs.DrawBackground();
                                eventArgs.Graphics.DrawString(comboBox.Items[eventArgs.Index].ToString(), eventArgs.Font, Brushes.White, eventArgs.Bounds);
                            };
                        }
                    }

                    if (control is RichTextBox richTextBox)
                    {
                        richTextBox.BorderStyle = BorderStyle.FixedSingle;
                        richTextBox.BackColor = bgDark;
                        richTextBox.ForeColor = Color.White;
                    }

                    if (control is TextBox textBox)
                    {
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                        textBox.BackColor = bgDark;
                        textBox.ForeColor = Color.White;
                    }
                }

                form.BackColor = bgDark;
            }
            else
            {
                isDarkMode = false;
                foreach (Control control in form.Controls)
                {
                    control.BackColor = Color.White;
                    control.ForeColor = Color.Black;

                    if (control is Button button)
                    {
                        button.FlatStyle = FlatStyle.Flat; // Set the FlatStyle to Flat
                        button.BackColor = Color.White;
                        button.FlatAppearance.BorderColor = Color.Gray; // Set the border color to the same as the background color
                    }

                    if (control is ComboBox comboBox)
                    {
                        comboBox.FlatStyle = FlatStyle.Standard;
                        comboBox.BackColor = Color.White;
                        comboBox.ForeColor = Color.Black;
                        comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                        if (comboBox.Name == txtTicketNo.Name)
                        {
                            comboBox.DrawItem += (s, eventArgs) =>
                            {
                                eventArgs.DrawBackground();
                                eventArgs.Graphics.DrawString(lTickets[eventArgs.Index].Name.ToString(), eventArgs.Font, Brushes.Black, eventArgs.Bounds);
                            };
                        }
                        else if (comboBox.Name == drpType.Name)
                        {
                            comboBox.DrawItem += (s, eventArgs) =>
                            {
                                eventArgs.DrawBackground();
                                eventArgs.Graphics.DrawString(comboBox.Items[eventArgs.Index].ToString(), eventArgs.Font, Brushes.Black, eventArgs.Bounds);
                            };
                        }
                    }

                    if (control is RichTextBox richTextBox)
                    {
                        richTextBox.BorderStyle = BorderStyle.FixedSingle;
                        richTextBox.BackColor = Color.White;
                        richTextBox.ForeColor = Color.Black;
                    }

                    if (control is TextBox textBox)
                    {
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                        textBox.BackColor = Color.White;
                        textBox.ForeColor = Color.Black;
                    }
                }

                form.BackColor = Color.White;
            }
            this.Show();
            HideSpinner();
        }

        #endregion DarkMode

    }
}
