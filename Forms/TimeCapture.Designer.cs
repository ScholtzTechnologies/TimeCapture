namespace TimeCapture
{
    partial class TimeCapture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeCapture));
            this.Heading = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.exportProgress = new System.Windows.Forms.ProgressBar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iTimeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TicketNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TicketType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Button = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Continue = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblPlay = new System.Windows.Forms.Label();
            this.lblStop = new System.Windows.Forms.Label();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.lblTimeStarted = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.drpType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.rbNonCharge = new System.Windows.Forms.RadioButton();
            this.rbCharge = new System.Windows.Forms.RadioButton();
            this.rbSupport = new System.Windows.Forms.RadioButton();
            this.containerControl1 = new System.Windows.Forms.ContainerControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadAllResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncapturedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capturedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byDateRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ticketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.txtTicketNo = new System.Windows.Forms.ComboBox();
            this.btnAddTicket = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnClear = new System.Windows.Forms.Button();
            this.responseMessage = new System.Windows.Forms.TextBox();
            this.UpdCurrent = new System.Windows.Forms.Button();
            this.txtDesc = new System.Windows.Forms.RichTextBox();
            this.btnCaptureTime = new System.Windows.Forms.Button();
            this.btnDelTicket = new System.Windows.Forms.Button();
            this.lblDarkMode = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new CustomControls.ToggleSwitch();
            this.txtTotalTime = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.containerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Heading
            // 
            this.Heading.AutoSize = true;
            this.Heading.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Heading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Heading.Location = new System.Drawing.Point(94, 49);
            this.Heading.Name = "Heading";
            this.Heading.Size = new System.Drawing.Size(160, 29);
            this.Heading.TabIndex = 1;
            this.Heading.Text = "Time Capture";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(11, 125);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 27);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblName.Location = new System.Drawing.Point(11, 101);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 18);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDesc.Location = new System.Drawing.Point(11, 236);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(83, 18);
            this.lblDesc.TabIndex = 7;
            this.lblDesc.Text = "Description";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(11, 523);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 29);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close App";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSettings.Location = new System.Drawing.Point(11, 485);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(101, 29);
            this.btnSettings.TabIndex = 18;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(255, 523);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(101, 29);
            this.btnExport.TabIndex = 21;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // exportProgress
            // 
            this.exportProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportProgress.Location = new System.Drawing.Point(11, 559);
            this.exportProgress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exportProgress.Name = "exportProgress";
            this.exportProgress.Size = new System.Drawing.Size(343, 32);
            this.exportProgress.TabIndex = 22;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTimeID,
            this.tName,
            this.TicketNumber,
            this.StartTime,
            this.EndTime,
            this.Total,
            this.TimeType,
            this.Description,
            this.TicketType,
            this.Date,
            this.Button,
            this.Continue});
            this.dataGridView1.Location = new System.Drawing.Point(359, 13);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(523, 539);
            this.dataGridView1.TabIndex = 23;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // iTimeID
            // 
            this.iTimeID.HeaderText = "iTimeID";
            this.iTimeID.MinimumWidth = 6;
            this.iTimeID.Name = "iTimeID";
            this.iTimeID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iTimeID.Visible = false;
            this.iTimeID.Width = 125;
            // 
            // tName
            // 
            this.tName.HeaderText = "Name";
            this.tName.MinimumWidth = 6;
            this.tName.Name = "tName";
            this.tName.Width = 125;
            // 
            // TicketNumber
            // 
            this.TicketNumber.HeaderText = "Ticket Number";
            this.TicketNumber.MinimumWidth = 6;
            this.TicketNumber.Name = "TicketNumber";
            this.TicketNumber.Width = 125;
            // 
            // StartTime
            // 
            this.StartTime.HeaderText = "Start Time";
            this.StartTime.MinimumWidth = 6;
            this.StartTime.Name = "StartTime";
            this.StartTime.Width = 125;
            // 
            // EndTime
            // 
            this.EndTime.HeaderText = "End Time";
            this.EndTime.MinimumWidth = 6;
            this.EndTime.Name = "EndTime";
            this.EndTime.Width = 125;
            // 
            // Total
            // 
            this.Total.HeaderText = "Total Time";
            this.Total.MinimumWidth = 6;
            this.Total.Name = "Total";
            this.Total.Width = 125;
            // 
            // TimeType
            // 
            this.TimeType.HeaderText = "Time Type";
            this.TimeType.MinimumWidth = 6;
            this.TimeType.Name = "TimeType";
            this.TimeType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TimeType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TimeType.Width = 125;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.Width = 125;
            // 
            // TicketType
            // 
            this.TicketType.HeaderText = "TicketType";
            this.TicketType.MinimumWidth = 6;
            this.TicketType.Name = "TicketType";
            this.TicketType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TicketType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TicketType.Width = 125;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 6;
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 125;
            // 
            // Button
            // 
            this.Button.HeaderText = "Button";
            this.Button.MinimumWidth = 6;
            this.Button.Name = "Button";
            this.Button.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Button.Width = 125;
            // 
            // Continue
            // 
            this.Continue.HeaderText = "Continue";
            this.Continue.MinimumWidth = 6;
            this.Continue.Name = "Continue";
            this.Continue.Width = 125;
            // 
            // lblPlay
            // 
            this.lblPlay.AutoSize = true;
            this.lblPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPlay.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblPlay.Location = new System.Drawing.Point(267, 285);
            this.lblPlay.Name = "lblPlay";
            this.lblPlay.Size = new System.Drawing.Size(41, 39);
            this.lblPlay.TabIndex = 24;
            this.lblPlay.Text = "▶︎";
            this.lblPlay.Click += new System.EventHandler(this.lblPlay_Click);
            // 
            // lblStop
            // 
            this.lblStop.AutoSize = true;
            this.lblStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStop.ForeColor = System.Drawing.Color.Crimson;
            this.lblStop.Location = new System.Drawing.Point(314, 285);
            this.lblStop.Name = "lblStop";
            this.lblStop.Size = new System.Drawing.Size(41, 39);
            this.lblStop.TabIndex = 25;
            this.lblStop.Text = "■";
            this.lblStop.Click += new System.EventHandler(this.lblStop_Click);
            // 
            // txtCurrent
            // 
            this.txtCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCurrent.Location = new System.Drawing.Point(227, 432);
            this.txtCurrent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new System.Drawing.Size(127, 27);
            this.txtCurrent.TabIndex = 26;
            this.txtCurrent.TextChanged += new System.EventHandler(this.txtCurrent_TextChanged);
            // 
            // lblCurrent
            // 
            this.lblCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(224, 409);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(110, 20);
            this.lblCurrent.TabIndex = 27;
            this.lblCurrent.Text = "Current Activity";
            // 
            // txtStartTime
            // 
            this.txtStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtStartTime.Location = new System.Drawing.Point(227, 488);
            this.txtStartTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(127, 27);
            this.txtStartTime.TabIndex = 28;
            // 
            // lblTimeStarted
            // 
            this.lblTimeStarted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTimeStarted.AutoSize = true;
            this.lblTimeStarted.Location = new System.Drawing.Point(224, 464);
            this.lblTimeStarted.Name = "lblTimeStarted";
            this.lblTimeStarted.Size = new System.Drawing.Size(94, 20);
            this.lblTimeStarted.TabIndex = 29;
            this.lblTimeStarted.Text = "Time Started";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(209, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 18);
            this.label1.TabIndex = 31;
            this.label1.Text = "Ticket No";
            // 
            // drpType
            // 
            this.drpType.FormattingEnabled = true;
            this.drpType.Location = new System.Drawing.Point(11, 189);
            this.drpType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.drpType.Name = "drpType";
            this.drpType.Size = new System.Drawing.Size(194, 28);
            this.drpType.TabIndex = 32;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblType.Location = new System.Drawing.Point(11, 165);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(40, 18);
            this.lblType.TabIndex = 33;
            this.lblType.Text = "Type";
            // 
            // rbNonCharge
            // 
            this.rbNonCharge.AutoSize = true;
            this.rbNonCharge.Location = new System.Drawing.Point(213, 164);
            this.rbNonCharge.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbNonCharge.Name = "rbNonCharge";
            this.rbNonCharge.Size = new System.Drawing.Size(140, 24);
            this.rbNonCharge.TabIndex = 8;
            this.rbNonCharge.TabStop = true;
            this.rbNonCharge.Text = "Non-Chargeable";
            this.rbNonCharge.UseVisualStyleBackColor = true;
            this.rbNonCharge.CheckedChanged += new System.EventHandler(this.rbNonCharge_CheckedChanged);
            // 
            // rbCharge
            // 
            this.rbCharge.AutoSize = true;
            this.rbCharge.Location = new System.Drawing.Point(213, 197);
            this.rbCharge.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbCharge.Name = "rbCharge";
            this.rbCharge.Size = new System.Drawing.Size(106, 24);
            this.rbCharge.TabIndex = 10;
            this.rbCharge.TabStop = true;
            this.rbCharge.Text = "Chargeable";
            this.rbCharge.UseVisualStyleBackColor = true;
            this.rbCharge.CheckedChanged += new System.EventHandler(this.rbCharge_CheckedChanged);
            // 
            // rbSupport
            // 
            this.rbSupport.AutoSize = true;
            this.rbSupport.Location = new System.Drawing.Point(213, 231);
            this.rbSupport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbSupport.Name = "rbSupport";
            this.rbSupport.Size = new System.Drawing.Size(83, 24);
            this.rbSupport.TabIndex = 9;
            this.rbSupport.TabStop = true;
            this.rbSupport.Text = "Support";
            this.rbSupport.UseVisualStyleBackColor = true;
            this.rbSupport.CheckedChanged += new System.EventHandler(this.rbSupport_CheckedChanged);
            // 
            // containerControl1
            // 
            this.containerControl1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.containerControl1.Controls.Add(this.pictureBox1);
            this.containerControl1.Controls.Add(this.Heading);
            this.containerControl1.Controls.Add(this.menuStrip1);
            this.containerControl1.Location = new System.Drawing.Point(0, -1);
            this.containerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.containerControl1.Name = "containerControl1";
            this.containerControl1.Size = new System.Drawing.Size(746, 99);
            this.containerControl1.TabIndex = 34;
            this.containerControl1.Text = "containerControl1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(28, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Size = new System.Drawing.Size(60, 63);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAllResultsToolStripMenuItem,
            this.importToolStripMenuItem,
            this.notesToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.tsAdmin});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(746, 30);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadAllResultsToolStripMenuItem
            // 
            this.loadAllResultsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.uncapturedToolStripMenuItem,
            this.capturedToolStripMenuItem,
            this.byDayToolStripMenuItem,
            this.byDateRangeToolStripMenuItem,
            this.fromDateToolStripMenuItem,
            this.byTextToolStripMenuItem});
            this.loadAllResultsToolStripMenuItem.Name = "loadAllResultsToolStripMenuItem";
            this.loadAllResultsToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.loadAllResultsToolStripMenuItem.Text = "Load Results";
            this.loadAllResultsToolStripMenuItem.ToolTipText = "Load all stored time";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // uncapturedToolStripMenuItem
            // 
            this.uncapturedToolStripMenuItem.Name = "uncapturedToolStripMenuItem";
            this.uncapturedToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.uncapturedToolStripMenuItem.Text = "Uncaptured";
            this.uncapturedToolStripMenuItem.Click += new System.EventHandler(this.uncapturedToolStripMenuItem_Click);
            // 
            // capturedToolStripMenuItem
            // 
            this.capturedToolStripMenuItem.Name = "capturedToolStripMenuItem";
            this.capturedToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.capturedToolStripMenuItem.Text = "Captured";
            this.capturedToolStripMenuItem.Click += new System.EventHandler(this.capturedToolStripMenuItem_Click);
            // 
            // byDayToolStripMenuItem
            // 
            this.byDayToolStripMenuItem.Name = "byDayToolStripMenuItem";
            this.byDayToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.byDayToolStripMenuItem.Text = "By Date...";
            this.byDayToolStripMenuItem.Click += new System.EventHandler(this.byDayToolStripMenuItem_Click);
            // 
            // byDateRangeToolStripMenuItem
            // 
            this.byDateRangeToolStripMenuItem.Name = "byDateRangeToolStripMenuItem";
            this.byDateRangeToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.byDateRangeToolStripMenuItem.Text = "By Date Range...";
            this.byDateRangeToolStripMenuItem.Click += new System.EventHandler(this.byDateRangeToolStripMenuItem_Click);
            // 
            // fromDateToolStripMenuItem
            // 
            this.fromDateToolStripMenuItem.Name = "fromDateToolStripMenuItem";
            this.fromDateToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.fromDateToolStripMenuItem.Text = "From Date...";
            this.fromDateToolStripMenuItem.Click += new System.EventHandler(this.fromDateToolStripMenuItem_Click);
            // 
            // byTextToolStripMenuItem
            // 
            this.byTextToolStripMenuItem.Name = "byTextToolStripMenuItem";
            this.byTextToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.byTextToolStripMenuItem.Text = "By Text...";
            this.byTextToolStripMenuItem.Click += new System.EventHandler(this.byTextToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csvToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // csvToolStripMenuItem
            // 
            this.csvToolStripMenuItem.Name = "csvToolStripMenuItem";
            this.csvToolStripMenuItem.Size = new System.Drawing.Size(115, 26);
            this.csvToolStripMenuItem.Text = ".csv";
            this.csvToolStripMenuItem.Click += new System.EventHandler(this.csvToolStripMenuItem_Click);
            // 
            // notesToolStripMenuItem
            // 
            this.notesToolStripMenuItem.Name = "notesToolStripMenuItem";
            this.notesToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.notesToolStripMenuItem.Text = "Notes";
            this.notesToolStripMenuItem.Click += new System.EventHandler(this.notesToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ticketToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.donateToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // ticketToolStripMenuItem
            // 
            this.ticketToolStripMenuItem.Name = "ticketToolStripMenuItem";
            this.ticketToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.ticketToolStripMenuItem.Text = "Ticket ...";
            this.ticketToolStripMenuItem.Click += new System.EventHandler(this.ticketToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.reportsToolStripMenuItem.Text = "Reports...";
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.donateToolStripMenuItem.Text = "Donate...";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // tsAdmin
            // 
            this.tsAdmin.Name = "tsAdmin";
            this.tsAdmin.Size = new System.Drawing.Size(67, 24);
            this.tsAdmin.Text = "Admin";
            this.tsAdmin.Click += new System.EventHandler(this.adminToolStripMenuItem_Click);
            // 
            // txtTicketNo
            // 
            this.txtTicketNo.FormattingEnabled = true;
            this.txtTicketNo.Location = new System.Drawing.Point(213, 125);
            this.txtTicketNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTicketNo.Name = "txtTicketNo";
            this.txtTicketNo.Size = new System.Drawing.Size(143, 28);
            this.txtTicketNo.TabIndex = 35;
            // 
            // btnAddTicket
            // 
            this.btnAddTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddTicket.Location = new System.Drawing.Point(11, 451);
            this.btnAddTicket.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddTicket.Name = "btnAddTicket";
            this.btnAddTicket.Size = new System.Drawing.Size(99, 29);
            this.btnAddTicket.TabIndex = 36;
            this.btnAddTicket.Text = "Add Ticket";
            this.btnAddTicket.UseVisualStyleBackColor = true;
            this.btnAddTicket.Click += new System.EventHandler(this.btnAddTicket_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(118, 485);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(99, 29);
            this.btnClear.TabIndex = 37;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // responseMessage
            // 
            this.responseMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.responseMessage.Location = new System.Drawing.Point(361, 560);
            this.responseMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.responseMessage.Name = "responseMessage";
            this.responseMessage.Size = new System.Drawing.Size(393, 27);
            this.responseMessage.TabIndex = 38;
            // 
            // UpdCurrent
            // 
            this.UpdCurrent.Location = new System.Drawing.Point(267, 331);
            this.UpdCurrent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdCurrent.Name = "UpdCurrent";
            this.UpdCurrent.Size = new System.Drawing.Size(85, 29);
            this.UpdCurrent.TabIndex = 39;
            this.UpdCurrent.Text = "Update";
            this.UpdCurrent.UseVisualStyleBackColor = true;
            this.UpdCurrent.Click += new System.EventHandler(this.UpdCurrent_Click);
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(11, 260);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtDesc.Size = new System.Drawing.Size(194, 99);
            this.txtDesc.TabIndex = 40;
            this.txtDesc.Text = "";
            // 
            // btnCaptureTime
            // 
            this.btnCaptureTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCaptureTime.Location = new System.Drawing.Point(119, 523);
            this.btnCaptureTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCaptureTime.Name = "btnCaptureTime";
            this.btnCaptureTime.Size = new System.Drawing.Size(129, 31);
            this.btnCaptureTime.TabIndex = 41;
            this.btnCaptureTime.Text = "Capture Time";
            this.btnCaptureTime.UseVisualStyleBackColor = true;
            this.btnCaptureTime.Click += new System.EventHandler(this.btnCaptureTime_Click);
            // 
            // btnDelTicket
            // 
            this.btnDelTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelTicket.Location = new System.Drawing.Point(118, 451);
            this.btnDelTicket.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelTicket.Name = "btnDelTicket";
            this.btnDelTicket.Size = new System.Drawing.Size(99, 29);
            this.btnDelTicket.TabIndex = 42;
            this.btnDelTicket.Text = "Delete Ticket";
            this.btnDelTicket.UseVisualStyleBackColor = true;
            this.btnDelTicket.Click += new System.EventHandler(this.btnDelTicket_Click);
            // 
            // lblDarkMode
            // 
            this.lblDarkMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDarkMode.AutoSize = true;
            this.lblDarkMode.Location = new System.Drawing.Point(65, 421);
            this.lblDarkMode.Name = "lblDarkMode";
            this.lblDarkMode.Size = new System.Drawing.Size(83, 20);
            this.lblDarkMode.TabIndex = 45;
            this.lblDarkMode.Text = "Dark Mode";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toggleSwitch1.AutoSize = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(9, 420);
            this.toggleSwitch1.MaximumSize = new System.Drawing.Size(50, 21);
            this.toggleSwitch1.MinimumSize = new System.Drawing.Size(35, 21);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Size = new System.Drawing.Size(50, 21);
            this.toggleSwitch1.TabIndex = 46;
            this.toggleSwitch1.Text = "toggleSwitch1";
            this.toggleSwitch1.UseVisualStyleBackColor = true;
            this.toggleSwitch1.CheckedChanged += new System.EventHandler(this.toggleSwitch1_CheckedChanged_1);
            // 
            // txtTotalTime
            // 
            this.txtTotalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalTime.Location = new System.Drawing.Point(806, 560);
            this.txtTotalTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalTime.Name = "txtTotalTime";
            this.txtTotalTime.Size = new System.Drawing.Size(76, 27);
            this.txtTotalTime.TabIndex = 47;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.Location = new System.Drawing.Point(759, 564);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(41, 18);
            this.lblTotal.TabIndex = 48;
            this.lblTotal.Text = "Total";
            // 
            // TimeCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(896, 599);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtTotalTime);
            this.Controls.Add(this.toggleSwitch1);
            this.Controls.Add(this.lblDarkMode);
            this.Controls.Add(this.btnDelTicket);
            this.Controls.Add(this.btnCaptureTime);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.UpdCurrent);
            this.Controls.Add(this.responseMessage);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddTicket);
            this.Controls.Add(this.txtTicketNo);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.drpType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTimeStarted);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.txtCurrent);
            this.Controls.Add(this.lblStop);
            this.Controls.Add(this.lblPlay);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.exportProgress);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.rbCharge);
            this.Controls.Add(this.rbSupport);
            this.Controls.Add(this.rbNonCharge);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.containerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(673, 632);
            this.Name = "TimeCapture";
            this.Text = "Time Capture Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.containerControl1.ResumeLayout(false);
            this.containerControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Heading;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ProgressBar exportProgress;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblPlay;
        private System.Windows.Forms.Label lblStop;
        private System.Windows.Forms.TextBox txtCurrent;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label lblTimeStarted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox drpType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.RadioButton rbNonCharge;
        private System.Windows.Forms.RadioButton rbCharge;
        private System.Windows.Forms.RadioButton rbSupport;
        private System.Windows.Forms.ContainerControl containerControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadAllResultsToolStripMenuItem;
        private System.Windows.Forms.ComboBox txtTicketNo;
        private System.Windows.Forms.Button btnAddTicket;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem csvToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private Button btnClear;
        private ToolStripMenuItem notesToolStripMenuItem;
        private TextBox responseMessage;
        private Button UpdCurrent;
        private RichTextBox txtDesc;
        private Button btnCaptureTime;
        private ToolStripMenuItem allToolStripMenuItem;
        private ToolStripMenuItem uncapturedToolStripMenuItem;
        private ToolStripMenuItem capturedToolStripMenuItem;
        private Button btnDelTicket;
        private Label lblDarkMode;
        private CustomControls.ToggleSwitch toggleSwitch1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem ticketToolStripMenuItem;
        private PictureBox pictureBox1;
        private ToolStripMenuItem byDayToolStripMenuItem;
        private ToolStripMenuItem byDateRangeToolStripMenuItem;
        private ToolStripMenuItem fromDateToolStripMenuItem;
        private ToolStripMenuItem byTextToolStripMenuItem;
        private TextBox txtTotalTime;
        private Label lblTotal;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem donateToolStripMenuItem;
        private ToolStripMenuItem tsAdmin;
        private DataGridViewTextBoxColumn iTimeID;
        private DataGridViewTextBoxColumn tName;
        private DataGridViewTextBoxColumn TicketNumber;
        private DataGridViewTextBoxColumn StartTime;
        private DataGridViewTextBoxColumn EndTime;
        private DataGridViewTextBoxColumn Total;
        private DataGridViewComboBoxColumn TimeType;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewComboBoxColumn TicketType;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewButtonColumn Button;
        private DataGridViewButtonColumn Continue;
    }
}

