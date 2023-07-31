using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using TimeCapture.Selenium.TimeTaker;

namespace TimeCapture.Forms
{
    public partial class TicketViewer : Form
    {
        public TicketViewer(string sValue)
        {
            InitializeComponent();
            string sURL;
            new TicketSystem().GetTicketURL(BrowserType.Chrome, sValue, out sURL);
            if (sURL != null)
            {
                this.webViewer1.Url = new System.Uri(sURL);
            }
        }
    }
}
