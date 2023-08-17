namespace TimeCapture.Forms
{
    public partial class AddTicket : Form
    {
        public TimeCapture frmCapture { get; set; }
        public AddTicket(TimeCapture timeCapture)
        {
            InitializeComponent();
            frmCapture = timeCapture;
            bool isDarkMode;
            frmCapture.generic_DarkMode(this, out isDarkMode);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty");
            }
            else if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Ticket Number cannot be empty");
            }
            else
            {
                try
                {
                    int ID = Convert.ToInt32(textBox1.Text);
                    new Access().AddTicket(ID, txtName.Text);
                    MessageBox.Show("Ticket added");
                    frmCapture.getTickets();
                    this.Dispose();
                }
                catch
                {
                    MessageBox.Show("Please provide a valid number for the ID");
                }
            }
        }
    }
}
