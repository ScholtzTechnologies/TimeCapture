namespace TimeCapture.Forms
{
    partial class Mailer
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
            this.dgvTags = new System.Windows.Forms.DataGridView();
            this.colTagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSQL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tglUseTags = new CustomControls.ToggleSwitch();
            this.lblUseTags = new System.Windows.Forms.Label();
            this.rtbTo = new System.Windows.Forms.RichTextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblToComment = new System.Windows.Forms.Label();
            this.lblCC = new System.Windows.Forms.Label();
            this.txtCC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tglSeperateMails = new CustomControls.ToggleSwitch();
            this.lblSeperateMails = new System.Windows.Forms.Label();
            this.rtbBody = new System.Windows.Forms.RichTextBox();
            this.lblBody = new System.Windows.Forms.Label();
            this.lblBodyComment = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.roundedProgressBar1 = new CustomControls.RoundedProgressBar();
            this.roundedProgressBar2 = new CustomControls.RoundedProgressBar();
            this.roundedProgressBar3 = new CustomControls.RoundedProgressBar();
            this.roundedProgressBar4 = new CustomControls.RoundedProgressBar();
            this.lnkHTMLHelp = new System.Windows.Forms.LinkLabel();
            this.roundedProgressBar5 = new CustomControls.RoundedProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTags
            // 
            this.dgvTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTagName,
            this.colSQL});
            this.dgvTags.Location = new System.Drawing.Point(12, 250);
            this.dgvTags.Name = "dgvTags";
            this.dgvTags.RowHeadersWidth = 51;
            this.dgvTags.RowTemplate.Height = 29;
            this.dgvTags.Size = new System.Drawing.Size(415, 155);
            this.dgvTags.TabIndex = 0;
            // 
            // colTagName
            // 
            this.colTagName.HeaderText = "Tag Name";
            this.colTagName.MinimumWidth = 6;
            this.colTagName.Name = "colTagName";
            this.colTagName.Width = 125;
            // 
            // colSQL
            // 
            this.colSQL.HeaderText = "SQL";
            this.colSQL.MinimumWidth = 6;
            this.colSQL.Name = "colSQL";
            this.colSQL.Width = 125;
            // 
            // tglUseTags
            // 
            this.tglUseTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tglUseTags.AutoSize = true;
            this.tglUseTags.Location = new System.Drawing.Point(12, 222);
            this.tglUseTags.MaximumSize = new System.Drawing.Size(50, 22);
            this.tglUseTags.MinimumSize = new System.Drawing.Size(35, 22);
            this.tglUseTags.Name = "tglUseTags";
            this.tglUseTags.Size = new System.Drawing.Size(50, 22);
            this.tglUseTags.TabIndex = 1;
            this.tglUseTags.Text = "toggleSwitch1";
            this.tglUseTags.UseVisualStyleBackColor = true;
            this.tglUseTags.CheckedChanged += new System.EventHandler(this.tglUseTags_CheckedChanged);
            // 
            // lblUseTags
            // 
            this.lblUseTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUseTags.AutoSize = true;
            this.lblUseTags.Location = new System.Drawing.Point(68, 222);
            this.lblUseTags.Name = "lblUseTags";
            this.lblUseTags.Size = new System.Drawing.Size(65, 20);
            this.lblUseTags.TabIndex = 2;
            this.lblUseTags.Text = "Use tags";
            // 
            // rtbTo
            // 
            this.rtbTo.Location = new System.Drawing.Point(12, 83);
            this.rtbTo.Name = "rtbTo";
            this.rtbTo.Size = new System.Drawing.Size(415, 54);
            this.rtbTo.TabIndex = 3;
            this.rtbTo.Text = "";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(14, 60);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(25, 20);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To";
            // 
            // lblToComment
            // 
            this.lblToComment.AutoSize = true;
            this.lblToComment.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblToComment.Location = new System.Drawing.Point(45, 60);
            this.lblToComment.Name = "lblToComment";
            this.lblToComment.Size = new System.Drawing.Size(151, 20);
            this.lblToComment.TabIndex = 5;
            this.lblToComment.Text = "Seperate using \';\' or \',\'";
            // 
            // lblCC
            // 
            this.lblCC.AutoSize = true;
            this.lblCC.Location = new System.Drawing.Point(12, 140);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(27, 20);
            this.lblCC.TabIndex = 6;
            this.lblCC.Text = "CC";
            // 
            // txtCC
            // 
            this.txtCC.Location = new System.Drawing.Point(12, 163);
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(415, 27);
            this.txtCC.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 31);
            this.label1.TabIndex = 8;
            this.label1.Text = "TimeCapture Mailer";
            // 
            // tglSeperateMails
            // 
            this.tglSeperateMails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tglSeperateMails.AutoSize = true;
            this.tglSeperateMails.Location = new System.Drawing.Point(12, 196);
            this.tglSeperateMails.MaximumSize = new System.Drawing.Size(50, 22);
            this.tglSeperateMails.MinimumSize = new System.Drawing.Size(35, 22);
            this.tglSeperateMails.Name = "tglSeperateMails";
            this.tglSeperateMails.Size = new System.Drawing.Size(50, 22);
            this.tglSeperateMails.TabIndex = 9;
            this.tglSeperateMails.Text = "toggleSwitch1";
            this.tglSeperateMails.UseVisualStyleBackColor = true;
            // 
            // lblSeperateMails
            // 
            this.lblSeperateMails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSeperateMails.AutoSize = true;
            this.lblSeperateMails.Location = new System.Drawing.Point(68, 198);
            this.lblSeperateMails.Name = "lblSeperateMails";
            this.lblSeperateMails.Size = new System.Drawing.Size(235, 20);
            this.lblSeperateMails.TabIndex = 10;
            this.lblSeperateMails.Text = "Each to address recieves own mail";
            // 
            // rtbBody
            // 
            this.rtbBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbBody.Location = new System.Drawing.Point(433, 140);
            this.rtbBody.Name = "rtbBody";
            this.rtbBody.Size = new System.Drawing.Size(355, 298);
            this.rtbBody.TabIndex = 11;
            this.rtbBody.Text = "";
            // 
            // lblBody
            // 
            this.lblBody.AutoSize = true;
            this.lblBody.Location = new System.Drawing.Point(433, 117);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(43, 20);
            this.lblBody.TabIndex = 12;
            this.lblBody.Text = "Body";
            // 
            // lblBodyComment
            // 
            this.lblBodyComment.AutoSize = true;
            this.lblBodyComment.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblBodyComment.Location = new System.Drawing.Point(482, 117);
            this.lblBodyComment.Name = "lblBodyComment";
            this.lblBodyComment.Size = new System.Drawing.Size(212, 20);
            this.lblBodyComment.TabIndex = 13;
            this.lblBodyComment.Text = "HTML Can be used in the body";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(433, 83);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(355, 27);
            this.txtSubject.TabIndex = 14;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(433, 60);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(58, 20);
            this.lblSubject.TabIndex = 15;
            this.lblSubject.Text = "Subject";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(694, 9);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(94, 29);
            this.btnSend.TabIndex = 16;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(694, 44);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 29);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // roundedProgressBar1
            // 
            this.roundedProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedProgressBar1.Location = new System.Drawing.Point(257, 409);
            this.roundedProgressBar1.Name = "roundedProgressBar1";
            this.roundedProgressBar1.ProgressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.roundedProgressBar1.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.roundedProgressBar1.ProgressFont = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.roundedProgressBar1.ProgressFontColor = System.Drawing.Color.Black;
            this.roundedProgressBar1.Size = new System.Drawing.Size(29, 29);
            this.roundedProgressBar1.TabIndex = 18;
            // 
            // roundedProgressBar2
            // 
            this.roundedProgressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedProgressBar2.Location = new System.Drawing.Point(292, 409);
            this.roundedProgressBar2.Name = "roundedProgressBar2";
            this.roundedProgressBar2.ProgressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.roundedProgressBar2.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.roundedProgressBar2.ProgressFont = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.roundedProgressBar2.ProgressFontColor = System.Drawing.Color.Black;
            this.roundedProgressBar2.Size = new System.Drawing.Size(29, 29);
            this.roundedProgressBar2.TabIndex = 19;
            // 
            // roundedProgressBar3
            // 
            this.roundedProgressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedProgressBar3.Location = new System.Drawing.Point(327, 409);
            this.roundedProgressBar3.Name = "roundedProgressBar3";
            this.roundedProgressBar3.ProgressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.roundedProgressBar3.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.roundedProgressBar3.ProgressFont = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.roundedProgressBar3.ProgressFontColor = System.Drawing.Color.Black;
            this.roundedProgressBar3.Size = new System.Drawing.Size(29, 29);
            this.roundedProgressBar3.TabIndex = 20;
            // 
            // roundedProgressBar4
            // 
            this.roundedProgressBar4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedProgressBar4.Location = new System.Drawing.Point(362, 409);
            this.roundedProgressBar4.Name = "roundedProgressBar4";
            this.roundedProgressBar4.ProgressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.roundedProgressBar4.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.roundedProgressBar4.ProgressFont = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.roundedProgressBar4.ProgressFontColor = System.Drawing.Color.Black;
            this.roundedProgressBar4.Size = new System.Drawing.Size(29, 29);
            this.roundedProgressBar4.TabIndex = 21;
            // 
            // lnkHTMLHelp
            // 
            this.lnkHTMLHelp.AutoSize = true;
            this.lnkHTMLHelp.Location = new System.Drawing.Point(694, 117);
            this.lnkHTMLHelp.Name = "lnkHTMLHelp";
            this.lnkHTMLHelp.Size = new System.Drawing.Size(16, 20);
            this.lnkHTMLHelp.TabIndex = 22;
            this.lnkHTMLHelp.TabStop = true;
            this.lnkHTMLHelp.Text = "?";
            this.lnkHTMLHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHTMLHelp_LinkClicked);
            // 
            // roundedProgressBar5
            // 
            this.roundedProgressBar5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedProgressBar5.Location = new System.Drawing.Point(397, 409);
            this.roundedProgressBar5.Name = "roundedProgressBar5";
            this.roundedProgressBar5.ProgressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.roundedProgressBar5.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.roundedProgressBar5.ProgressFont = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.roundedProgressBar5.ProgressFontColor = System.Drawing.Color.Black;
            this.roundedProgressBar5.Size = new System.Drawing.Size(29, 29);
            this.roundedProgressBar5.TabIndex = 23;
            // 
            // Mailer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.roundedProgressBar5);
            this.Controls.Add(this.lnkHTMLHelp);
            this.Controls.Add(this.roundedProgressBar4);
            this.Controls.Add(this.roundedProgressBar3);
            this.Controls.Add(this.roundedProgressBar2);
            this.Controls.Add(this.roundedProgressBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.lblBodyComment);
            this.Controls.Add(this.lblBody);
            this.Controls.Add(this.rtbBody);
            this.Controls.Add(this.lblSeperateMails);
            this.Controls.Add(this.tglSeperateMails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCC);
            this.Controls.Add(this.lblCC);
            this.Controls.Add(this.lblToComment);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.rtbTo);
            this.Controls.Add(this.lblUseTags);
            this.Controls.Add(this.tglUseTags);
            this.Controls.Add(this.dgvTags);
            this.MinimumSize = new System.Drawing.Size(818, 497);
            this.Name = "Mailer";
            this.Text = "Mailer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dgvTags;
        private CustomControls.ToggleSwitch tglUseTags;
        private Label lblUseTags;
        private RichTextBox rtbTo;
        private Label lblTo;
        private Label lblToComment;
        private Label lblCC;
        private TextBox txtCC;
        private Label label1;
        private CustomControls.ToggleSwitch tglSeperateMails;
        private Label lblSeperateMails;
        private RichTextBox rtbBody;
        private Label lblBody;
        private Label lblBodyComment;
        private TextBox txtSubject;
        private Label lblSubject;
        private Button btnSend;
        private Button btnCancel;
        private CustomControls.RoundedProgressBar roundedProgressBar1;
        private CustomControls.RoundedProgressBar roundedProgressBar2;
        private CustomControls.RoundedProgressBar roundedProgressBar3;
        private CustomControls.RoundedProgressBar roundedProgressBar4;
        private LinkLabel lnkHTMLHelp;
        private CustomControls.RoundedProgressBar roundedProgressBar5;
        private DataGridViewTextBoxColumn colTagName;
        private DataGridViewTextBoxColumn colSQL;
    }
}