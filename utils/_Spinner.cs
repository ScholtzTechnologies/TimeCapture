using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;

namespace TimeCapture.Forms.Shared
{
    public partial class _Spinner : Form
    {
        public _Spinner()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            int radius = 20;

            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            this.Region = new Region(path);
        }
    }
}
