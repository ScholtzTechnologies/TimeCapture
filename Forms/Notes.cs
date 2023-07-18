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
using TimeCapture.utils;
using Uno.Extensions;
using Uno.Extensions.Specialized;

namespace TimeCapture.Forms
{
    public partial class Notes : Form
    {
        public int iTaskID { get; set; }
        public static string root = Directory.GetCurrentDirectory();
        public static string fNotes = Path.Combine(root, "Data", "Notes.txt");
        public static string fTasks = Path.Combine(root, "Data", "Todo.csv");
        public DB.Access Access = new DB.Access();
        public List<CSVImport.Tasks> lTasks = new List<CSVImport.Tasks>();
        public Notes()
        {
            InitializeComponent();

            dgTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            getTasks();
            getNotes();
            getNoteByID(0);

            this.btnSave.MouseClick += btnSave_Click;
            treeNotes.NodeMouseDoubleClick += qAddEdit;
            treeNotes.NodeMouseClick += GetNote;
            dgTasks.RowsAdded += SetNewTaskID;

            txtParentID.Visible = false;
            txtNoteID.Visible = false;
        }

        public void qAddEdit(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node is NoteTreeNode noteNode)
            {
                int NoteID = noteNode.NodeID;
                string name = noteNode.Name;
                int ParentID = noteNode.ParentID;
                string Date = noteNode.Date;
                AddEditNote addEditNote = new(NoteID, ParentID, name.FixString(), Date, this);
                addEditNote.Show();
            }
        }

        public void GetNote(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node is NoteTreeNode noteNode)
            {
                getNoteByID(noteNode.NodeID);
            }
        }

        private void SetNewTaskID(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rowIndex = e.RowIndex;
            for (int i = 0; i < e.RowCount; i++)
            {
                iTaskID++;
                dgTasks.Rows[rowIndex + i].Cells["TaskID"].Value = iTaskID;
            }
        }

        public void getTasks()
        {
            DataSet dsTasks = Access.GetTasks();
            if (dsTasks.HasRows())
            {
                foreach (DataRow row in dsTasks.Tables[0].Rows)
                {
                    dgTasks.Rows.Add(row.GetDataRowIntValue("TaskID"), row.GetDataRowStringValue("Task"), row.GetDataRowBoolValue("Status"));
                }
            }
            DataRow rowWithGreatestNoteID = dsTasks.Tables[0].Rows.Cast<DataRow>()
                .OrderByDescending(row => row.Field<int>("TaskID"))
                .FirstOrDefault();

            if (rowWithGreatestNoteID != null)
            {
                iTaskID = rowWithGreatestNoteID.Field<int>("TaskID");
            }
        }
        
        public void getNotes()
        {
            NoteTreeNode root = new NoteTreeNode("Root", 0, "", 0);
            DataSet dsNotes = Access.getNotes();

            Dictionary<int, NoteTreeNode> nodeDictionary = new Dictionary<int, NoteTreeNode>();
            nodeDictionary.Add(0, root); // Add root node to the dictionary

            if (dsNotes.HasRows())
            {
                foreach (DataRow row in dsNotes.Tables[0].Rows)
                {
                    int noteID = row.GetDataRowIntValue("NoteID");
                    int parentID = row.GetDataRowIntValue("ParentID");
                    string name = row.GetDataRowStringValue("Name");
                    string date = row.GetDataRowStringValue("Date");

                    NoteTreeNode node = new NoteTreeNode(name, noteID, date, parentID);
                    nodeDictionary.Add(noteID, node); // Add the current node to the dictionary

                    if (nodeDictionary.ContainsKey(parentID))
                    {
                        NoteTreeNode parentNode = nodeDictionary[parentID];
                        parentNode.Nodes.Add(node); // Add the current node to its parent node
                    }
                }
            }

            treeNotes.Nodes.Add(root);
        }

        public void getNoteByID(int NoteID)
        {
            Access access = new Access();
            DataSet dsNote = access.GetNoteByNoteID(NoteID);
            if (dsNote.HasRows())
            {
                DataRow note = dsNote.Tables[0].Rows[0];
                txtNoteID.Text = note.GetDataRowStringValue("NoteID");
                txtParentID.Text = note.GetDataRowStringValue("ParentID");
                rtxtNote.Text = note.GetDataRowStringValue("Note");
                lblNote.Text = "Note: " + note.GetDataRowStringValue("Name");
            }
        }

        public void updateNotes()
        {
            treeNotes.Nodes.Clear();
            getNotes();
        }

        public void updateNotes(int i)
        {
            treeNotes.Nodes.Clear();
            getNoteByID(0);
            getNotes();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Note = rtxtNote.Text;
            int NoteID = Convert.ToInt32(txtNoteID.Text);
            int ParentID = Convert.ToInt32(txtParentID.Text);
            Access access = new();
            access.saveNote(NoteID, Name, Note, DateTime.Now.ToString("dd MMM yyyy"), ParentID);
        }

        private void btnSaveTask_Click(object sender, EventArgs e)
        {
            Access access = new();
            foreach (DataGridViewRow row in dgTasks.Rows)
            {
                if (!row.GetDataGridViewStringValue("Task").IsNullOrEmpty())
                {
                    access.SaveTask(row.GetDataGridViewIntValue("TaskID"), row.GetDataGridViewStringValue("Task"), row.GetDataGridViewCheckBoxAsInt("Status"));
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dgTasks.SelectedRows[0];
                int TaskID = Convert.ToInt32(selectedRow.Cells["TaskID"].Value);
                Access access = new();
                access.DeleteTask(TaskID);
                dgTasks.Rows.Remove(selectedRow);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Index"))
                {
                    MessageBox.Show("Please select a row");
                }
            }
        }
    }
}
