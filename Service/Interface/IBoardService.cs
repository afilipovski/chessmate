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
        /// <summary>
        /// Generates all successive states of a board for every possible move for every piece in the board.
        /// </summary>
        /// <param name="board">A board.</param>
        /// <returns>A list of all successive states of the board.</returns>
        List<Board> GenerateSuccessiveStates(Board board);

        /// <summary>
        /// Fetches successor state for the piece at a clicked position. If a player presses his own piece, it colors all possible positions to move to. If the player chooses one of those positions, the colors are removed.
        /// </summary>
        /// <param name="position">A clicked board tile position.</param>
        /// <param name="board">A board state.</param>
        /// <param name="successiveStates">A list of successive states from a previous position click.</param>
        /// <returns>A board state.</returns>
        Board GetSuccessorStateForClickedPosition(Position position, Board board, List<Board> successiveStates);

        /// <summary>
        /// Determines whether the king piece of a given color is in check.
        /// </summary>
        /// <param name="board">A board state.</param>
        /// <param name="isKingWhite">The color of the king.</param>
        /// <returns>A boolean on whether the desired king is in check.</returns>
        bool IsKingInCheck(Board board, bool isKingWhite);

        /// <summary>
        /// Determines whether there are any possible successor states of a board state.
        /// </summary>
        /// <param name="board">A board state.</param>
        /// <returns>A boolean on whether there are any possible successor states of the board state.</returns>
        bool PossibleMovesNotExisting(Board board);

        /// <summary>
        /// Fetches a colored version of the position of the king whose turn it is in the current board state.
        /// </summary>
        /// <param name="board">A board state.</param>
        /// <returns>A red-colored position if a king is in check or null otherwise.</returns>
        ColoredPosition GetColoredKingCheckPosition(Board board);
    }
}
