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

                int x = Math.Min(p1.X, p2.X);
                int y = Math.Min(p1.Y, p2.Y);
                int w = Math.Max(p1.X, p2.X);
                int h = Math.Max(p1.Y, p2.Y);

                g.DrawLine(drawingPen, x, y, w, h);

                drawingPen.Dispose();
            }
        }
        private class RectangleTool : Tool
        {
            public override void Draw(Graphics g, Brush drawingBrush, Point p1, Point p2)
            {
                throw new NotImplementedException();
            }
        }
        private class ElipseTool : Tool
        {
            public override void Draw(Graphics g, Brush drawingBrush, Point p1, Point p2)
            {
                throw new NotImplementedException();
            }
        }

        private Tool drawingTool;   // Currently-selected drawing tool

        private Brush drawingBrush; // Currently-selected brush color (converted to pen in Linetool)
        private Bitmap drawingBmp;  // in-memory representation of canvas

        private bool userIsDrawing; // whether user is currently dragging the mouse

        private Point p1, p2;       // start and end point for drawin

        public Form1()
        {
            drawingTool = new LineTool();
            drawingBrush = Brushes.Black;
            drawingBmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            userIsDrawing = false;
            p1 = new Point(0, 0);
            p2 = new Point(0, 0);

            InitializeComponent();
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
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("Mouse moved " + e.X + ", " + e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("Mouse released at " + e.X + ", " + e.Y);
        }
    }
}
