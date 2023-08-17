using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeCapture.Forms.Shared
{
    public partial class Login : Form
    {
        public TimeCapture time { get; set; }
        public Login(TimeCapture timeCapture)
        {
            InitializeComponent();
            time = timeCapture;
            timeCapture.Hide();
            this.textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int UserID;
            if (new Access().Login(textBox1.Text.ToString(), textBox2.Text.ToString(), out UserID))
            {
                time.UserID = UserID;
                time.CompleteLogin();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Incorrect Credentials");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bool isOK;
            string Email;
            int UserID;
            if (!string.IsNullOrEmpty(textBox1.Text.ToString()) && !string.IsNullOrEmpty(textBox2.Text.ToString()))
            {
                new TimeCapture().ShowInputDialog("Please provide your email", out isOK, out Email);
                if (isOK && !string.IsNullOrEmpty(Email))
                    if (new Access().Register(textBox1.Text.ToString(), textBox2.Text.ToString(), Email))
                        if (new Access().Login(textBox1.Text.ToString(), textBox2.Text.ToString(), out UserID))
                        {
                            time.UserID = UserID;
                            time.CompleteLogin();
                            this.Dispose();
                        }
                        else
                            MessageBox.Show("Error when signing in. Please try again");
                    else
                        MessageBox.Show("Failed to register, please try again or contact support");
                else if (isOK && string.IsNullOrEmpty(Email))
                    MessageBox.Show("Please provide a valid email");
            }
            else
            {
                MessageBox.Show("Please provide a username and password on the login form first");
            }

        }
    }
}
