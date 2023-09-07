namespace TimeCapture
{
    public partial class ConfirmClose : Form
    {
        public TimeCapture timeCapture { get; set; }
        public ConfirmClose(TimeCapture timeCapture)
        {
            InitializeComponent();
            this.timeCapture = timeCapture;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timeCapture.PushNofication("You have clocked out, enjoy the evening", NotificationType.Info);
            timeCapture.Dispose();
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeCapture timeCapture = new TimeCapture();
            timeCapture.btnExport_Click(sender, e);
            timeCapture.PushNofication("You have clocked out, enjoy the evening", NotificationType.Info);
            timeCapture.Dispose();
            Dispose();
        }
    }
}
