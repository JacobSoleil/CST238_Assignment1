using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Assignment1
{
    public partial class Form1 : Form
    {
        // Abstract tool class (to be used by drawing tools)
        private abstract class Tool
        {
            public abstract void Draw(Graphics g, Brush drawingBrush, Point p1, Point p2);
        }

        //Drawing tools, each with own behavior for drawing
        private class LineTool : Tool
        {
            public override void Draw(Graphics g, Brush drawingBrush, Point p1, Point p2)
            {
                Pen drawingPen = new Pen(drawingBrush);

                g.DrawLine(drawingPen, p1.X, p1.Y, p2.X, p2.Y);

                drawingPen.Dispose();
            }
        }
        private class RectangleTool : Tool
        {
            public override void Draw(Graphics g, Brush drawingBrush, Point p1, Point p2)
            {
                int x = Math.Min(p1.X, p2.X);
                int y = Math.Min(p1.Y, p2.Y);
                int w = Math.Abs(p2.X - p1.X);
                int h = Math.Abs(p2.Y - p1.Y);

                g.FillRectangle(drawingBrush, x, y, w, h);
            }
        }
        private class EllipseTool : Tool
        {
            public override void Draw(Graphics g, Brush drawingBrush, Point p1, Point p2)
            {
                int x = Math.Min(p1.X, p2.X);
                int y = Math.Min(p1.Y, p2.Y);
                int w = Math.Abs(p2.X - p1.X);
                int h = Math.Abs(p2.Y - p1.Y);

                g.FillEllipse(drawingBrush, x, y, w, h);
            }
        }

        private Tool drawingTool;   // Currently-selected drawing tool

        private Brush drawingBrush; // Currently-selected brush color (converted to pen in Linetool)
        
        private Bitmap drawingBmp;  // in-memory representation of canvas

        private bool userIsDrawing; // whether user is currently dragging the mouse

        private Point p1, p2;       // start and end point for drawin

        public Form1()
        {
            InitializeComponent();

            drawingTool = new RectangleTool();   // Default drawing behavior is a black line
            drawingBrush = Brushes.Black;

            drawingBmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics g = Graphics.FromImage(drawingBmp);
            g.Clear(Color.White);

            userIsDrawing = false;
            p1 = new Point(0, 0);
            p2 = new Point(0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("Mouse clicked at "+e.X+", "+e.Y);

            // Beginning to draw, set initial point & reset point 2
            userIsDrawing=true;
            p1 = new Point(e.X, e.Y);
            p2 = p1;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("Mouse moved " + e.X + ", " + e.Y);

            if (userIsDrawing)
            {
                // Update second point & canvas for real-time imaging
                p2 = new Point(e.X, e.Y);
                Refresh();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("Mouse released at " + e.X + ", " + e.Y);

            if (userIsDrawing)
            {
                userIsDrawing = false;
                p2 = new Point(e.X, e.Y);

                // Draw onto the bitmap permanently
                Graphics g = Graphics.FromImage(drawingBmp);
                drawingTool.Draw(g, drawingBrush, p1, p2);
            }
        }

        private void rectangleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            drawingTool = new RectangleTool();
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawingTool = new EllipseTool();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawingTool = new LineTool();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Draw the bitmap onto the screen
            e.Graphics.DrawImage(drawingBmp, 0, 0);

            // If currently drawing, preview what it would look like
            if (userIsDrawing)
            {
                drawingTool.Draw(e.Graphics, drawingBrush, p1, p2);
            }
        }
    }
}
