using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate
{
    internal class Pawn : Piece
    {
        public Pawn(Position position, bool white) : base(position, white)
        {
        }

        public override List<Board> PossibleMoves(Board b)
        {
            throw new NotImplementedException();
        }
        
        public override void Draw(Graphics g)
        {
            
            Image newImage = Image.FromFile((White) ? @"D:\Visual Studio projects\ChessMate\PieceImages\w_pawn_png_shadow_1024px.png" 
                : @"D:\Visual Studio projects\ChessMate\PieceImages\b_pawn_png_shadow_1024px.png");
            g.DrawImage(newImage, Position.X * Board.HEIGHT, Position.Y * Board.HEIGHT, Board.HEIGHT, Board.HEIGHT);
        }
    }
}
