using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace CustomControls
{
    public class ToggleSwitch : CheckBox
    {
        private Color onBGColour = Color.MediumSlateBlue;
        private Color onFGColour = Color.WhiteSmoke;
        private Color offBGColour = Color.Gray;
        private Color offFGColour = Color.Gainsboro;

        public ToggleSwitch()
        {
            MinimumSize = new Size(35, 22);
            MaximumSize = new Size(50, 22);
        }

        private GraphicsPath GetFigurePath()
        {
            int arcSize = Height - 1;
            Rectangle leftArc = new(0, 0, arcSize, arcSize);
            Rectangle rightArc = new(Width - arcSize - 2, 0, arcSize, arcSize);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(leftArc, 90, 180);
            path.AddArc(rightArc, 270, 180);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int toggleSize = Height - 5;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(Parent.BackColor);

            if (Checked)
            {
                pevent.Graphics.FillPath(new SolidBrush(onBGColour), GetFigurePath());
                pevent.Graphics.FillEllipse(new SolidBrush(onFGColour),
                    new Rectangle(Width - Height + 1, 2, toggleSize, toggleSize));
            }
            else
            {
                pevent.Graphics.FillPath(new SolidBrush(offBGColour), GetFigurePath());
                pevent.Graphics.FillEllipse(new SolidBrush(offFGColour),
                    new Rectangle(2, 2, toggleSize, toggleSize));
            }
        }
    }

    public class WebViewer : WebBrowser
    {
        public WebViewer()
        {
            MinimumSize = new Size(20, 20);
        }
    }

    public class RoundedProgressBar : ProgressBar
    {
        public RoundedProgressBar()
        {
            DoubleBuffered = true;
            ProgressBarColor = Color.FromArgb(224, 224, 224);
            ProgressBackColor = Color.FromArgb(255, 128, 255);
            ProgressFont = new Font(Font.FontFamily, (int)(this.Height * 0.7), FontStyle.Bold);
            ProgressFontColor = Color.Black;
            Value = 0;
        }

        public int Value { get; set; }

        [Category("Appearance")]
        public Color ProgressBarColor { get; set; }

        [Category("Appearance")]
        public Color ProgressBackColor { get; set; }

        [Category("Appearance")]
        public Font ProgressFont { get; set; }

        [Category("Appearance")]
        public Color ProgressFontColor { get; set; }


        private GraphicsPath GetRoundRectagle(Rectangle bounds)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = bounds.Height;
            if (bounds.Height <= 0) radius = 20;
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius,
                        radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
        private void RecreateRegion()
        {
            var bounds = new Rectangle(this.ClientRectangle.Location, this.ClientRectangle.Size);
            bounds.Inflate(-1, -1);
            this.Region = new Region(GetRoundRectagle(bounds));
            this.Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.RecreateRegion();
        }
    }
}
