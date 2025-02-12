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

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="p">A piece.</param>
        public Piece(Piece p) 
        {
            Position = p.Position;
            White = p.White;
        }

        /// <summary>
        /// Generates all possible successor states of the piece inside a board.
        /// </summary>
        /// <param name="b">A board.</param>
        /// <returns>A list of all successor states of the piece.</returns>
        public abstract List<Board> PossibleMoves(Board b);

        /// <summary>
        /// Name and color for shape rendering.
        /// </summary>
        /// <returns>A name and color.</returns>
        public string NameColor() => (White ? "white" : "black") + "-" + Name();

        /// <summary>
        /// Name for shape rendering.
        /// </summary>
        /// <returns>A name.</returns>
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

        /// <summary>
        /// Creates a deep copy.
        /// </summary>
        /// <returns>A piece.</returns>
        abstract public Piece Clone();
    }
}
