namespace TimeCapture.Forms
{
    partial class AddEditNote
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
            this.tabCollection = new System.Windows.Forms.TabControl();
            this.tabAdd = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tabEdit = new System.Windows.Forms.TabPage();
            this.txtEditName = new System.Windows.Forms.TextBox();
            this.btnEditCancel = new System.Windows.Forms.Button();
            this.btnEditSave = new System.Windows.Forms.Button();
            this.lblEditName = new System.Windows.Forms.Label();
            this.tabDelete = new System.Windows.Forms.TabPage();
            this.lblSure = new System.Windows.Forms.Label();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.tabCollection.SuspendLayout();
            this.tabAdd.SuspendLayout();
            this.tabEdit.SuspendLayout();
            this.tabDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCollection
            // 
            this.tabCollection.Controls.Add(this.tabAdd);
            this.tabCollection.Controls.Add(this.tabEdit);
            this.tabCollection.Controls.Add(this.tabDelete);
            this.tabCollection.Location = new System.Drawing.Point(12, 12);
            this.tabCollection.Name = "tabCollection";
            this.tabCollection.SelectedIndex = 0;
            this.tabCollection.Size = new System.Drawing.Size(238, 100);
            this.tabCollection.TabIndex = 0;
            // 
            // tabAdd
            // 
            this.tabAdd.Controls.Add(this.btnSave);
            this.tabAdd.Controls.Add(this.btnCancel);
            this.tabAdd.Controls.Add(this.lblName);
            this.tabAdd.Controls.Add(this.txtName);
            this.tabAdd.Location = new System.Drawing.Point(4, 24);
            this.tabAdd.Name = "tabAdd";
            this.tabAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdd.Size = new System.Drawing.Size(230, 72);
            this.tabAdd.TabIndex = 0;
            this.tabAdd.Text = "Add";
            this.tabAdd.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(68, 35);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(149, 35);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(74, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 23);
            this.txtName.TabIndex = 0;
            // 
            // tabEdit
            // 
            this.tabEdit.Controls.Add(this.txtEditName);
            this.tabEdit.Controls.Add(this.btnEditCancel);
            this.tabEdit.Controls.Add(this.btnEditSave);
            this.tabEdit.Controls.Add(this.lblEditName);
            this.tabEdit.Location = new System.Drawing.Point(4, 24);
            this.tabEdit.Name = "tabEdit";
            this.tabEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tabEdit.Size = new System.Drawing.Size(230, 72);
            this.tabEdit.TabIndex = 1;
            this.tabEdit.Text = "Edit";
            this.tabEdit.UseVisualStyleBackColor = true;
            // 
            // txtEditName
            // 
            this.txtEditName.Location = new System.Drawing.Point(74, 6);
            this.txtEditName.Name = "txtEditName";
            this.txtEditName.Size = new System.Drawing.Size(150, 23);
            this.txtEditName.TabIndex = 5;
            // 
            // btnEditCancel
            // 
            this.btnEditCancel.Location = new System.Drawing.Point(149, 35);
            this.btnEditCancel.Name = "btnEditCancel";
            this.btnEditCancel.Size = new System.Drawing.Size(75, 23);
            this.btnEditCancel.TabIndex = 3;
            this.btnEditCancel.Text = "Cancel";
            this.btnEditCancel.UseVisualStyleBackColor = true;
            this.btnEditCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEditSave
            // 
            this.btnEditSave.Location = new System.Drawing.Point(68, 35);
            this.btnEditSave.Name = "btnEditSave";
            this.btnEditSave.Size = new System.Drawing.Size(75, 23);
            this.btnEditSave.TabIndex = 2;
            this.btnEditSave.Text = "Save";
            this.btnEditSave.UseVisualStyleBackColor = true;
            this.btnEditSave.Click += new System.EventHandler(this.btnEditSave_Click);
            // 
            // lblEditName
            // 
            this.lblEditName.AutoSize = true;
            this.lblEditName.Location = new System.Drawing.Point(6, 9);
            this.lblEditName.Name = "lblEditName";
            this.lblEditName.Size = new System.Drawing.Size(39, 15);
            this.lblEditName.TabIndex = 0;
            this.lblEditName.Text = "Name";
            // 
            // tabDelete
            // 
            this.tabDelete.Controls.Add(this.lblSure);
            this.tabDelete.Controls.Add(this.btnNo);
            this.tabDelete.Controls.Add(this.btnYes);
            this.tabDelete.Location = new System.Drawing.Point(4, 24);
            this.tabDelete.Name = "tabDelete";
            this.tabDelete.Padding = new System.Windows.Forms.Padding(3);
            this.tabDelete.Size = new System.Drawing.Size(230, 72);
            this.tabDelete.TabIndex = 2;
            this.tabDelete.Text = "Delete";
            this.tabDelete.UseVisualStyleBackColor = true;
            // 
            // lblSure
            // 
            this.lblSure.AutoSize = true;
            this.lblSure.Location = new System.Drawing.Point(7, 12);
            this.lblSure.Name = "lblSure";
            this.lblSure.Size = new System.Drawing.Size(217, 15);
            this.lblSure.TabIndex = 2;
            this.lblSure.Text = "Are you sure you want to delete [Name]";
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(123, 30);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(31, 30);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // AddEditNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 121);
            this.Controls.Add(this.tabCollection);
            this.MaximumSize = new System.Drawing.Size(277, 160);
            this.MinimumSize = new System.Drawing.Size(277, 160);
            this.Name = "AddEditNote";
            this.Text = "AddEditNote";
            this.tabCollection.ResumeLayout(false);
            this.tabAdd.ResumeLayout(false);
            this.tabAdd.PerformLayout();
            this.tabEdit.ResumeLayout(false);
            this.tabEdit.PerformLayout();
            this.tabDelete.ResumeLayout(false);
            this.tabDelete.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabCollection;
        private TabPage tabAdd;
        private TabPage tabEdit;
        private TabPage tabDelete;
        private Label lblName;
        private TextBox txtName;
        private Button btnSave;
        private Button btnCancel;
        private TextBox txtEditName;
        private Button btnEditCancel;
        private Button btnEditSave;
        private Label lblEditName;
        private Label lblSure;
        private Button btnNo;
        private Button btnYes;
    }
}