using System.Text.RegularExpressions;

namespace TimeCapture.utils
{
    public static class Extensions
    {
        #region Checks
        public static bool HasRows(this DataSet ds)
        {
            try
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }

        public static string FixString(this string sString)
        {
            if (String.IsNullOrEmpty(sString))
            {
                return "";
            }
            else
            {
                return sString.Replace("'", "''");
            }
        }

        public static int IsChecked(this CheckBox checkBox)
        {
            if (checkBox.CheckState == CheckState.Checked)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static string ValidateTimeString(this string TimeStamp, out int errorType)
        {
            ErrorType e = new();
            errorType = e.None;
            List<string> invalidCharacters = new Invalid().Characters();
            CultureInfo enUS = new CultureInfo("en-US");
            DateTime x;

            if (TimeStamp.Length == 5)
            {
                foreach (var Char in invalidCharacters)
                {
                    TimeStamp.Replace(Char.ToCharArray()[0], ':');
                }
                try
                {
                    DateTime.TryParseExact(TimeStamp, "hh:mm", enUS, DateTimeStyles.None, out x);
                }
                catch
                {
                    errorType = e.Format;
                }
            }
            else
                errorType = e.Length;

            return TimeStamp;
        }

        public static bool isNullOrEmpty(this string sString)
        {
            if (String.IsNullOrEmpty(sString) || String.IsNullOrWhiteSpace(sString))
                return true;
            else
                return false;
        }

        public static bool isTrue(this string sString)
        {
            if (!sString.isNullOrEmpty())
            {
                if (sString == "1" || sString.ToLower() == "y" || sString.ToLower() == "yes")
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        #endregion Checks

        #region DataRow Extensions
        public static string GetDataRowStringValue(this DataRow dataRow, string sColumn)
        {
            string sValue = "";
            if (dataRow[sColumn] != null)
            {
                try
                {
                    sValue = dataRow[sColumn].ToString();
                }
                catch
                {

                }
            }
            return sValue;
        }

        public static int GetDataRowIntValue(this DataRow dataRow, string sColumn)
        {
            int iValue = 0;
            if (dataRow[sColumn] != null)
            {
                try
                {
                    iValue = Convert.ToInt32(dataRow[sColumn].ToString());
                }
                catch
                {

                }
            }
            return iValue;
        }

        public static bool GetDataRowBoolValue(this DataRow dataRow, string sColumn)
        {
            try
            {
                bool iValue = false;
                if (dataRow[sColumn] != null)
                {
                    try
                    {
                        try
                        {
                            int x = Convert.ToInt32(dataRow[sColumn].ToString());
                            if (x == 1)
                            {
                                iValue = true;
                            }
                        }
                        catch
                        {
                            if (dataRow[sColumn].ToString().ToLower() == "true")
                            {
                                iValue = true;
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                return iValue;
            }
            catch
            {
                return false;
            }
        }

        #endregion DataRow Extensions

        #region Data Grid View Extensions
        public static string GetDataGridViewStringValue(this DataGridViewRow dvRow, string sColumn)
        {
            if (dvRow.Cells[sColumn].Value == null)
            {
                return "";
            }
            else
            {
                return dvRow.Cells[sColumn].Value.ToString();
            }
        }

        public static int GetDataGridViewIntValue(this DataGridViewRow dvRow, string sColumn)
        {
            if (dvRow.Cells[sColumn].Value == null)
            {
                return 0;
            }
            else
            {
                if (dvRow.Cells[sColumn].Value.ToString().ToLower() == "true")
                {
                    return 1;
                }
                else if (dvRow.Cells[sColumn].Value.ToString().ToLower() == "true")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(dvRow.Cells[sColumn].Value.ToString());
                }
            }
        }

        public static int GetDataGridViewCheckBoxAsInt(this DataGridViewRow dvRow, string sColumn)
        {
            if (dvRow.Cells[sColumn].Value == null)
            {
                return 0;
            }
            else
            {
                if (dvRow.Cells[sColumn].Value.ToString().ToLower() == "true")
                {
                    return 1;
                }
                else if (dvRow.Cells[sColumn].Value.ToString().ToLower() == "false")
                {
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static DataGridViewRow FindTextInGrid(this DataGridView dataGridView, string sText, string sColumn)
        {
            DataGridViewRow row = null;

            foreach (DataGridViewRow xrow in dataGridView.Rows)
            {
                if (xrow.GetDataGridViewStringValue(sColumn).Contains(sText))
                {
                    row = xrow;
                    break;
                }
            }

            return row;
        }
        #endregion Data Grid View Extensions

        #region Conversions

        public static int ConvertTimeTypeToInt(string sTimeType)
        {
            int Type = 1;

            switch (sTimeType)
            {
                case var t when t.Contains(TimeType.General):
                    Type = 1;
                    break;
                case var t when t.Contains(TimeType.Investigation):
                    Type = 2;
                    break;
                case var t when t.Contains(TimeType.Report):
                    Type = 3;
                    break;
                case var t when t.Contains(TimeType.Bug):
                    Type = 4;
                    break;
                case var t when t.Contains(TimeType.Dev.Split(',')[0]) || t.Contains(TimeType.Dev.Split(',')[1]) || t.Contains(TimeType.Dev.Split(',')[2]):
                    Type = 5;
                    break;
                case var t when t.Contains(TimeType.Meetings.Split(',')[0]) || t.Contains(TimeType.Meetings.Split(',')[1]):
                    Type = 10;
                    break;
                case var t when t.Contains(TimeType.Training):
                    Type = 12;
                    break;
                case var t when t.Contains(TimeType.Testing.Split(',')[0]) || t.Contains(TimeType.Testing.Split(',')[1]):
                    Type = 13;
                    break;
            }
            return Type;
        }

        public static bool ToBool(this int i)
        {
            if (i == 1)
                return true;
            else
                return false;
        }

        public static void Hide(this Control control)
        {
            control.Visible = false;
        }

        public static void Show(this Control control)
        {
            control.Visible = true;
        }

        /// <summary>
        ///  Encases the provided text in the default TimeCapture HTML body
        /// </summary>
        public static string EncaseMailBody(this string sBody)
        {
            string sMailCasingFile = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\utils\\files\\MailCasing.html").ToString();
            string sFileText = System.IO.File.ReadAllText(sMailCasingFile).ToString();
            string sEncasedBody = sFileText.Replace("[body]", sBody);

            return sEncasedBody;
        }

        public static string ToHTMLTable(this string sText)
        {
            // 1, 1 == td in one row
            // 1, 2, 3 == seperate rows
            string sNewText = sText.Replace("\n", "");
            Dictionary<int, List<string>> rows = new Dictionary<int, List<string>>();
            string[] parts = Regex.Split(sNewText, @"(\d+)");

            int currentRow = 0;

            foreach (var part in parts)
            {
                if (int.TryParse(part, out int rowNum))
                {
                    currentRow = rowNum;
                    if (!rows.ContainsKey(currentRow))
                    {
                        rows[currentRow] = new List<string>();
                    }
                }
                else if (!string.IsNullOrWhiteSpace(part))
                {
                    if (rows.Count > 0)
                    {
                        rows[currentRow].Add(part);
                    }
                    else
                    {
                        MessageBox.Show("Invalid format for table, please ensure table contains numbers for rows");
                        return sText;
                    }
                }
            }

            string htmlTable = "<table>\n";

            foreach (var row in rows)
            {
                htmlTable += "\t<tr>\n";
                foreach (var cell in row.Value)
                {
                    htmlTable += $"\t\t<td>{cell}</td>\n";
                }
                htmlTable += "\t</tr>\n";
            }

            htmlTable += "</table>";

            return htmlTable.Replace("/n", "/r");
        }

        #endregion Conversions
    }

    #region Classes

    public class ErrorType
    {
        public int None = 0;
        public int Format = 1;
        public int Length = 2;
    }

    public class NoteTreeNode : TreeNode
    {
        public int NodeID { get; set; }
        public string Date { get; set; }
        public int ParentID { get; set; }
        public NoteTreeNode(string text, int id, string sDate, int iParentID) : base(text)
        {
            NodeID = id;
            Name = text;
            Date = sDate;
            ParentID = iParentID;
        }
    }

    public class Invalid
    {
        public List<string> Characters()
        {
            List<string> lCharacters = new();
            lCharacters.Add(";");
            lCharacters.Add("'");
            lCharacters.Add('"'.ToString());
            lCharacters.Add("/");
            lCharacters.Add(".");
            lCharacters.Add(",");
            return lCharacters;
        }
    }

    public enum NotificationType
    {
        Success,
        Error,
        Info,
        Logo
    }

    public class Notifications
    {
        public void SendNotification(string Message, NotificationType Type, string additionalMessage = "")
        {
            NotifyIcon notifyIcon = new NotifyIcon();

            string sIconDir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Icons"));

            switch (Type)
            {
                case NotificationType.Logo:
                    notifyIcon.Icon = new System.Drawing.Icon(Path.Combine(sIconDir, "TimeIcon.ico"));
                    notifyIcon.Text = "TimeCapture";
                    break;
                case NotificationType.Success:
                    notifyIcon.Icon = new System.Drawing.Icon(Path.Combine(sIconDir, "Success.ico"));
                    notifyIcon.Text = "TimeCapture - Success";
                    break;
                case NotificationType.Error:
                    notifyIcon.Icon = new System.Drawing.Icon(Path.Combine(sIconDir, "Error.ico"));
                    notifyIcon.Text = "TimeCapture - Error";
                    break;
                case NotificationType.Info:
                    notifyIcon.Icon = new System.Drawing.Icon(Path.Combine(sIconDir, "Info.ico"));
                    notifyIcon.Text = "TimeCapture - Info";
                    break;
            }

            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");
            exitMenuItem.Click += (sender, e) => { Application.Exit(); };
            contextMenu.Items.Add(exitMenuItem);

            notifyIcon.ContextMenuStrip = contextMenu;

            notifyIcon.Visible = true;

            notifyIcon.BalloonTipTitle = Message;
            if (!additionalMessage.isNullOrEmpty())
                notifyIcon.BalloonTipText = additionalMessage;
            else
                notifyIcon.BalloonTipText = " ";

            notifyIcon.ShowBalloonTip(5000);
        }
    }

    public enum Status
    {
        Busy,
        Idle,
        Error
    }

    #endregion Classes

    public static class Loader
    {
        public static Status oStatus = new Status();

        /// <summary>
        ///     Uses the custom control RoundedProgressBars to initialize a loading animation. Requires that the Rounded Progress Bars be left with their default names or be named in a orderable way.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static async Task Start(Form form)
        {
            List<ProgressBar> progressBars = new List<ProgressBar>();
            oStatus = Status.Busy;

            foreach (Control control in form.Controls)
            {
                if (control is CustomControls.RoundedProgressBar pb)
                {
                    pb.Invoke((MethodInvoker)delegate
                    {
                        pb.Maximum = 1;
                    });
                    progressBars.Add(pb);
                }
            }
            progressBars = progressBars.OrderBy(x => x.Name).ToList();

            await Task.Run(() =>
            {
                int i = 0;

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

        public static void Stop(bool isSuccess)
        {
            oStatus = isSuccess ? Status.Idle : Status.Error;
        }
    }
}
