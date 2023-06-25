using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public abstract Bitmap GetBitmap(Graphics g);

        public void Draw(Graphics g)
        {
            Bitmap bitmap = GetBitmap(g);
            g.DrawImage(bitmap, Position.X * Board.HEIGHT + Board.OFFSET, Position.Y * Board.HEIGHT, Board.HEIGHT, Board.HEIGHT);
        }

        public bool Equals(Piece piece)
        {
            if (Position.X != piece.Position.X || Position.Y != piece.Position.Y) return false;
            if (GetType() != piece.GetType()) return false;
            if (White !=  piece.White) return false;
            return true;
        }

    }
}
