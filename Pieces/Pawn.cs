using ChessMate.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChessMate
{
    [Serializable]
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
            if (b.WhiteTurn ? Position.Y == 0 : Position.Y == 7)
            {
                Board newBoard = new Board(b);
                newBoard.PieceByPosition[Position] = new Queen(Position, White);
                boards.Add(newBoard);
            }

            Position tempPos = new Position(Position.X, b.WhiteTurn ? Position.Y - 1 : Position.Y + 1);
            void forward()
            {
                if (tempPos.Y > 7 || tempPos.Y < 0) return;
                if (!b.IsOccupied(tempPos))
                {
                    boards.Add(new Board(b, Position, tempPos, this));
                }
            }

            forward();
            tempPos = new Position(Position.X, b.WhiteTurn ? Position.Y - 2 : Position.Y + 2);
            forward();

            void capture()
            {
                if (tempPos.X > 7 || tempPos.X < 0 || tempPos.Y > 7 || tempPos.Y < 0) return;
                Board newBoard = new Board(b, Position, tempPos, this);

                //en passant position
                Position tempPos2 = new Position(tempPos.X, b.WhiteTurn ? tempPos.Y + 1 : tempPos.Y - 1);
                if ((tempPos2.Y < 8 && tempPos2.Y >= 0) && b.IsOccupied(tempPos2) && b.PieceByPosition[tempPos2].GetType().Name == "Pawn" 
                    && b.PieceByPosition[tempPos2].White != this.White)
                {
                    newBoard.PieceByPosition[tempPos2] = null;
                }

                else if (!b.IsOccupied(tempPos) || b.PieceByPosition[tempPos].White == this.White) return;
                boards.Add(newBoard);
            }

            tempPos = new Position(Position.X + 1, b.WhiteTurn ? Position.Y - 1 : Position.Y + 1);
            capture();
            tempPos = new Position(Position.X - 1, b.WhiteTurn ? Position.Y - 1 : Position.Y + 1);
            capture();
            return boards;
        }

        public override Piece Clone()
        {
            Pawn p = new Pawn(this.Position, this.White);
            return p;
        }

    }
}
