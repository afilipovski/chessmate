using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Pieces
{
    public abstract class Piece
    {
        protected Piece(Position position, bool white)
        {
            Position = position;
            White = white;
        }

        public Position Position { get; set; }
        public bool White { get; set; }

        protected Piece() { }
        public Piece(Piece p) { }

        public abstract List<Board> PossibleMoves(Board b);

        public abstract Image GetImage(Graphics g);

        public void Draw(Graphics g)
        {
            Image newImage = GetImage(g);
            g.DrawImage(newImage, Position.X * Board.HEIGHT, Position.Y * Board.HEIGHT, Board.HEIGHT, Board.HEIGHT);
        }

    }
}
