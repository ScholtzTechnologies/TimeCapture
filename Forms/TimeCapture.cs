using TimeCapture.DB;
using TimeCapture.Forms;
using TimeCapture.utils;
using TimeCapture.Forms.Shared;
using SharpCompress.Archives;
using SharpCompress.Common;
using Microsoft.Toolkit.Uwp.Notifications;
using ScottPlot.Palettes;
using Uno.Extensions;

namespace TimeCapture
{
    public partial class TimeCapture : Form
    {
        #region Properties
        public List<Time> lTime = new List<Time>();
        public List<CSVImport.Types> lTypes = new List<CSVImport.Types>();
        public List<CSVImport.Clients> lClients = new List<CSVImport.Clients>();
        public List<CSVImport.Tickets> lTickets = new List<CSVImport.Tickets>();
        public List<CSVImport.Settings> Settings = new List<CSVImport.Settings>();
        public List<ProgressBar> progressBars = new List<ProgressBar>();
        public DB.Access Access = new DB.Access();
        public static string root = Directory.GetCurrentDirectory();
        public static string SettingsCsv = Path.Combine(root, "Data", "Settings.csv");
        public System.Drawing.Point lblTypeLocal { get; set; }
        public System.Drawing.Point drpTypeLocal { get; set; }
        public System.Drawing.Size drpTypeSize { get; set; }
        public System.Drawing.Point lblDescLocal { get; set; }
        public System.Drawing.Point txtDescLocal { get; set; }
        public System.Drawing.Size txtDescSize { get; set; }
        public bool isTaskRunning { get; set; }
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
        public Status oStatus { get; set; }

        public int UserID { get; set; }
        public string iTicketNumber { get; set; }

        public string TotalTime { get; set; }
        #endregion Properties

        public TimeCapture()
        {
            InitializeComponent();
            CheckDB();
            TotalTime = "00:00";

            string response;
            new Access().TestConnection(out response);
            new Access().InitiateKeepAlive();

            UserID = -1;
            responseMessage.Text = response;
            bool updatedNeeded = false;
            bool isSuccess = true;
            bool isSelenium = Access.GetSettingValue(7);

            getTypes();
            getTickets();
            SetLocations();
            CheckHidden();
            isCodeplex();
            sendReminders();

            dataGridView1.RowsAdded += PaintRows;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ContextMenuStrip = tblTimeContextMenu;
            dataGridView1.CellMouseDown += DataGridView1_CellMouseDown;
            (dataGridView1.Columns[6] as DataGridViewComboBoxColumn).FlatStyle = FlatStyle.Flat;
            (dataGridView1.Columns[8] as DataGridViewComboBoxColumn).FlatStyle = FlatStyle.Flat;

            txtTotalTime.Enabled = false;
            tsAdmin.Visible = false;

            var lTypesColumn = lTypes.Select(x => x.Name).ToList();
            drpType.DataSource = lTypesColumn;

            txtStartTime.Enabled = false;
            responseMessage.Enabled = false;

            lblStop.Enabled = false;

            TimeID = -1;
            isInitialized = true;

            string sError;
            bool bDriverUpdated = new _nuget().UpdateChromeDriver(out sError);
            if (!bDriverUpdated)
            {
                MessageBox.Show("Error updating chrome driver. Please restart app, if issue persists please contact support.");
                new _logger().Log(LogType.Error, sError, "Chrome Driver Update");
            }

            bool isDefaultDarkMode = Convert.ToInt32(_configuration.GetConfigValue("isDefaultDarkMode")).ToBool();
            if (isDefaultDarkMode)
                toggleSwitch1.Checked = true;

            progressBars.Add(this.roundedProgressBar1);
            progressBars.Add(this.roundedProgressBar2);
            progressBars.Add(this.roundedProgressBar3);
            progressBars.Add(this.roundedProgressBar4);

            foreach (var oBar in progressBars)
            {
                oBar.Maximum = 1;
            }
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

        private void lblPlay_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be blank");
            }
            else if (String.IsNullOrWhiteSpace(txtTicketNo.Text) && !Access.GetSettingValue(3))
            {
                MessageBox.Show("Please fill in the Ticket Number, if unkown, simply put 0");
            }
            else if (lblPlay.Enabled)
            {
                isTaskRunning = true;
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
                isTaskRunning = false;
                TimeSpan preTotal = DateTime.Now.Subtract(dtStart);
                string Total = preTotal.ToString().Substring(0, 5);

                Time time = new Time()
                {
                    TimeID = -1,
                    Item = ptName,
                    TicketNo = ptTicketNumber,
                    Start = ptStart,
                    End = DateTime.Now.ToString("HH:mm"),
                    Total = Total,
                    TimeType = ptTimeType,
                    Description = ptDesc,
                    Type = ptTicketType,
                    Date = DateTime.Now.ToString("dd MMM yyyy")
                };

                int iTimeID = new Access().InsertTime(time, UserID);

                dataGridView1.Rows.Add(iTimeID, ptName, ptTicketNumber, ptStart, DateTime.Now.ToString("HH:mm"),
                    Total, ptTimeType, ptDesc, ptTicketType, DateTime.Now.ToString("dd MMM yyyy"), "Delete", "Continue");
                lblStop.Enabled = false;
                lblPlay.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lTime.Clear();
            dataGridView1.Rows.Clear();
            PushNofication("All recently captured time cleared", NotificationType.Info);
            txtTotalTime.Text = "00:00";
        }

