using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate
{
    public class Board
    {
        public Dictionary<Position,Piece> PieceByPosition { get; set; }
        public bool WhiteTurn { get; set; }



        public List<Board> Successor()
        {
            List<Board> res = new List<Board>();

            PieceByPosition.Values.ToList()
                .Where(piece => piece.White == WhiteTurn).ToList()
                .ForEach(piece =>
                {
                    res.Concat(piece.PossibleMoves(this));
                });

            return res;
        }
        public bool IsOccupied(Position position)
        {
            return PieceByPosition.ContainsKey(position);
        }



    }
}
