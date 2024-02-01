using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeCapture;
using Tulpep.NotificationWindow;

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
        public void SendNotification(string Message, NotificationType Type)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.ShowGrip = false;
            popup.HeaderColor = Color.Black;
            popup.BodyColor = Color.Black;
            popup.BorderColor = Color.Black;
            popup.Size = new System.Drawing.Size(300, 75);

            if (Type == NotificationType.Success)
                popup.Image = Properties.Resources.Success;
            else if (Type == NotificationType.Error)
                popup.Image = Properties.Resources.Error;
            else if (Type == NotificationType.Info)
                popup.Image = Properties.Resources.Info;
            else if (Type == NotificationType.Logo)
                popup.Image = Properties.Resources.TimeIcon_60x_White;

            popup.TitleColor = Color.White;
            popup.TitleText = "Time Capture";
            popup.ContentColor = Color.Gray;
            popup.ContentText = Message;
            popup.Popup();
        }
    }

    public enum Status
    {
        Busy,
        Idle,
        Error
    }

    #endregion Classes

}
