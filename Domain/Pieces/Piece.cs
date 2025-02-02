using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain.Positions;

namespace ChessMate.Domain.Pieces
{
    [Serializable]
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
        public Piece(Piece p) 
        {
            Position = p.Position;
            White = p.White;
        }

        public abstract List<Board> PossibleMoves(Board b);

        public string NameColor() => (White ? "white" : "black") + "-" + Name();

        public abstract string Name();

        public bool Equals(Piece piece)
        {
            if (Position.X != piece.Position.X || Position.Y != piece.Position.Y) return false;
            if (GetType() != piece.GetType()) return false;
            if (White !=  piece.White) return false;
            return true;
        }

        public override string ToString()
        {
            return (this.White ? "White" : "Black") + " " + this.GetType().Name + " " + this.Position.ToString();
        }

        abstract public Piece Clone();
    }
}
