using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Pieces
{
    internal class Knight : Piece
    {
        public Knight(Position position, bool white) : base(position, white)
        {
        }

        public override Image GetImage(Graphics g)
        {
            return Image.FromFile((White) ? @"D:\Visual Studio projects\ChessMate\PieceImages\w_knight_png_shadow_1024px.png"
                : @"D:\Visual Studio projects\ChessMate\PieceImages\b_knight_png_shadow_1024px.png");
        }

        public override List<Board> PossibleMoves(Board b)
        {
            throw new NotImplementedException();
        }
    }
}
