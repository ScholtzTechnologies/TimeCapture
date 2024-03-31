namespace TimeCapture.Forms
{
    partial class Notes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.treeNotes = new System.Windows.Forms.TreeView();
            this.rtxtNote = new System.Windows.Forms.RichTextBox();
            this.dgTasks = new System.Windows.Forms.DataGridView();
            this.TaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Task = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblTasks = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtParentID = new System.Windows.Forms.TextBox();
            this.btnSaveTask = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtNoteID = new System.Windows.Forms.TextBox();
            this.tglAutosave = new CustomControls.ToggleSwitch();
            this.lblAutosave = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(696, 503);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 31);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Note";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // treeNotes
            // 
            this.treeNotes.Location = new System.Drawing.Point(14, 44);
            this.treeNotes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeNotes.Name = "treeNotes";
            this.treeNotes.Size = new System.Drawing.Size(283, 125);
            this.treeNotes.TabIndex = 1;
            // 
            // rtxtNote
            // 
            this.rtxtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtNote.Location = new System.Drawing.Point(304, 44);
            this.rtxtNote.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtxtNote.Name = "rtxtNote";
            this.rtxtNote.Size = new System.Drawing.Size(477, 449);
            this.rtxtNote.TabIndex = 2;
            this.rtxtNote.Text = "";
            // 
            // dgTasks
            // 
            this.dgTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskID,
            this.Task,
            this.Status});
            this.dgTasks.Location = new System.Drawing.Point(14, 207);
            this.dgTasks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgTasks.Name = "dgTasks";
            this.dgTasks.RowHeadersWidth = 51;
            this.dgTasks.RowTemplate.Height = 25;
            this.dgTasks.Size = new System.Drawing.Size(283, 288);
            this.dgTasks.TabIndex = 3;
            // 
            // TaskID
            // 
            this.TaskID.HeaderText = "TaskID";
            this.TaskID.MinimumWidth = 6;
            this.TaskID.Name = "TaskID";
            this.TaskID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TaskID.Visible = false;
            this.TaskID.Width = 125;
            // 
            // Task
            // 
            this.Task.HeaderText = "Task";
            this.Task.MinimumWidth = 6;
            this.Task.Name = "Task";
            this.Task.Width = 125;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Status.Width = 125;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNotes.Location = new System.Drawing.Point(14, 12);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(68, 28);
            this.lblNotes.TabIndex = 4;
            this.lblNotes.Text = "Notes";
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTasks.Location = new System.Drawing.Point(14, 175);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(62, 28);
            this.lblTasks.TabIndex = 5;
            this.lblTasks.Text = "Tasks";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(304, 503);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(385, 31);
            this.progressBar1.TabIndex = 6;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNote.Location = new System.Drawing.Point(304, 12);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(135, 28);
            this.lblNote.TabIndex = 7;
            this.lblNote.Text = "[Note Name]";
            // 
            // txtParentID
            // 
            this.txtParentID.Location = new System.Drawing.Point(754, 12);
            this.txtParentID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtParentID.Name = "txtParentID";
            this.txtParentID.Size = new System.Drawing.Size(27, 27);
            this.txtParentID.TabIndex = 8;
            // 
            // btnSaveTask
            // 
            this.btnSaveTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveTask.Location = new System.Drawing.Point(211, 503);
            this.btnSaveTask.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveTask.Name = "btnSaveTask";
            this.btnSaveTask.Size = new System.Drawing.Size(86, 31);
            this.btnSaveTask.TabIndex = 9;
            this.btnSaveTask.Text = "Save Tasks";
            this.btnSaveTask.UseVisualStyleBackColor = true;
            this.btnSaveTask.Click += new System.EventHandler(this.btnSaveTask_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(137, 503);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 31);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtNoteID
            // 
            this.txtNoteID.Location = new System.Drawing.Point(720, 12);
            this.txtNoteID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNoteID.Name = "txtNoteID";
            this.txtNoteID.Size = new System.Drawing.Size(27, 27);
            this.txtNoteID.TabIndex = 11;
            // 
            // tglAutosave
            // 
            this.tglAutosave.AutoSize = true;
            this.tglAutosave.Location = new System.Drawing.Point(81, 508);
            this.tglAutosave.MaximumSize = new System.Drawing.Size(50, 22);
            this.tglAutosave.MinimumSize = new System.Drawing.Size(35, 22);
            this.tglAutosave.Name = "tglAutosave";
            this.tglAutosave.Size = new System.Drawing.Size(50, 22);
            this.tglAutosave.TabIndex = 12;
            this.tglAutosave.Text = "toggleSwitch1";
            this.tglAutosave.UseVisualStyleBackColor = true;
            this.tglAutosave.CheckedChanged += new System.EventHandler(this.tglAutosave_CheckedChanged);
            // 
            // lblAutosave
            // 
            this.lblAutosave.AutoSize = true;
            this.lblAutosave.Location = new System.Drawing.Point(12, 508);
            this.lblAutosave.Name = "lblAutosave";
            this.lblAutosave.Size = new System.Drawing.Size(70, 20);
            this.lblAutosave.TabIndex = 13;
            this.lblAutosave.Text = "Autosave";
            // 
            // Notes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 549);
            this.Controls.Add(this.lblAutosave);
            this.Controls.Add(this.tglAutosave);
            this.Controls.Add(this.txtNoteID);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSaveTask);
            this.Controls.Add(this.txtParentID);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblTasks);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.dgTasks);
            this.Controls.Add(this.rtxtNote);
            this.Controls.Add(this.treeNotes);
            this.Controls.Add(this.btnSave);
            this.Name = "Notes";
            this.Text = "Notes";
            ((System.ComponentModel.ISupportInitialize)(this.dgTasks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnSave;
        private TreeView treeNotes;
        private RichTextBox rtxtNote;
        private DataGridView dgTasks;
        private Label lblNotes;
        private Label lblTasks;
        private ProgressBar progressBar1;
        private Label lblNote;
        private TextBox txtParentID;
        private DataGridViewTextBoxColumn TaskID;
        private DataGridViewTextBoxColumn Task;
        private DataGridViewCheckBoxColumn Status;
        private Button btnSaveTask;
        private Button btnDelete;
        private TextBox txtNoteID;
        private CustomControls.ToggleSwitch tglAutosave;
        private Label lblAutosave;
    }
}