using System.Collections.Generic;
using System.Drawing;

namespace ChessMate.Pieces
{
    public class King : Piece
    {
        public bool MovedSinceStart { get; set; }

        public King(Position position, bool white) : base(position, white)
        {
            MovedSinceStart = false;
        }

        public override Image GetImage(Graphics g)
        {
            return Image.FromFile((White) ? @"D:\Visual Studio projects\ChessMate\PieceImages\w_king_png_shadow_1024px.png"
                : @"D:\Visual Studio projects\ChessMate\PieceImages\b_king_png_shadow_1024px.png");
        }

        public override List<Board> PossibleMoves(Board b)
        {
            List<Board> boards = new List<Board>();

            // Castling
            if (!MovedSinceStart &&
                !b.IsOccupied(new Position(Position.X, 5)) &&
                !b.IsOccupied(new Position(Position.X, 6)) &&
                b.IsOccupied(new Position(Position.X, 7)) &&
                b.PieceByPosition[new Position(Position.X, 7)] is Rook rook
                && !rook.MovedSinceStart)
            {
                Board newBoard = new Board(b);
                newBoard.PieceByPosition[new Position(Position.X, 5)] = rook;
                newBoard.PieceByPosition[new Position(Position.X, 6)] = this;
                newBoard.PieceByPosition.Remove(new Position(Position.X, 7));
                newBoard.PieceByPosition.Remove(new Position(Position.X, 4));
                boards.Add(newBoard);
            }

            // Surrounding spaces
            Position p;
            if (Position.Y < 7)
            {
                p = new Position(Position.X, Position.Y + 1);
                if (isSpaceAvailable(b, p))
                    boards.Add(new Board(b, Position, p, this));

                p = new Position(Position.X - 1, Position.Y + 1);
                if (Position.X != 0 && isSpaceAvailable(b, p))
                {
                    boards.Add(new Board(b, Position, p, this));
                }

                p = new Position(Position.X + 1, Position.Y + 1);
                if (Position.X != 7 && isSpaceAvailable(b, p))
                {
                    boards.Add(new Board(b, Position, p, this));
                }
            }

            if (Position.Y > 0)
            {
                p = new Position(Position.X, Position.Y - 1);
                if (isSpaceAvailable(b, p))
                    boards.Add(new Board(b, Position, p, this));

                p = new Position(Position.X - 1, Position.Y - 1);
                if (Position.X != 0 && isSpaceAvailable(b, p))
                {
                    boards.Add(new Board(b, Position, p, this));
                }

                p = new Position(Position.X + 1, Position.Y + 1);
                if (Position.X != 7 && isSpaceAvailable(b, p))
                {
                    boards.Add(new Board(b, Position, p, this));
                }
            }

            p = new Position(Position.X - 1, Position.Y);
            if (Position.X > 0 && isSpaceAvailable(b, p))
            {
                boards.Add(new Board(b, Position, p, this));
            }

            p = new Position(Position.X + 1, Position.Y);
            if (Position.X < 7 && isSpaceAvailable(b, p))
            {
                boards.Add(new Board(b, Position, p, this));
            }

            return boards;
        }

        private bool isSpaceAvailable(Board board, Position p)
        {
            return !board.IsOccupied(p) || board.IsOccupied(p) && board.PieceByPosition[p].White != White;
        }
    }
}
