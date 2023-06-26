using ChessMate.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate
{
    internal class Pawn : ContinuousPathPiece
    {
        public Pawn(Position position, bool white) : base(position, white)
        {
        }
        public override Bitmap GetBitmap(Graphics g)
        {
            return White ? Properties.Resources.w_pawn_png_shadow_1024px
                : @Properties.Resources.b_pawn_png_shadow_1024px;
        }

        public override List<Board> PossibleMoves(Board b)
        {
            List<Board> boards = new List<Board>();
            if (b.WhiteTurn ? Position.Y == 0 : Position.Y == 7)
            {
                Board newBoard = new Board(b);
                newBoard.PieceByPosition[Position] = new Queen(Position, White);
                boards.Add(newBoard);
            }

            Position tempPos = new Position(Position.X, b.WhiteTurn ? Position.Y - 1 : Position.Y + 1);
            void forward()
            {
                if (tempPos.X > 7 || tempPos.X < 0) return;
                Board newBoard = new Board(b);
                if (!b.IsOccupied(tempPos))
                {
                    newBoard.AddPosition(tempPos, this);
                    newBoard.PieceByPosition[Position] = null;
                    boards.Add(newBoard);
                }
            }

            forward();
            tempPos = new Position(Position.X, b.WhiteTurn ? Position.Y - 2 : Position.Y + 2);
            forward();

            void capture()
            {
                if (tempPos.X > 7 || tempPos.X < 0 || tempPos.Y > 7 || tempPos.Y < 0) return;
                Board newBoard = new Board(b);

                //en passant position
                Position tempPos2 = new Position(tempPos.X, b.WhiteTurn ? tempPos.Y + 1 : tempPos.Y - 1);
                if (b.IsOccupied(tempPos2) && b.PieceByPosition[tempPos2].GetType().Name == "Pawn" && b.PieceByPosition[tempPos2].White != this.White)
                {
                    newBoard.PieceByPosition[tempPos2] = null;
                }

                else if (!b.IsOccupied(tempPos) || b.PieceByPosition[tempPos].White == this.White) return;
                newBoard.AddPosition(tempPos, this);
                newBoard.PieceByPosition[Position] = null;
                boards.Add(newBoard);
            }

            tempPos = new Position(Position.X + 1, b.WhiteTurn ? Position.Y - 1 : Position.Y + 1);
            capture();
            tempPos = new Position(Position.X - 1, b.WhiteTurn ? Position.Y - 1 : Position.Y + 1);
            capture();
            return boards;
        }
        
    }
}
