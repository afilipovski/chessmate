using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ChessMate.Domain.Positions;

namespace ChessMate.Domain.Pieces
{
    [Serializable]
    public class King : Piece
    {
        public bool MovedSinceStart { get; set; }

        public King(Position position, bool white) : base(position, white)
        {
            MovedSinceStart = false;
        }

        public override List<Board> PossibleMoves(Board b)
        {
            List<Board> boards = GetSurroundingPositions()
                .Where(p => Board.IsInBoard(p) && IsSpaceAvailable(b, p))
                .Select(p => new Board(b, Position, p, this))
                .ToList();
            CheckCastling(b, boards);

            boards.ForEach(board =>
            {
                Position p = board.NewPos;
                King k = (King)board.PieceByPosition[p];
                k.MovedSinceStart = true;
            });

            return boards;
        }

        private void CheckCastling(Board b, List<Board> boards)
        {
            if (!MovedSinceStart &&
                !b.IsOccupied(new Position(5, Position.Y)) &&
                !b.IsOccupied(new Position(6, Position.Y)) &&
                b.IsOccupied(new Position(7, Position.Y)) &&
                b.PieceByPosition[new Position(7, Position.Y)] is Rook rook &&
                !rook.MovedSinceStart &&
                rook.White == White)
            {
                ColoredPosition cp = new ColoredPosition(new Position(6, Position.Y), PositionColor.Blue);
                Board board = new Board(b, Position, cp, this);
                Piece r = rook.Clone();
                r.Position = new Position(5, Position.Y);
                board.PieceByPosition[new Position(5, Position.Y)] = r;
                board.PieceByPosition[new Position(7, Position.Y)] = null;
                board.PieceByPosition[new Position(4, Position.Y)] = null;
                boards.Add(board);
            }
        }

        private bool IsSpaceAvailable(Board board, Position p)
        {
            return !board.IsOccupied(p) || board.IsOccupied(p) && board.PieceByPosition[p].White != White;
        }

        private List<Position> GetSurroundingPositions()
        {
            return new List<Position>()
            {
                new Position(Position.X+1, Position.Y),
                new Position(Position.X+1, Position.Y+1),
                new Position(Position.X, Position.Y+1),
                new Position(Position.X-1, Position.Y+1),
                new Position(Position.X-1, Position.Y),
                new Position(Position.X-1, Position.Y-1),
                new Position(Position.X, Position.Y-1),
                new Position(Position.X+1, Position.Y-1)
            };
        }

        public override Piece Clone()
        {
            King p = new King(this.Position, this.White);
            // For castling
            p.MovedSinceStart = this.MovedSinceStart;
            return p;
        }

        public override string Name() => "king";
    }
}
