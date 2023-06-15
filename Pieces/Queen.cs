using System.Collections.Generic;
using System.Linq;

namespace ChessMate.Pieces
{
    public class Queen : ContinuousPathPiece
    {
        public Queen(Position position, bool white) : base(position, white)
        {
        }

        public override List<Board> PossibleMoves(Board b)
        {
            List<Board> boards = new List<Board>();

            Bishop bishop = new Bishop(this);
            Rook rook = new Rook(this);

            b.PieceByPosition[Position] = bishop;
            boards.AddRange(bishop.PossibleMoves(b)
                .Select(board =>
                {
                    Position p = board.PieceByPosition.Keys
                        .Where(k => board.PieceByPosition[k] == bishop)
                        .FirstOrDefault();
                    board.PieceByPosition[p] = this;
                    return board;
                })
            );

            b.PieceByPosition[Position] = rook;
            boards.AddRange(rook.PossibleMoves(b)
                .Select(board =>
                {
                    Position p = board.PieceByPosition.Keys
                        .Where(k => board.PieceByPosition[k] == rook)
                        .FirstOrDefault();
                    board.PieceByPosition[p] = this;
                    return board;
                })
            );

            return boards;
        }
    }
}
