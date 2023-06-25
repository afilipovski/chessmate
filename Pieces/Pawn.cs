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
    internal class Pawn : Piece
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
            Position tempPos = new Position(Position.X, Position.Y - 1);
            void forward()
            {
                if (tempPos.X > 7) return;
                Board newBoard = new Board(b);
                /*if (tempPos.Y > 7)
                {
                    newBoard.PieceByPosition[Position] = new Queen(Position, White);
                    boards.Add(newBoard);
                }*/
                if (!b.IsOccupied(tempPos))
                {
                    newBoard.PieceByPosition[tempPos] = this;
                    newBoard.greenPositions.Add(tempPos);
                    Debug.WriteLine("Added " + tempPos.X + " " + tempPos.Y);
                    newBoard.PieceByPosition[Position] = null;
                    boards.Add(newBoard);
                }
            }
            forward();
            tempPos = new Position(Position.X, Position.Y - 2);
            forward();

            void capture()
            {
                if (tempPos.X > 7 || tempPos.Y > 7 || tempPos.X < 0) return;
                Board newBoard = new Board(b);
                if (!b.IsOccupied(tempPos) || b.PieceByPosition[tempPos].White == this.White)
                {
                    if (!b.IsOccupied(new Position(tempPos.X, tempPos.Y + 1))) return;
                    //en passant
                    else newBoard.PieceByPosition[new Position(tempPos.X, tempPos.Y + 1)] = null;
                }
                newBoard.PieceByPosition[tempPos] = this;
                newBoard.PieceByPosition[Position] = null;
                boards.Add(newBoard);
            }
            tempPos = new Position(Position.X + 1, Position.Y - 1);
            capture();
            tempPos = new Position(Position.X - 1, Position.Y - 1);
            capture();
            return boards;
        }
        
    }
}
