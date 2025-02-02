using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ChessMate.Domain.Positions;

namespace ChessMate.Domain.Pieces
{
    [Serializable]
    public class Queen : ContinuousPathPiece
    {
        public Queen(Position position, bool white) : base(position, white)
        {
        }

        public override Piece Clone()
        {
            Queen p = new Queen(this.Position, this.White);
            return p;
        }

        public override List<Board> PossibleMoves(Board b)
        {
            List<Board> boards = new List<Board>();

            Bishop bishop = new Bishop(this);
            Rook rook = new Rook(this);

            b.PieceByPosition[Position] = bishop;
            boards.AddRange(bishop.PossibleMoves(b)
                .Select(board =>
                {
                    Position p = board.NewPos;
                    Piece queen = Clone();
                    board.PieceByPosition[p] = queen;
                    queen.Position = p;
                    return board;
                })
            );

            b.PieceByPosition[Position] = rook;
            boards.AddRange(rook.PossibleMoves(b)
                .Select(board =>
                {
                    Position p = board.NewPos;
                    Piece queen = Clone();
                    board.PieceByPosition[p] = queen;
                    queen.Position = p;
                    return board;
                })
            );

            b.PieceByPosition[Position] = this;

            return boards;
        }

        public override string Name() => "queen";
    }
}
