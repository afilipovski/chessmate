using ChessMate.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace ChessMate.Domain.Pieces
{
    [Serializable]
    internal class Pawn : Piece
    {
        public int TwoSquareAdvanceTimestamp { get; set; } = -1;

		public Pawn(Position position, bool white) : base(position, white)
		{
		}

		public Pawn(Position position, bool white, int twoSquareAdvanceTimestamp) : base(position, white)
        {
            TwoSquareAdvanceTimestamp = twoSquareAdvanceTimestamp;
        }

        public override List<Board> PossibleMoves(Board b)
        {
            List<Board> boards = new List<Board>();

            Position forwardOne = White ? new Position(Position.X, Position.Y-1) : new Position(Position.X, Position.Y + 1);
            Position forwardTwo = White ? new Position(Position.X, Position.Y - 2) : new Position(Position.X, Position.Y + 2);
            Position captureLeft = White ? new Position(Position.X - 1, Position.Y - 1) : new Position(Position.X - 1, Position.Y + 1);
            Position captureRight = White ? new Position(Position.X + 1, Position.Y - 1) : new Position(Position.X + 1, Position.Y + 1);
            Position left = new Position(Position.X-1, Position.Y);
            Position right = new Position(Position.X+1, Position.Y);

            int startY = White ? 6 : 1;
            int endY = White ? 0 : 7;

            void ProcessedAdd(Board rawBoard)
            {
                if (rawBoard.NewPos.Y == endY)
                {
                    rawBoard.NewPos = new ColoredPosition(rawBoard.NewPos, PositionColor.Blue);
                    rawBoard.PieceByPosition[rawBoard.NewPos] = new Queen(rawBoard.NewPos, White);
                }
                boards.Add(rawBoard);
            }

			//Forward
            if (Board.IsInBoard(forwardOne) && !b.IsOccupied(forwardOne)) {
                ProcessedAdd(new Board(b, Position, forwardOne, new Pawn(forwardOne, White)));
                if (Position.Y == startY && !b.IsOccupied(forwardTwo))
                    ProcessedAdd(new Board(b, Position, forwardTwo, new Pawn(forwardTwo, White, b.TurnNumber)));
            }

            //Capture
            void CaptureForward(Position capture)
            {
				if (Board.IsInBoard(capture) && b.IsOccupied(capture) && b.PieceByPosition[capture].White != White)
					ProcessedAdd(new Board(b, Position, capture, new Pawn(capture, White)));
			}
            CaptureForward(captureLeft); CaptureForward(captureRight);

            //En passant
            void Enpassant(Position adjacent, Position capture)
            {
                if (Board.IsInBoard(adjacent) && b.IsOccupied(adjacent) && b.PieceByPosition[adjacent].White != White &&
                    b.PieceByPosition[adjacent] is Pawn p && p.TwoSquareAdvanceTimestamp == b.TurnNumber-1)
                {
                    ColoredPosition cp = new ColoredPosition(capture, PositionColor.Blue);
                    Board epb = new Board(b, Position, cp, new Pawn(capture, White));
                    epb.PieceByPosition[adjacent] = null;
                    ProcessedAdd(epb);
                }
            }
            Enpassant(left, captureLeft); Enpassant(right, captureRight);

            return boards;
        }

        public override Piece Clone()
        {
            Pawn p = new Pawn(this.Position, this.White, this.TwoSquareAdvanceTimestamp);
            return p;
        }

        public override string Name() => "pawn";
    }
}
