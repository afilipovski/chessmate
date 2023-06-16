using System;
using System.Collections.Generic;

namespace ChessMate.Pieces
{
    public abstract class ContinuousPathPiece : Piece
    {
        protected ContinuousPathPiece(Position position, bool white) : base(position, white)
        {
        }

        protected ContinuousPathPiece(ContinuousPathPiece cpp) : base(cpp)
        {
        }

        protected delegate void Operation(Position p);

        protected void findValidPositions(Position p, Board b, List<Board> boards, Predicate<Position> condition, Operation operation)
        {
            for (; condition(p); operation(p))
            {
                if (b.IsOccupied(p) && b.PieceByPosition[p].White == b.WhiteTurn)
                    break;
                boards.Add(new Board(b, Position, p, this));
                if (!b.IsOccupied(p))
                    break;
            }
        }
    }
}
