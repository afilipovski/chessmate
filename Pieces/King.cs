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
            Piece p = b.PieceByPosition[new Position(Position.X, 7)];
            if (!MovedSinceStart &&
                b.PieceByPosition[new Position(Position.X, 5)] == null &&
                b.PieceByPosition[new Position(Position.X, 6)] == null &&
                p is Rook rook
                && !rook.MovedSinceStart)
            {
                Board newBoard = new Board(b);
                newBoard.PieceByPosition[new Position(Position.X, 5)] = rook;
                newBoard.PieceByPosition[new Position(Position.X, 6)] = this;
                newBoard.PieceByPosition[new Position(Position.X, 7)]
                    = newBoard.PieceByPosition[new Position(Position.X, 4)] = null;
                boards.Add(newBoard);
            }

            // Surrounding spaces
            {
                boards.Add(new Board(b, new Position(Position.X, Position.Y + 1), this));

                if (Position.X != 0)
                {
                    boards.Add(new Board(b, new Position(Position.X - 1, Position.Y + 1), this));
                }

                if (Position.X != 7)
                {
                    boards.Add(new Board(b, new Position(Position.X + 1, Position.Y + 1), this));
                }
            }

            if (Position.Y > 0)
            {
                boards.Add(new Board(b, new Position(Position.X, Position.Y - 1), this));

                if (Position.X != 0)
                {
                    boards.Add(new Board(b, new Position(Position.X - 1, Position.Y - 1), this));
                }

                if (Position.X != 7)
                {
                    boards.Add(new Board(b, new Position(Position.X + 1, Position.Y + 1), this));
                }
            }

            if (Position.X > 0)
            {
                boards.Add(new Board(b, new Position(Position.X - 1, Position.Y), this));
            }

            if (Position.X < 7)
            {
                boards.Add(new Board(b, new Position(Position.X + 1, Position.Y), this));
            }

            return boards;
        }
    }
}
