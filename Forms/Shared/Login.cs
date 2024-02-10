using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uno.Extensions;
using Keys = System.Windows.Forms.Keys;

namespace TimeCapture.Forms.Shared
{
    public partial class Login : Form
    {
        public TimeCapture time { get; set; }

        public Login()
        {
            InitializeComponent();
            time = new TimeCapture();
            time.CheckDB();
            time.Dispose();
            string response;
            new Access().TestConnection(out response);
            new Access().InitiateKeepAlive();
            this.textBox2.PasswordChar = '*';

            if (_configuration.GetConfigValue("isLoginBypass").isTrue())
            {
                int iUserID;
                if (_configuration.isByPassValid(out iUserID))
                {
                    time = new TimeCapture();
                    time.UserID = iUserID;
                    time.Show();
                    time.LoginResponse(iUserID);
                    time.Disposed += Time_Disposed;
                    this.Shown += Login_Shown; ;
                }
            }
            else
            {

                int AllowNewUsers = Convert.ToInt32(_configuration.GetConfigValue("AllowNewUsers"));
                if (AllowNewUsers == 0)
                    linkLabel1.Visible = false;
                this.textBox2.KeyPress += TextBox2_KeyPress;
            }
        }

        private void Login_Shown(object? sender, EventArgs e)
        {
            this.Hide();
        }

        private void TextBox2_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                button1.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int UserID;
            if (!textBox1.Text.ToString().IsNullOrEmpty() && !textBox2.Text.ToString().IsNullOrEmpty())
            {
                if (new Access().Login(textBox1.Text.ToString(), textBox2.Text.ToString(), out UserID))
                {
                    time = new TimeCapture();
                    time.UserID = UserID;
                    time.Show();
                    this.Hide();
                    time.LoginResponse(UserID);
                    time.Disposed += Time_Disposed;
                    _configuration.UpdateLocalUser(textBox1.Text.ToString(), textBox2.Text.ToString(), UserID);
                }
                else
                {
                    MessageBox.Show("Incorrect Credentials");
                }
            }
            else
            {
                MessageBox.Show("Please provide a username and password");
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
                            time.Show();
                            this.Hide();
                            time.LoginResponse(UserID);
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

        private void Time_Disposed(object? sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
