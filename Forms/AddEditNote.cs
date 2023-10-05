using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeCapture.DB;

namespace TimeCapture.Forms
{
    public partial class AddEditNote : Form
    {
        public int NoteID { get; set; }
        public int ParentID { get; set; }
        public string sName { get; set; }
        public string Date { get; set; }
        public Access access = new Access();
        public Notes frmNotes { get; set; }
        TimeCapture time { get; set; }
        public AddEditNote(int iNoteID, int parentID, string name, string date, Notes notes)
        {
            InitializeComponent();
            NoteID = iNoteID;
            sName = name;
            Date = date;
            ParentID = parentID;
            frmNotes = notes;
            
            txtEditName.Text = sName;
            lblSure.Text = "Are you sure you wish to delete " + sName + "?";


            time = notes.time;
            bool isDarkMode;
            time.generic_DarkMode(this, out isDarkMode);
            if (isDarkMode)
            {
                this.tabAdd.BackColor = Color.Black;
                this.tabDelete.BackColor = Color.Black;
                this.tabEdit.BackColor = Color.Black;

                List<Control> lControls = new List<Control>();
                foreach (Control control in tabAdd.Controls)
                {
                    lControls.Add(control);
                }
                foreach (Control control in tabEdit.Controls)
                {
                    lControls.Add(control);
                }
                foreach (Control control in tabDelete.Controls)
                {
                    lControls.Add(control);
                }

                foreach (Control control in lControls)
                {
                    Color bgDark = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
                    Color bgDarkSecondary = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
                    Color controlDark = Color.SlateGray;

                    if (control is Button button)
                    {
                        button.FlatStyle = FlatStyle.Flat; // Set the FlatStyle to Flat
                        button.BackColor = bgDarkSecondary;
                        button.FlatAppearance.BorderColor = bgDark; // Set the border color to the same as the background color
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
            }
        }

        #region Add
        private void btnSave_Click(object sender, EventArgs e)
        {
            access.AddNote(-1, txtName.Text.FixString(), "", DateTime.Now.ToString("dd MMM yyyy"), NoteID);
            frmNotes.updateNotes();
            Dispose();
        }
        #endregion

        #region universal
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            access.UpdateNoteName(NoteID, txtEditName.Text.ToString().FixString());
            frmNotes.updateNotes();
            Dispose();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            bool isDeleted = access.DeleteNote(NoteID);
            if (isDeleted)
                frmNotes.updateNotes();
            Dispose();
        }
    }
}
