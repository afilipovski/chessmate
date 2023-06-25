using System.Collections.Generic;
using System.Drawing;

namespace ChessMate.Pieces
{
    // Lovec
    public class Bishop : ContinuousPathPiece
    {
        public Bishop(ContinuousPathPiece cpp) : base(cpp)
        {
        }

        public Bishop(Position position, bool white) : base(position, white)
        {
        }

        public override Bitmap GetBitmap(Graphics g)
        {
            return White ? Properties.Resources.w_bishop_png_shadow_1024px
                : @Properties.Resources.b_bishop_png_shadow_1024px;
        }

        public override List<Board> PossibleMoves(Board b)
        {
            List<Board> boards = new List<Board>();

            // top right
            findValidPositions(
                new Position(Position.X + 1, Position.Y + 1),
                b,
                boards,
                p => p.X <= 7 && p.Y <= 7,
                p =>
                {
                    p.X++;
                    p.Y++;
                }
            );

            // top left
            findValidPositions(
                new Position(Position.X - 1, Position.Y + 1),
                b,
                boards,
                p => p.X >= 0 && p.Y <= 7,
                p =>
                {
                    p.X--;
                    p.Y++;
                }
            );

            // bottom left
            findValidPositions(
                new Position(Position.X - 1, Position.Y - 1),
                b,
                boards,
                p => p.X >= 0 && p.Y >= 0,
                p =>
                {
                    p.X--;
                    p.Y--;
                }
            );

            // bottom right
            findValidPositions(
                new Position(Position.X + 1, Position.Y - 1),
                b,
                boards,
                p => p.X <= 7 && p.Y >= 0,
                p =>
                {
                    p.X++;
                    p.Y--;
                }
            );

            return boards;
        }
    }
}
