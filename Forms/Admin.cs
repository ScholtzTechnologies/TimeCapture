using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Uno.Extensions;

namespace TimeCapture.Forms
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            GetUsers();
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void GetUsers()
        {
            DataSet dsUsers = new Access().GetUsers();
            foreach (DataRow row in dsUsers.Tables[0].Rows)
            {
                dataGridView1.Rows.Add(
                    row.GetDataRowIntValue("ID"), row.GetDataRowStringValue("Name"), row.GetDataRowStringValue("Password"),
                    row.GetDataRowStringValue("Email"), "Delete", "Make Admin", "Reset Password"
                );
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn
                && e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                new Access().DeleteUser(dataGridView1.Rows[e.RowIndex].GetDataGridViewIntValue("ID"));
                dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                new Notifications().SendNotification("User Deleted", NotificationType.Success);
            }
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn
                && e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                new Access().MakeAdmin(dataGridView1.Rows[e.RowIndex].GetDataGridViewIntValue("ID"));
                new Notifications().SendNotification("User Updated", NotificationType.Success);
            }
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn
                && e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                int iUserID = dataGridView1.CurrentRow.GetDataGridViewIntValue("ID");
                string Name = dataGridView1.CurrentRow.GetDataGridViewStringValue("sName");
                bool bOK;
                string sValue;
                
                TimeCapture time = new TimeCapture();
                time.ShowInputDialog("Please Provide new password for " + Name, out bOK, out sValue);

                if (bOK && !sValue.IsNullOrEmpty())
                    new Access().UpdateUsersPassword(iUserID, sValue);

                dataGridView1.Rows.Clear();
                GetUsers();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int iUserID = dataGridView1.CurrentRow.GetDataGridViewIntValue("ID");
            string Name = dataGridView1.CurrentRow.GetDataGridViewStringValue("sName");
            string Email = dataGridView1.CurrentRow.GetDataGridViewStringValue("sEmail");
            new Access().UpdateUser(iUserID, Name, Email);
            new Notifications().SendNotification("User Updated", NotificationType.Success);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!txtName.Text.IsNullOrEmpty() && !txtEmail.Text.IsNullOrEmpty() && !txtPassword.Text.IsNullOrEmpty())
            {
                string Name = txtName.Text,
                Password = txtPassword.Text,
                Email = txtEmail.Text;

                new Access().Register(Name, Password, Email);
                dataGridView1.Rows.Clear();
                GetUsers();
            }
            else
            {
                MessageBox.Show("Please complete above fields");
            }
        }
    }
}
