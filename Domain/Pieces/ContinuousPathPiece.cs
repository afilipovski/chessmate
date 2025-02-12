using System;
using System.Collections.Generic;
using ChessMate.Domain.Positions;

namespace ChessMate.Domain.Pieces
{
    [Serializable]
    public abstract class ContinuousPathPiece : Piece
    {
        protected ContinuousPathPiece(Position position, bool white) : base(position, white)
        {
        }

        protected ContinuousPathPiece(ContinuousPathPiece cpp) : base(cpp)
        {
        }

        /// <summary>
        /// Update a piece position.
        /// </summary>
        /// <param name="p">A piece.</param>
        /// <returns>A piece.</returns>
        protected delegate Position Operation(Position p);

        /// <summary>
        /// Get all valid positions for a continuous path.
        /// </summary>
        /// <param name="p">A position.</param>
        /// <param name="b">A board state.</param>
        /// <param name="boards">A list of resulting board states.</param>
        /// <param name="condition">A predicate that determines if a position is valid.</param>
        /// <param name="operation">A callback that updates a position.</param>
        protected void FindValidPositions(Position p, Board b, List<Board> boards, Predicate<Position> condition, Operation operation)
        {
            Position position = p;
            while (condition(position))
            {
                if (b.IsOccupied(position) && b.PieceByPosition[position].White == White)
                    break;
                boards.Add(new Board(b, Position, position, this));
                if (b.IsOccupied(position))
                    break;
                position = operation(position);
            }
        }
    }
}
