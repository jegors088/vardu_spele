using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HangmanGame.Controls
{
    public class LetterButton : Button
    {
        private bool _isHovered;
        private Color _normalColor = Color.FromArgb(88, 86, 214);
        private Color _hoverColor = Color.FromArgb(108, 106, 234);
        private Color _disabledColor = Color.FromArgb(200, 203, 210);

        public char Letter { get; set; }

        public LetterButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.OptimizedDoubleBuffer, true);
            
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Bahnschrift", 14F, FontStyle.Bold);
            ForeColor = Color.White;
            Size = new Size(50, 50);
            Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Determine button color
            Color buttonColor = _normalColor;
            if (!Enabled)
                buttonColor = _disabledColor;
            else if (_isHovered)
                buttonColor = _hoverColor;

            // Draw shadow for depth
            if (Enabled && !_isHovered)
            {
                using (GraphicsPath shadowPath = GetRoundedRectangle(
                    new Rectangle(2, 2, ClientRectangle.Width - 2, ClientRectangle.Height - 2), 10))
                {
                    using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(40, 0, 0, 0)))
                    {
                        g.FillPath(shadowBrush, shadowPath);
                    }
                }
            }

            // Draw rounded rectangle
            using (GraphicsPath path = GetRoundedRectangle(ClientRectangle, 10))
            {
                using (SolidBrush brush = new SolidBrush(buttonColor))
                {
                    g.FillPath(brush, path);
                }
            }

            // Draw text
            TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            
            // Create more rounded corners for modern look
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            
            return path;
        }
    }
}
