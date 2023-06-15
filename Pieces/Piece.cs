using System;
using System.Collections.Generic;
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


        public abstract List<Board> PossibleMoves(Board b);



    }
}
