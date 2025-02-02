using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;
using ChessMate.Domain.Positions;

namespace ChessMate.Service.Interface
{
    public interface IBoardService
    {
        List<Board> GenerateSuccessiveStates(Board board);
        Board GetSuccessorStateForClickedPosition(Position position, Board board, List<Board> successiveStates);
        Position GetKingPositionIfInCheck(Board board, bool isKingWhite);
        bool IsKingInCheck(Board board, bool isKingWhite);
        bool PossibleMovesNotExisting(Board board);
        ColoredPosition GetColoredKingCheckPosition(Board board);
    }
}
