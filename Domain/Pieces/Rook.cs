﻿using System;
using System.Collections.Generic;
using System.Drawing;
using ChessMate.Domain.Positions;

namespace ChessMate.Domain.Pieces
{
    // Top
    [Serializable]
    public class Rook : ContinuousPathPiece
    {
        public bool MovedSinceStart { get; set; }

        public Rook(Position position, bool white) : base(position, white)
        {
            MovedSinceStart = false;
        }

        public Rook(ContinuousPathPiece cpp) : base(cpp)
        {
        }

        public override List<Board> PossibleMoves(Board b)
        {
            List<Board> boards = new List<Board>();

            // right
            FindValidPositions(new Position(Position.X + 1, Position.Y), b, boards, p => p.X <= 7, p => new Position(p.X + 1, p.Y));
            // left
            FindValidPositions(new Position(Position.X - 1, Position.Y), b, boards, p => p.X >= 0, p => new Position(p.X - 1, p.Y));
            // top
            FindValidPositions(new Position(Position.X, Position.Y + 1), b, boards, p => p.Y <= 7, p => new Position(p.X, p.Y + 1));
            // bottom
            FindValidPositions(new Position(Position.X, Position.Y - 1), b, boards, p => p.Y >= 0, p => new Position(p.X, p.Y - 1));

            boards.ForEach(board =>
            {
                Position p = board.NewPos;
                Rook r = (Rook)board.PieceByPosition[p];
                r.MovedSinceStart = true;
            });

            return boards;
        }

        public override Piece Clone()
        {
            Rook r = new Rook(this.Position, this.White);
            // For castling
            r.MovedSinceStart = this.MovedSinceStart;
            return r;
        }

        public override string Name() => "rook";
    }
}
