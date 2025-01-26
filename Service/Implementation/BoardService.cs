using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Pieces;
using ChessMate.Service.Interface;

namespace ChessMate.Service.Implementation
{
    public class BoardService : IBoardService
    {
        public Board GetSuccessorStateForClickedPosition(Position position, Board board, List<Board> successiveStates)
        {
            Position clickedPosition = new Position((position.X - Board.OFFSET_X) / Board.TILE_SIDE, (position.Y - Board.OFFSET_Y) / Board.TILE_SIDE);
            if (!board.WhiteTurn || !Board.IsInBoard(clickedPosition))
                return board;

            Piece clickedPiece = board.PieceByPosition[clickedPosition];


            if (clickedPiece == null || !clickedPiece.White)
            {
                if (board.CurrentClickedPiece == null) return board;

                //impossible move, i.e. unclicked the currently selected piece

                if (!successiveStates.Any(ss => ss.NewPos.Equals(clickedPosition)))
                {
                    board.CurrentClickedPiece = null;
                    successiveStates.Clear();
                    return board;
                }

                Board res = successiveStates.Find(ss => ss.NewPos.Equals(clickedPosition));
                successiveStates.Clear();
                return res;
            }
            else if (clickedPiece.White)
            {
                successiveStates.Clear();
                board.CurrentClickedPiece = clickedPiece;
                board.CurrentClickedPiece.PossibleMoves(board)
                    .Where(b => !IsKingInCheck(b, true)).ToList()
                    .ForEach(b => { successiveStates.Add(b); });
            }

            return board;
        }

        public List<Board> GenerateSuccessiveStates(Board board)
        {
            List<Board> result = new List<Board>();

            List<Piece> pieces = board.PieceByPosition.Values.ToList();
            foreach (Piece piece in pieces)
            {
                if (piece == null) continue;
                if (piece.White != board.WhiteTurn)
                    continue;
                List<Board> moves = piece.PossibleMoves(board);
                foreach (Board move in moves)
                {
                    result.Add(move);
                }
            }

            return result.Where(b => !IsKingInCheck(b, !b.WhiteTurn)).ToList();
        }

        public Position GetKingPositionIfInCheck(Board board, bool isKingWhite)
        {
            King king = null;
            foreach (Piece piece in board.PieceByPosition.Values)
            {
                if (piece is null) continue;
                if (piece is King kx && piece.White == isKingWhite)
                {
                    king = kx;
                    break;
                }
            }

            foreach (Piece piece in board.PieceByPosition.Values.ToArray())
            {
                //skip friendly pieces
                if (piece is null || piece.White == king.White)
                    continue;
                //if piece can attack king, king is in check
                if (piece.PossibleMoves(board).Any(b => b.NewPos.Equals(king.Position)))
                    return king.Position;
            }
            return null;
        }

        public bool IsKingInCheck(Board board, bool isKingWhite)
        {
            return !(GetKingPositionIfInCheck(board, isKingWhite) is null);
        }

        public bool PossibleMovesNotExisting(Board board)
        {
            return GenerateSuccessiveStates(board).Count() == 0;
        }
    }
}
