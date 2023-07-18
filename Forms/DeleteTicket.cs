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
    public partial class DeleteTicket : Form
    {
        public TimeCapture frmCapture { get; set; }
        public DeleteTicket(TimeCapture timeCapture)
        {
            InitializeComponent();
            GetTickets();
            frmCapture = timeCapture;
        }

        public void GetTickets()
        {
            DataSet dataSet = new DataSet();
            dataSet = new Access().GetTickets();
            if (dataSet.HasRows())
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    dataGridView1.Rows.Add(row.GetDataRowIntValue("ID"), row.GetDataRowStringValue("Name"));
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
            {
                int TicketID = dataGridView1.Rows[e.RowIndex].GetDataGridViewIntValue("ID");
                string Name = dataGridView1.Rows[e.RowIndex].GetDataGridViewStringValue("Name");
                new Access().DelTicket(TicketID);
                dataGridView1.Rows.Clear();
                GetTickets();
                frmCapture.getTickets();
                MessageBox.Show(Name + " was deleted");
            }
        }
    }
}