        private void btnCaptureTime_Click(object sender, EventArgs e)
        {
            this.Hide();
            Control lblAction;
            ShowSpinner(out lblAction);
            try
            {
                timeCapture.CaptureTime(BrowserType.Chrome, lblAction);
                new _logger().Log(LogType.Info, "Time for " + DateTime.Now.ToString("dd MMM yyyy") + " captured");
            }
            catch (Exception ex)
            {
                new _logger().Log(LogType.Error, "Time for " + DateTime.Now.ToString("dd MMM yyyy") + " captured");
                DialogResult error = MessageBox.Show("Failed to capture time, see exception message?", "", MessageBoxButtons.YesNo);

                if (error == DialogResult.Yes)
                    MessageBox.Show(ex.Message.ToString());
            }
            HideSpinner();
            this.Show();
        }

        private void btnDelTicket_Click(object sender, EventArgs e)
        {
            ViewTickets frmTickets = new(this);
            frmTickets.Show();
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            NewTimeStart("Internal Support", 31790, "General", "", "Non-Chargeable");
            txtName.Text = "Internal Support";
            rbNonCharge.Checked = true;
        }

        private void btnMeeting_Click(object sender, EventArgs e)
        {
            NewTimeStart("Internal Meeting", 39971, "Meeting", "", "Non-Chargeable");
            txtName.Text = "Internal Meeting";
            rbNonCharge.Checked = true;
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
            try
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
            catch
            {
                MessageBox.Show("Failed to delete time");
            }
        }

        public void ShowSpinner(out Control lblAction)
        {
            if (Spinner == null)
                Spinner = new(out lblAction);
            else
                lblAction = Spinner.GetLabel();

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

        public void ShowTextAreaInputDialog(string sContext, out bool OK, out string sValue)
        {
            // Create a custom input box form
            Form inputBoxForm = new Form();
            inputBoxForm.Text = "";
            inputBoxForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBoxForm.StartPosition = FormStartPosition.CenterParent;
            inputBoxForm.Width = 240;
            inputBoxForm.Height = 190;

            // Create the label and text box for input
            Label label = new Label();
            label.Text = sContext;
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(10, 10);

            RichTextBox textBox = new RichTextBox();
            textBox.Location = new System.Drawing.Point(10, 30);
            textBox.Size = new System.Drawing.Size(200, 85);

            // Create the OK and Cancel buttons
            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new System.Drawing.Point(10, 115);
            okButton.Height = 25;
            okButton.Click += (sender, e) => inputBoxForm.Close();

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(90, 115);
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
                    sValue = textBox.Text;
                }
                else
                {
                    sValue = "";
                }
            }
            else
            {
                sValue = "";
                OK = false;
            }
        }

        public void SetLocations()
        {
            lblTypeLocal = lblType.Location;
            drpTypeLocal = drpType.Location;
            drpTypeSize = drpType.Size;

            lblDescLocal = lblDesc.Location;
            txtDescLocal = txtDesc.Location;
            txtDescSize = txtDesc.Size;
        }

        public void InsertIntoTimeTable(DataSet ds)
        {
            foreach (DataRow time in ds.Tables[0].Rows)
            {
                dataGridView1.Rows.Add(time.GetDataRowIntValue("TimeID"),
                        time.GetDataRowStringValue("Item"), time.GetDataRowIntValue("TicketNo"),
                        time.GetDataRowStringValue("Start"), time.GetDataRowStringValue("End"),
                        time.GetDataRowStringValue("Total"), time.GetDataRowStringValue("TimeType"),
                        time.GetDataRowStringValue("Description"), time.GetDataRowStringValue("TicketType"),
                        time.GetDataRowStringValue("Date"),
                        "Delete", "Continue"
                    );
            }
        }

