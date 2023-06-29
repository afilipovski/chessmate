using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate
{
    public class GreenPosition : Position
    {
        public GreenPosition(string stringpos) : base(stringpos)
        {
        }

        public GreenPosition(Position p) : base(p)
        {
        }

        public GreenPosition(int x, int y) : base(x, y)
        {
        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 255, 0)), X * Board.TILE_SIDE + Board.OFFSET_X, Y * Board.TILE_SIDE + Board.OFFSET_Y, Board.TILE_SIDE, Board.TILE_SIDE);
        }
    }
}
