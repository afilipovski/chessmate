using ChessMate.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessMate
{
    public partial class Form1 : Form
    {
        public Board Board { get; set; }
        public List<GreenPosition> greenPositions { get; set; } = new List<GreenPosition>();
        public Form1()
        {
            InitializeComponent();
            Board = new Board();
            this.DoubleBuffered = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Board.HEIGHT = ClientSize.Height / 8;
            Board.WIDTH = Width / 8;
            Board.OFFSET = (Width - Height) / 2;
            Board.DrawTiles(e.Graphics);
            foreach(GreenPosition pos in greenPositions)
            {
                pos.Draw(e.Graphics);
            }
        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Height -= Height % 8;
            Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            greenPositions = new List<GreenPosition>();
            Board = Board.Click(new Position(e.X, e.Y), greenPositions);
            Invalidate();
        }
    }
}