        public void UpdateTotal()
        {
            txtTotalTime.Text = "00:00";
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                string sTotal = item.GetDataGridViewStringValue("Total");
                if (string.IsNullOrEmpty(txtTotalTime.Text))
                    TotalTime = "00:00";
                else
                    TotalTime = txtTotalTime.Text;
                try
                {
                    TimeSpan Total;
                    TimeSpan.TryParse(TotalTime, out Total);
                    TimeSpan.TryParse(Convert.ToDateTime(sTotal).Add(Total).ToString("HH:mm"), out Total);
                    txtTotalTime.Text = Total.ToString().Substring(0, 5);
                }
                catch { }
            }
        }

        public TimeSpan GetTimeSpan(string initial, string final)
        {
            TimeSpan Total =
            Convert.ToDateTime(initial).Subtract(Convert.ToDateTime(final));
            return Total;
        }

        public void LoginResponse(int iUserID)
        {
            bool isAdmin = Access.isAdmin(iUserID);
            if (isAdmin)
                tsAdmin.Visible = true;
        }

        public void isCodeplex()
        {
            bool bIsCodeplex = Convert.ToInt32(_configuration.GetConfigValue("IsCodeplex")).ToBool();
            if (!bIsCodeplex)
            {
                btnSupport.Hide();
                btnMeeting.Hide();
            }
        }

        public void NewTimeStart(string sName, int iTicketNo, string sTimeType, string sDescription, string sTicketType)
        {
            if (isTaskRunning)
                lblStop_Click(null, null);

            isTaskRunning = true;

            txtCurrent.Text = sName;
            txtStartTime.Text = DateTime.Now.ToString("HH:mm");

            ptName = sName;

            if (Access.GetSettingValue(3))
            {
                ptTicketNumber = 0;
            }
            else
            {
                ptTicketNumber = iTicketNo;
            }
            ptStart = DateTime.Now.ToString("HH:mm");
            dtStart = DateTime.Now;
            ptTimeType = sTimeType;
            ptDesc = sDescription;
            ptTicketType = sTicketType;

            lblStop.Enabled = true;
            lblPlay.Enabled = false;
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
                    StartLoading();
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
                            time.TimeID = -1;
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

                            dataGridView1.Rows.Add(TimeID, ptName, ptTicketNumber, ptStart, DateTime.Now.ToString("HH:mm"),
                                Total, ptTimeType, ptDesc, ptTicketType, DateTime.Now.ToString("dd MMM yyyy"), "Delete", "Continue");
                        }
                        PushNofication(Count.ToString() + " records added", NotificationType.Info);
                    }
                    StopLoading(true);
                }
                catch
                {
                    StopLoading(false);
                    PushNofication("Please make sure a valid csv with the correct columns was submitted", NotificationType.Error);
                }
            }
        }

        private void notesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Notes notes = new Forms.Notes(this);
            notes.Show();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsTime = new Access().getTime(1, UserID);
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
            DataSet dsTime = new Access().getTime(2, UserID);
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
            DataSet dsTime = new Access().getTime(3, UserID);
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
                Control x;
                ShowSpinner(out x);
                x.Visible = false;
                TicketViewer ticketViewer = new(sValue, this);
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
                DataSet dsTime = new Access().getTimeByDay(sDate, UserID);
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
                DataSet dsTime = new Access().GetTimeByDateRange(sStart, sEnd, UserID);
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
                DataSet dsTime = new Access().GetTimeByDateRange(sStart, null, UserID);
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

        private void byTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Ok;
            string sValue;
            ShowInputDialog("Please fill in text to search", out Ok, out sValue);

            if (Ok)
            {
                DataSet ds = new Access().GetTimeByString(sValue);
                if (ds.HasRows())
                    InsertIntoTimeTable(ds);
            }
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Reports Report = new Reports(this);
            Report.Show();
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KofiQR QR = new KofiQR();
            QR.Show();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin frmAdmin = new Admin();
            frmAdmin.Show();
        }

        #endregion Nav

        #region DataGridView

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isInitialized)
            {
                int error;
                // Get the edited values from the DataGridView row
                int selectedTimeID = dataGridView1.CurrentRow.GetDataGridViewIntValue("iTimeID");
                string selectedStart = dataGridView1.CurrentRow.GetDataGridViewStringValue("StartTime");
                string selectedEnd = dataGridView1.CurrentRow.GetDataGridViewStringValue("EndTime");
                string editedPtName = dataGridView1.CurrentRow.GetDataGridViewStringValue("tName");
                string editedPtTicketNumber = dataGridView1.CurrentRow.GetDataGridViewStringValue("TicketNumber");
                string editedPtStart = dataGridView1.CurrentRow.GetDataGridViewStringValue("StartTime").ValidateTimeString(out error);
                string editedPtEnd = dataGridView1.CurrentRow.GetDataGridViewStringValue("EndTime").ValidateTimeString(out error);
                string editedTotal = dataGridView1.CurrentRow.GetDataGridViewStringValue("Total").ValidateTimeString(out error);
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
                        PushNofication("Please provide a valid ticket number", NotificationType.Error);
                    }
                    else
                    {
                        new Access().UpdateTime(time);
                    }
                }

                UpdateTotal();
            }
        }

        void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
                PushNofication("Time Deleted", NotificationType.Success);
            }
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn
                && e.RowIndex >= 0 && e.ColumnIndex == 11)
            {
                if (isTaskRunning)
                    lblStop_Click(null, null);

                isTaskRunning = true;

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
            Control lblAction;
            ShowSpinner(out lblAction);
            this.Hide();
            Color bgDark = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            Color bgDarkSecondary = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            Color controlDark = Color.SlateGray;

            if (toggleSwitch1.Checked)
            {
                lblAction.Text = "Setting up DarkMode";
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
                lblAction.Text = "Setting up LightMode";
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
            Color bgDark = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            Color bgDarkSecondary = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            Color controlDark = Color.SlateGray;
            if (isInitialized)
            {
                if (toggleSwitch1.Checked)
                {
                    for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        DataGridViewComboBoxColumn cbTimeType = (dataGridView1.Columns[6] as DataGridViewComboBoxColumn);
                        DataGridViewComboBoxColumn cbTicketType = (dataGridView1.Columns[8] as DataGridViewComboBoxColumn);

                        cbTimeType.FlatStyle = FlatStyle.Flat;
                        cbTicketType.FlatStyle = FlatStyle.Flat;
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

                UpdateTotal();

                List<string> lTimeType = new List<string>();
                List<string> lTicketType = ("Support,Non-Chargeable,Chargeable").Split(',').ToList();

                DataSet dsTypes = new Access().GetTicketTypes();

                if (dsTypes.HasRows())
                {
                    foreach (DataRow row in dsTypes.Tables[0].Rows)
                    {
                        string Name = row.GetDataRowStringValue("Name");
                        lTimeType.Add(Name);
                    }
                }

                for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                {
                    (dataGridView1.Columns[6] as DataGridViewComboBoxColumn).DataSource = lTimeType;
                    (dataGridView1.Columns[8] as DataGridViewComboBoxColumn).DataSource = lTicketType;

                    DataGridViewButtonColumn btnDelete = (dataGridView1.Columns[10] as DataGridViewButtonColumn);
                    DataGridViewButtonColumn btnContinue = (dataGridView1.Columns[11] as DataGridViewButtonColumn);
                    if (btnDelete.Text.IsNullOrEmpty())
                    {
                        btnDelete.Text = "Delete";
                        btnContinue.Text = "Continue";
                    }
                }
            }
        }

        public void generic_DarkMode(Form form, out bool isDarkMode)
        {
            Control x;
            ShowSpinner(out x);
            x.Visible = false;
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
                isDarkMode = false;
            this.Show();
            HideSpinner();
        }

        #endregion DarkMode

        #region RAR Handler

        public void CheckDB()
        {
            string rarFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "DB.rar"));
            string extractPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "DB"));
            string existingDB = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "DB", "Time.mdf"));

            if (!File.Exists(existingDB))
            {
                using (var archive = ArchiveFactory.Open(rarFilePath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(extractPath, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = false
                            });
                        }
                    }

                    new _scriptTool().RunInScripts();
                }
                new _logger().Log(LogType.Info, "Database Extracted");
                new Access().FillSettings();
            }
        }

        #endregion RAR Handler

        #region Notifications

        public void PushNofication(string Message, NotificationType Type)
        {
            new Notifications().SendNotification(Message, Type);
            responseMessage.Text = DateTime.Now.ToString("[hh:mm]") + " " + Message;
        }

        #endregion Notifications

        #region Context Menu

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                iTicketNumber = dataGridView1.Rows[e.RowIndex].GetDataGridViewStringValue("TicketNumber");
            }
            catch { iTicketNumber = null; }
        }

        private void openTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new Access().GetSettingValue(3))
                this.tblTimeContextMenu.Hide();
            else
            {
                if (!iTicketNumber.IsNullOrEmpty())
                {
                    Control x;
                    ShowSpinner(out x);
                    x.Visible = false;
                    TicketViewer ticketViewer = new(iTicketNumber, this);
                    ticketViewer.Show();
                    HideSpinner();
                }
                else
                    this.tblTimeContextMenu.Hide();
            }
        }

        #endregion Context Menu

        #region Loader

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

        #endregion Loader

        #region Mails

        public void sendReminders()
        {
            int iCount = Access.GetUncapturedTimeCount();
            if (iCount > 20 && Access.GetSettingValue(7))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Hello There!" + Environment.NewLine);
                sb.AppendLine($"You have {iCount} uncaptured time records, please make sure to stay up to date on your time capture." + Environment.NewLine);
                sb.AppendLine("Kind regards," + Environment.NewLine + "TimeCapture");
                new _mailer().SendMail("Time Reminders", sb.ToString(), "brayden@codeplex.co.za");
            }
        }

        #endregion Mails
    }
}
