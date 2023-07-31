using Windows.UI.Xaml;

namespace TimeCapture.Forms
{
    public partial class Settings : Form
    {
        public static string root = Directory.GetCurrentDirectory();
        public static string SettingsCsv = Path.Combine(root, "Data", "Settings.csv");
        public List<CSVImport.Settings> lSettings = new List<CSVImport.Settings>();
        public bool isInitialized { get; set; }
        public DB.Access access = new DB.Access();
        public TimeCapture timeCapture { get; set; }
        public Settings(TimeCapture capture)
        {
            InitializeComponent();
            this.Hide();
            capture.ShowSpinner();
            timeCapture = capture;
            isInitialized = false;
            GetSettings();
            SetSettings();
            isInitialized = true;
            bool isDarkMode;
            capture.generic_DarkMode(this, out isDarkMode);
            if (isDarkMode)
            {
                this.label1.BackColor = Color.Gray;
                this.label1.ForeColor = Color.White;
                this.containerControl1.BackColor = Color.Gray;
            }
            capture.HideSpinner();
            this.Show();
            this.FormClosed += OnClose;
        }

        private void OnClose(object sender, FormClosedEventArgs e)
        {
            timeCapture.CheckHidden();
            Dispose();
        }

        public void GetSettings()
        {
            DataSet Settings = access.getsettings();

            if (Settings.HasRows())
            {
                foreach (DataRow row in Settings.Tables[0].Rows)
                {
                    CSVImport.Settings setting = new CSVImport.Settings();
                    setting.ID = row.GetDataRowIntValue("ID");
                    setting.Name = row.GetDataRowStringValue("Name");
                    setting.Value = row.GetDataRowIntValue("Value");
                    setting.Desc = row.GetDataRowStringValue("Description");
                    lSettings.Add(setting);
                }
            }
        }

        public void SetSettings()
        {
            DataSet dsSettings = access.getsettings();
            foreach (DataRow setting in dsSettings.Tables[0].Rows)
            {
                ToolTip toolTip = new ToolTip();
                int ID = setting.GetDataRowIntValue("ID");
                if (ID == 1)
                {
                    cbWarning.Checked = Convert.ToBoolean(setting.GetDataRowIntValue("Value"));
                    toolTip.SetToolTip(cbWarning, setting.GetDataRowStringValue("Description"));
                }
                else if (ID == 2)
                {
                    cbHideDesc.Checked = Convert.ToBoolean(setting.GetDataRowIntValue("Value"));
                    toolTip.SetToolTip(cbHideDesc, setting.GetDataRowStringValue("Description"));
                }
                else if (ID == 3)
                {
                    cbRemoveTicketNo.Checked = Convert.ToBoolean(setting.GetDataRowIntValue("Value"));
                    toolTip.SetToolTip(cbRemoveTicketNo, setting.GetDataRowStringValue("Description"));
                }
                else if (ID == 4)
                {
                    cbIsToasts.Checked = Convert.ToBoolean(setting.GetDataRowIntValue("Value"));
                    toolTip.SetToolTip(cbIsToasts, setting.GetDataRowStringValue("Description"));
                }
                else if (ID == 5)
                {
                    txtUsername.Text = setting.GetDataRowStringValue("Value");
                    toolTip.SetToolTip(txtUsername, setting.GetDataRowStringValue("Description"));
                }
                else if (ID == 6)
                {
                    txtPassword.Text = setting.GetDataRowStringValue("Value");
                    toolTip.SetToolTip(txtPassword, setting.GetDataRowStringValue("Description"));
                }
            }
        }

        public void UpdateSettings()
        {
            List<CSVImport.Settings> settings = new List<CSVImport.Settings>();

            CSVImport.Settings setting = new CSVImport.Settings();

            // Warning
            access.updSettings(1, cbWarning.IsChecked(), "");

            // Desc
            access.updSettings(2, cbHideDesc.IsChecked(), "");

            // Ticket No
            access.updSettings(3, cbRemoveTicketNo.IsChecked(), "");

            // Toasts
            access.updSettings(4, cbIsToasts.IsChecked(), "");

            // Username
            access.updSettings(5, -1, txtUsername.Text);

            // Password
            access.updSettings(6, -1, txtPassword.Text);

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            UpdateSettings();
            timeCapture.CheckHidden();
            Dispose();
        }
    }
}
