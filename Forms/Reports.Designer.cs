namespace TimeCapture.Forms
{
    partial class Reports
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
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.lblReportType = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.cbTicket = new System.Windows.Forms.ComboBox();
            this.lblTicket = new System.Windows.Forms.Label();
            this.descTicket = new System.Windows.Forms.Label();
            this.descTicket2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formsPlot1.Location = new System.Drawing.Point(-3, 67);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(801, 387);
            this.formsPlot1.TabIndex = 0;
            // 
            // lblReportType
            // 
            this.lblReportType.AutoSize = true;
            this.lblReportType.Location = new System.Drawing.Point(12, 9);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Size = new System.Drawing.Size(89, 20);
            this.lblReportType.TabIndex = 1;
            this.lblReportType.Text = "Report Type";
            // 
            // cbReportType
            // 
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Location = new System.Drawing.Point(107, 6);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(151, 28);
            this.cbReportType.TabIndex = 2;
            // 
            // cbTicket
            // 
            this.cbTicket.FormattingEnabled = true;
            this.cbTicket.Location = new System.Drawing.Point(107, 40);
            this.cbTicket.Name = "cbTicket";
            this.cbTicket.Size = new System.Drawing.Size(151, 28);
            this.cbTicket.TabIndex = 3;
            // 
            // lblTicket
            // 
            this.lblTicket.AutoSize = true;
            this.lblTicket.Location = new System.Drawing.Point(53, 43);
            this.lblTicket.Name = "lblTicket";
            this.lblTicket.Size = new System.Drawing.Size(48, 20);
            this.lblTicket.TabIndex = 4;
            this.lblTicket.Text = "Ticket";
            // 
            // descTicket
            // 
            this.descTicket.AutoSize = true;
            this.descTicket.Location = new System.Drawing.Point(264, 43);
            this.descTicket.Name = "descTicket";
            this.descTicket.Size = new System.Drawing.Size(213, 20);
            this.descTicket.TabIndex = 5;
            this.descTicket.Text = "To add more tickets to the list, ";
            // 
            // descTicket2
            // 
            this.descTicket2.AutoSize = true;
            this.descTicket2.Location = new System.Drawing.Point(264, 63);
            this.descTicket2.Name = "descTicket2";
            this.descTicket2.Size = new System.Drawing.Size(426, 20);
            this.descTicket2.TabIndex = 6;
            this.descTicket2.Text = "change the value of the dropdown and the ticket will be added";
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.descTicket2);
            this.Controls.Add(this.descTicket);
            this.Controls.Add(this.lblTicket);
            this.Controls.Add(this.cbTicket);
            this.Controls.Add(this.cbReportType);
            this.Controls.Add(this.lblReportType);
            this.Controls.Add(this.formsPlot1);
            this.MinimumSize = new System.Drawing.Size(818, 497);
            this.Name = "Reports";
            this.Text = "Reports";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private Label lblReportType;
        private ComboBox cbReportType;
        private ComboBox cbTicket;
        private Label lblTicket;
        private Label descTicket;
        private Label descTicket2;
    }
}