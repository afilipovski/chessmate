using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ChessMate.Pieces
{
    public class King : Piece
    {
        public bool MovedSinceStart { get; set; }

        public King(Position position, bool white) : base(position, white)
        {
            MovedSinceStart = false;
        }

        public override Bitmap GetBitmap(Graphics g)
        {
            return White ? Properties.Resources.w_king_png_shadow_1024px
                : @Properties.Resources.b_king_png_shadow_1024px;
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
                Board board = new Board(b);
                board.PieceByPosition[new Position(Position.X, 5)] = rook;
                board.PieceByPosition[new Position(Position.X, 6)] = this;
                board.PieceByPosition[new Position(Position.X, 7)] = null;
                board.PieceByPosition[new Position(Position.X, 4)] = null;
                boards.Add(board);
            }

            // Outcomes from other figures
            List<Board> otherOutcomes = b.PieceByPosition.Values.ToList()
                .Where(p => p.White != White)
                .Select(p =>
                {
                    if (p is King k)
                        return k.AvailableSurroundingPositions(b);
                    return p.PossibleMoves(b);
                })
                .SelectMany(l => l)
                .ToList();

            // Check all surrounding if they are available and aren't capturable from
            getSurroundingPositions()
                .Where(p => Board.IsInBoard(p) && isSpaceAvailable(b, p))
                .Where(p => !otherOutcomes.Any(board =>
                    board.IsOccupied(p) &&
                    board.PieceByPosition[p].White != White
                ))
                .Select(p => new Board(b, Position, p, this))
                .ToList()
                .ForEach(board => boards.Add(board));

            return boards;
        }

        private bool isSpaceAvailable(Board board, Position p)
        {
            return !board.IsOccupied(p) || board.IsOccupied(p) && board.PieceByPosition[p].White != White;
        }

        public List<Board> AvailableSurroundingPositions(Board b)
        {
            return getSurroundingPositions()
                .Where(p => Board.IsInBoard(p) && isSpaceAvailable(b, p))
                .Select(p => new Board(b, Position, p, this))
                .ToList();
        }

        private List<Position> getSurroundingPositions()
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
    }
}
