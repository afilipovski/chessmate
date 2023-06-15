using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessMate
{
    public partial class Form1 : Form
    {
        public Board Board { get; set; }
        public Form1()
        {
            InitializeComponent();
            Board = new Board();
            this.DoubleBuffered = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Board.DrawTiles(e.Graphics, ClientSize.Height / 8, Width / 8);
        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            Height = Width = Math.Min(Height,Width);
            Invalidate();
        }
    }
}
