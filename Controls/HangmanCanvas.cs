using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HangmanGame.Controls
{
    public class HangmanCanvas : Control
    {
        private int _wrongGuesses;
        private const int MaxStages = 7;

        public int WrongGuesses
        {
            get { return _wrongGuesses; }
            set
            {
                _wrongGuesses = value;
                Invalidate(); // Trigger redraw
            }
        }

        public HangmanCanvas()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.White;
            _wrongGuesses = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Calculate scaling based on control size
            int width = Width;
            int height = Height;
            int centerX = width / 2;

            // Define pen for drawing
            Pen gallowsPen = new Pen(Color.FromArgb(52, 73, 94), 4);
            Pen bodyPen = new Pen(Color.FromArgb(231, 76, 60), 3);

            // Draw gallows (always visible)
            DrawGallows(g, gallowsPen, width, height);

            // Draw hangman parts based on wrong guesses
            if (_wrongGuesses >= 1) DrawHead(g, bodyPen, centerX, height);
            if (_wrongGuesses >= 2) DrawBody(g, bodyPen, centerX, height);
            if (_wrongGuesses >= 3) DrawLeftArm(g, bodyPen, centerX, height);
            if (_wrongGuesses >= 4) DrawRightArm(g, bodyPen, centerX, height);
            if (_wrongGuesses >= 5) DrawLeftLeg(g, bodyPen, centerX, height);
            if (_wrongGuesses >= 6) DrawRightLeg(g, bodyPen, centerX, height);
            if (_wrongGuesses >= 7) DrawFace(g, bodyPen, centerX, height);

            gallowsPen.Dispose();
            bodyPen.Dispose();
        }

        private void DrawGallows(Graphics g, Pen pen, int width, int height)
        {
            int baseY = (int)(height * 0.9);
            int topY = (int)(height * 0.1);
            int poleX = (int)(width * 0.3);
            int ropeX = (int)(width * 0.5);

            // Base
            g.DrawLine(pen, width * 0.1f, baseY, width * 0.5f, baseY);
            // Vertical pole
            g.DrawLine(pen, poleX, baseY, poleX, topY);
            // Top beam
            g.DrawLine(pen, poleX, topY, ropeX, topY);
            // Rope
            g.DrawLine(pen, ropeX, topY, ropeX, (int)(height * 0.2));
        }

        private void DrawHead(Graphics g, Pen pen, int centerX, int height)
        {
            int headRadius = (int)(height * 0.08);
            int headY = (int)(height * 0.25);
            g.DrawEllipse(pen, centerX - headRadius, headY, headRadius * 2, headRadius * 2);
        }

        private void DrawBody(Graphics g, Pen pen, int centerX, int height)
        {
            int bodyTop = (int)(height * 0.33);
            int bodyBottom = (int)(height * 0.58);
            g.DrawLine(pen, centerX, bodyTop, centerX, bodyBottom);
        }

        private void DrawLeftArm(Graphics g, Pen pen, int centerX, int height)
        {
            int armY = (int)(height * 0.45);
            int armLength = (int)(Width * 0.1);
            g.DrawLine(pen, centerX, armY, centerX - armLength, armY + armLength / 2);
        }

        private void DrawRightArm(Graphics g, Pen pen, int centerX, int height)
        {
            int armY = (int)(height * 0.45);
            int armLength = (int)(Width * 0.1);
            g.DrawLine(pen, centerX, armY, centerX + armLength, armY + armLength / 2);
        }

        private void DrawLeftLeg(Graphics g, Pen pen, int centerX, int height)
        {
            int legTop = (int)(height * 0.58);
            int legBottom = (int)(height * 0.78);
            int legOffset = (int)(Width * 0.08);
            g.DrawLine(pen, centerX, legTop, centerX - legOffset, legBottom);
        }

        private void DrawRightLeg(Graphics g, Pen pen, int centerX, int height)
        {
            int legTop = (int)(height * 0.58);
            int legBottom = (int)(height * 0.78);
            int legOffset = (int)(Width * 0.08);
            g.DrawLine(pen, centerX, legTop, centerX + legOffset, legBottom);
        }

        private void DrawFace(Graphics g, Pen pen, int centerX, int height)
        {
            int headRadius = (int)(height * 0.08);
            int headY = (int)(height * 0.25);
            int eyeOffset = headRadius / 3;
            int eyeY = headY + headRadius / 2;

            // Draw X eyes
            int eyeSize = 5;
            // Left eye
            g.DrawLine(pen, centerX - eyeOffset - eyeSize, eyeY - eyeSize, 
                      centerX - eyeOffset + eyeSize, eyeY + eyeSize);
            g.DrawLine(pen, centerX - eyeOffset + eyeSize, eyeY - eyeSize, 
                      centerX - eyeOffset - eyeSize, eyeY + eyeSize);
            // Right eye
            g.DrawLine(pen, centerX + eyeOffset - eyeSize, eyeY - eyeSize, 
                      centerX + eyeOffset + eyeSize, eyeY + eyeSize);
            g.DrawLine(pen, centerX + eyeOffset + eyeSize, eyeY - eyeSize, 
                      centerX + eyeOffset - eyeSize, eyeY + eyeSize);
        }

        public void Reset()
        {
            WrongGuesses = 0;
        }
    }
}
