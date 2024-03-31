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
        #region Properties

        public int iTaskID { get; set; }
        public static string root = Directory.GetCurrentDirectory();
        public static string fNotes = Path.Combine(root, "Data", "Notes.txt");
        public static string fTasks = Path.Combine(root, "Data", "Todo.csv");
        public bool isAutosave { get; set; }
        public DB.Access Access = new DB.Access();
        public List<CSVImport.Tasks> lTasks = new List<CSVImport.Tasks>();
        public bool isDarkMode { get; set; }
        public TimeCapture time { get; set; }
        private static System.Timers.Timer TimerConn = null;

        #endregion Properties

        public Notes(TimeCapture timeCapture)
        {
            InitializeComponent();

            time = timeCapture;
            dgTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            getTasks();
            getNotes();
            getNoteByID(0);

            this.btnSave.MouseClick += btnSave_Click;
            treeNotes.NodeMouseDoubleClick += qAddEdit;
            treeNotes.NodeMouseClick += GetNote;
            treeNotes.AllowDrop = true;
            dgTasks.RowsAdded += SetNewTaskID;

            treeNotes.ItemDrag += new ItemDragEventHandler(NoteNodeDrag);
            treeNotes.DragEnter += new DragEventHandler(NoteNodeDragEnter);
            treeNotes.DragOver += new DragEventHandler(NoteNodeDragOver);
            treeNotes.DragDrop += new DragEventHandler(NoteNodeDragDrop);

            txtParentID.Visible = false;
            txtNoteID.Visible = false;

            bool isDark;
            timeCapture.generic_DarkMode(this, out isDark);
            isDarkMode = isDark;

            if (isDark)
                dgTasks.CellPainting += DarkBorders;
            else
                dgTasks.CellPainting += LightBorders;

            Color bgDark = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            Color bgDarkSecondary = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            Color controlDark = Color.SlateGray;

            if (isDark)
            {
                foreach (DataGridViewRow row in dgTasks.Rows)
                {
                    row.DefaultCellStyle.BackColor = bgDark;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }

                foreach (DataGridViewButtonCell buttonCell in dgTasks.Rows.Cast<DataGridViewRow>()
                                .SelectMany(row => row.Cells.OfType<DataGridViewButtonCell>()))
                {
                    buttonCell.Style.BackColor = bgDark;
                    buttonCell.Style.ForeColor = Color.White;
                }
                dgTasks.BackgroundColor = bgDarkSecondary;
                dgTasks.GridColor = Color.Black;
                dgTasks.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
                dgTasks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Adjust the heading text color if needed
                dgTasks.EnableHeadersVisualStyles = false; // Disable the default visual styles for the headers
            }
        }

        #region Get

        public void GetNote(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node is NoteTreeNode noteNode)
            {
                getNoteByID(noteNode.NodeID);
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

            if (NoteID == 0 || NoteID == -1)
            {
                lblNote.Text = "";
                rtxtNote.Text = "This is not a note. This is where all the notes come from.";
            }
            else
                rtxtNote.Enabled = true;
        }

        #endregion Get

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

        private void SetNewTaskID(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rowIndex = e.RowIndex;
            for (int i = 0; i < e.RowCount; i++)
            {
                iTaskID++;
                dgTasks.Rows[rowIndex + i].Cells["TaskID"].Value = iTaskID;
            }
        }

        #region Notes

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
            string Note = rtxtNote.Text.ToString();
            int NoteID = Convert.ToInt32(txtNoteID.Text);
            int ParentID = Convert.ToInt32(txtParentID.Text);
            Access access = new();
            access.saveNote(NoteID, Name, Note, DateTime.Now.ToString("dd MMM yyyy"), ParentID);
        }

        #endregion Notes

        #region Tasks

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
            dgTasks.Rows.Clear();
            getTasks();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dgTasks.SelectedRows[0];
                int TaskID = Convert.ToInt32(selectedRow.Cells["TaskID"].Value);
                Access access = new();
                if (TaskID != -1)
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

        #endregion Tasks

        #region TreeView

        private void NoteNodeDrag(object sender, ItemDragEventArgs e)
        {
            // Move the dragged node when the left mouse button is used.  
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }

            // Copy the dragged node when the right mouse button is used.  
            else if (e.Button == MouseButtons.Right)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        private void NoteNodeDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void NoteNodeDragOver(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse position.  
            Point targetPoint = treeNotes.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.  
            treeNotes.SelectedNode = treeNotes.GetNodeAt(targetPoint);
        }

        private void NoteNodeDragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.  
            Point targetPoint = treeNotes.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.  
            TreeNode targetNode = treeNotes.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.  
            NoteTreeNode draggedNode = (NoteTreeNode)e.Data.GetData(typeof(NoteTreeNode));

            // Confirm that the node at the drop location is not   
            // the dragged node or a descendant of the dragged node.  
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                // If it is a move operation, remove the node from its current   
                // location and add it to the node at the drop location.  
                if (e.Effect == DragDropEffects.Move)
                {
                    bool isSuccess = new Access().MoveNote(draggedNode.NodeID, draggedNode.ParentID);
                    if (isSuccess)
                    {
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);
                    }
                    else
                        MessageBox.Show("Failed to move note");
                }

                // Expand the node at the location   
                // to show the dropped node.  
                targetNode.Expand();
            }
        }

        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.  
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node,   
            // call the ContainsNode method recursively using the parent of   
            // the second node.  
            return ContainsNode(node1, node2.Parent);
        }

        #endregion TreeView

        #region DarkMode

        private void PaintRows(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (isDarkMode)
            {
                for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                {
                    dgTasks.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
                    dgTasks.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
            }
            else
            {
                for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                {
                    dgTasks.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    dgTasks.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
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

        private void LightBorders(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                using (Pen borderPen = new Pen(Color.Gray, 2))
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(borderPen, rect);
                }

                e.Handled = true;
            }
        }

        #endregion DarkMode

        private void tglAutosave_CheckedChanged(object sender, EventArgs e)
        {
            isAutosave = tglAutosave.Checked;
            if (isAutosave)
            {
                new Notifications().SendNotification("Autosave On", NotificationType.Info, "The app will now save Tasks and notes every 30 seconds");
                Autosave();
            }
            else
            {
                new Notifications().SendNotification("Autosave Off", NotificationType.Info);
            }
        }

        private async Task Autosave()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                while (isAutosave)
                {
                    if (TimerConn == null)
                    {
                        if (TimerConn != null)
                        {
                            TimerConn.Enabled = false;
                        }
                        TimerConn = new System.Timers.Timer();
                        TimerConn.Interval = 30000;
                        TimerConn.Elapsed += TimerConn_Elapsed;

                        TimerConn.Enabled = true;
                    }
                }
                TimerConn = null;
            });
        }

        private void TimerConn_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            btnSaveTask_Click(null, null);
            new Notifications().SendNotification("Tasks Saved", NotificationType.Success);
        }
    }
}
