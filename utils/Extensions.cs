using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeCapture;

namespace TimeCapture.utils
{
    public static class Extensions
    {
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

        public static string sDate(this DateTime sString, int? Format)
        {
            if (Format == TimeFormat.DDMMMYYYY)
            {
                return sString.ToString("dd MMM yyyy");
            }
            else if (Format == TimeFormat.HHMM)
            {
                return sString.ToString("HH:mm");
            }
            else if (Format == TimeFormat.DDMMMYYYYHHMM)
            {
                return sString.ToString("dd MMM yyyy HH:mm");
            }
            else if (String.IsNullOrEmpty(sString.ToString()))
            {
                return DateTime.Now.ToString();
            }
            else
            {
                return sString.ToString();
            }
        }

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

        public static int ConvertTimeTypeToInt(string sTimeType)
        {
            int Type = 1;
            if (sTimeType.Contains(TimeType.General))
            {
                Type = 1;
            }
            else if (sTimeType.Contains(TimeType.Investigation))
            {
                Type = 2;
            }
            else if (sTimeType.Contains(TimeType.Report))
            {
                Type = 3;
            }
            else if (sTimeType.Contains(TimeType.Bug))
            {
                Type = 4;
            }
            else if (sTimeType.Contains(TimeType.Dev.Split(',')[0]) || sTimeType.Contains(TimeType.Dev.Split(',')[1]) || sTimeType.Contains(TimeType.Dev.Split(',')[2]))
            {
                Type = 5;
            }
            else if (sTimeType.Contains(TimeType.Meetings.Split(',')[0]) || sTimeType.Contains(TimeType.Meetings.Split(',')[1]))
            {
                Type = 10;
            }
            else if (sTimeType.Contains(TimeType.Training))
            {
                Type = 12;
            }
            else if (sTimeType.Contains(TimeType.Testing.Split(',')[0]) || sTimeType.Contains(TimeType.Testing.Split(',')[1]))
            {
                Type = 13;
            }
            return Type;
        }

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
}
