using ChessMate.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessMate
{
    public class Board
    {
        public Dictionary<Position, Piece> PieceByPosition { get; set; }
        public bool WhiteTurn { get; set; }

        // copy constructor
        public Board(Board board)
        {
            PieceByPosition = new Dictionary<Position, Piece>();
            WhiteTurn = !board.WhiteTurn;
            foreach (Position key in board.PieceByPosition.Keys)
            {
                PieceByPosition[key] = board.PieceByPosition[key];
            }
        }

        // copy 2
        public Board(Board b, Position pos, Piece p) : this(b)
        {
            PieceByPosition[pos] = p;
        }


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
