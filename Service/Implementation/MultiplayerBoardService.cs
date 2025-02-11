using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;
using ChessMate.Domain.Pieces;
using ChessMate.Domain.Positions;
using ChessMate.Service.Interface;

namespace ChessMate.Service.Implementation
{
    public class MultiplayerBoardService : IBoardService
    {
        private readonly bool _whitePov;

        public MultiplayerBoardService(bool whitePov)
        {
            _whitePov = whitePov;
        }

        public List<Board> GenerateSuccessiveStates(Board board)
        {
            return AiBoardService.Instance.GenerateSuccessiveStates(board);
        }

        public Board GetSuccessorStateForClickedPosition(Position position, Board board, List<Board> successiveStates)
        {
            bool isPlayerTurn = _whitePov == board.WhiteTurn;

            if (!isPlayerTurn || !Board.IsInBoard(position))
                return board;

            Piece clickedPiece = board.PieceByPosition[position];


            bool clickedPieceBelongsToPlayer = clickedPiece != null && clickedPiece.White == _whitePov;
            if (!clickedPieceBelongsToPlayer)
            {
                if (board.CurrentClickedPiece == null) return board;

                //impossible move, i.e. unclicked the currently selected piece

                if (!successiveStates.Any(ss => ss.NewPos.Equals(position)))
                {
                    board.CurrentClickedPiece = null;
                    successiveStates.Clear();
                    return board;
                }

                Board res = successiveStates.Find(ss => ss.NewPos.Equals(position));
                successiveStates.Clear();
                return res;
            }
            else if (clickedPieceBelongsToPlayer)
            {
                successiveStates.Clear();
                board.CurrentClickedPiece = clickedPiece;
                board.CurrentClickedPiece.PossibleMoves(board)
                    .Where(b => !IsKingInCheck(b, _whitePov)).ToList()
                    .ForEach(b => { successiveStates.Add(b); });
            }

            return board;
        }

        public bool IsKingInCheck(Board board, bool isKingWhite)
        {
            return AiBoardService.Instance.IsKingInCheck(board, isKingWhite);
        }

        public bool PossibleMovesNotExisting(Board board)
        {
            return AiBoardService.Instance.PossibleMovesNotExisting(board);
        }

        public ColoredPosition GetColoredKingCheckPosition(Board board)
        {
            return AiBoardService.Instance.GetColoredKingCheckPosition(board);
        }
    }
}
