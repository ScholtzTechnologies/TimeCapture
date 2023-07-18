namespace TimeCapture.Forms
{
    partial class TicketViewer
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
            this.webViewer1 = new CustomControls.WebViewer();
            this.SuspendLayout();
            // 
            // webViewer1
            // 
            this.webViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webViewer1.Location = new System.Drawing.Point(12, 12);
            this.webViewer1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webViewer1.Name = "webViewer1";
            this.webViewer1.Size = new System.Drawing.Size(776, 426);
            this.webViewer1.TabIndex = 1;
            // 
            // TicketViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webViewer1);
            this.Name = "TicketViewer";
            this.Text = "TicketViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.WebViewer webViewer1;
    }
}