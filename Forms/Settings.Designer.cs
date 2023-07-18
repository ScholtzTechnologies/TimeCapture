namespace TimeCapture.Forms
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.containerControl1 = new System.Windows.Forms.ContainerControl();
            this.cbRemoveTicketNo = new System.Windows.Forms.CheckBox();
            this.cbIsToasts = new System.Windows.Forms.CheckBox();
            this.cbHideDesc = new System.Windows.Forms.CheckBox();
            this.cbWarning = new System.Windows.Forms.CheckBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.containerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(110, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // containerControl1
            // 
            this.containerControl1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.containerControl1.Controls.Add(this.label1);
            this.containerControl1.Location = new System.Drawing.Point(0, 0);
            this.containerControl1.Name = "containerControl1";
            this.containerControl1.Size = new System.Drawing.Size(703, 67);
            this.containerControl1.TabIndex = 6;
            this.containerControl1.Text = "containerControl1";
            // 
            // cbRemoveTicketNo
            // 
            this.cbRemoveTicketNo.AutoSize = true;
            this.cbRemoveTicketNo.Location = new System.Drawing.Point(10, 110);
            this.cbRemoveTicketNo.Name = "cbRemoveTicketNo";
            this.cbRemoveTicketNo.Size = new System.Drawing.Size(150, 19);
            this.cbRemoveTicketNo.TabIndex = 7;
            this.cbRemoveTicketNo.Text = "Remove Ticket Number";
            this.cbRemoveTicketNo.UseVisualStyleBackColor = true;
            // 
            // cbIsToasts
            // 
            this.cbIsToasts.AutoSize = true;
            this.cbIsToasts.Location = new System.Drawing.Point(10, 87);
            this.cbIsToasts.Name = "cbIsToasts";
            this.cbIsToasts.Size = new System.Drawing.Size(66, 19);
            this.cbIsToasts.TabIndex = 8;
            this.cbIsToasts.Text = "IsToasts";
            this.cbIsToasts.UseVisualStyleBackColor = true;
            // 
            // cbHideDesc
            // 
            this.cbHideDesc.AutoSize = true;
            this.cbHideDesc.Location = new System.Drawing.Point(10, 135);
            this.cbHideDesc.Name = "cbHideDesc";
            this.cbHideDesc.Size = new System.Drawing.Size(132, 19);
            this.cbHideDesc.TabIndex = 9;
            this.cbHideDesc.Text = "Remove Description";
            this.cbHideDesc.UseVisualStyleBackColor = true;
            // 
            // cbWarning
            // 
            this.cbWarning.AutoSize = true;
            this.cbWarning.Location = new System.Drawing.Point(10, 160);
            this.cbWarning.Name = "cbWarning";
            this.cbWarning.Size = new System.Drawing.Size(148, 19);
            this.cbWarning.TabIndex = 10;
            this.cbWarning.Text = "Show warning on close";
            this.cbWarning.UseVisualStyleBackColor = true;
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(278, 185);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(66, 22);
            this.btnAccept.TabIndex = 12;
            this.btnAccept.Text = "Ok";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(185, 108);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(159, 23);
            this.txtUsername.TabIndex = 13;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(185, 156);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(159, 23);
            this.txtPassword.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Username for Selenium";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "Password for Selenium";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 215);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.cbWarning);
            this.Controls.Add(this.cbHideDesc);
            this.Controls.Add(this.cbIsToasts);
            this.Controls.Add(this.cbRemoveTicketNo);
            this.Controls.Add(this.containerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(372, 254);
            this.MinimumSize = new System.Drawing.Size(372, 254);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.containerControl1.ResumeLayout(false);
            this.containerControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ContainerControl containerControl1;
        private System.Windows.Forms.CheckBox cbWarning;
        private System.Windows.Forms.CheckBox cbHideDesc;
        private System.Windows.Forms.CheckBox cbIsToasts;
        private System.Windows.Forms.CheckBox cbRemoveTicketNo;
        private System.Windows.Forms.Button btnAccept;
        private Label label3;
        private Label label2;
        private TextBox txtPassword;
        private TextBox txtUsername;
    }
}