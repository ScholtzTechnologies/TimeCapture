using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TimeCapture.utils.CSVImport;

namespace TimeCapture.Forms
{
    public partial class ViewTickets : Form
    {
        public TimeCapture frmCapture { get; set; }
        public ViewTickets(TimeCapture timeCapture)
        {
            InitializeComponent();
            GetTickets();
            frmCapture = timeCapture;
            bool isDarkMode;
            frmCapture.generic_DarkMode(this, out isDarkMode);
            if (isDarkMode)
            {
                Color bgDark = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
                Color bgDarkSecondary = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
                
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

                dataGridView1.BackgroundColor = bgDarkSecondary;
                dataGridView1.GridColor = Color.Black;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Adjust the heading text color if needed
                dataGridView1.EnableHeadersVisualStyles = false; // Disable the default visual styles for the headers
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView1.CellPainting += DarkBorders;
            }
        }

        public void GetTickets()
        {
            DataSet dataSet = new DataSet();
            dataSet = new Access().GetTickets();
            if (dataSet.HasRows())
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    dataGridView1.Rows.Add(row.GetDataRowIntValue("ID"), row.GetDataRowStringValue("Name"), "Delete", "Close");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&    
                e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)
                {
                    int TicketID = dataGridView1.Rows[e.RowIndex].GetDataGridViewIntValue("ID");
                    string Name = dataGridView1.Rows[e.RowIndex].GetDataGridViewStringValue("sName");
                    new Access().DelTicket(TicketID);
                    dataGridView1.Rows[e.RowIndex].Visible = false;
                    frmCapture.getTickets();
                    MessageBox.Show(Name + " was deleted");
                }
                else if (e.ColumnIndex == 3)
                {
                    bool bOk;
                    string sValue;
                    frmCapture.ShowTextAreaInputDialog("Please Provide a solution", out bOk, out sValue);
                    if (bOk && !String.IsNullOrEmpty(sValue))
                    {
                        MessageBox.Show("Not implemented, yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (String.IsNullOrEmpty(sValue) && bOk)
                        MessageBox.Show("Please Provide a solution", "Solution Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
    }
}
