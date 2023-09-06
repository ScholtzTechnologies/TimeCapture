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
