using ChessMate.Domain;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Service.Interface
{
    public interface IMultiplayerService
    {
        /// <summary>
        /// Fetch the multiplayer game of the user.
        /// </summary>
        /// <param name="username">A user's username.</param>
        /// <returns>A multiplayer game wrapped in an asynchronous task.</returns>
        Task<MultiplayerGame> GetMultiplayerGame(string username);

        /// <summary>
        /// Creates a multiplayer game for the user.
        /// </summary>
        /// <param name="username">A user's username.</param>
        /// <returns>A multiplayer game wrapped in an asynchronous task.</returns>
        Task<MultiplayerGame> CreateGame(string username);

        /// <summary>
        /// Allows the user to join another game.
        /// </summary>
        /// <param name="username">A user's username.</param>
        /// <param name="joinCode">A multiplayer game's join code.</param>
        /// <returns>A multiplayer game wrapped in an asynchronous task.</returns>
        Task<MultiplayerGame> JoinGame(string username, string joinCode);

        /// <summary>
        /// Submits the user's move to the game.
        /// </summary>
        /// <param name="username">A user's username.</param>
        /// <param name="joinCode">A multiplayer game's join code.</param>
        /// <param name="move">A player's move.</param>
        /// <returns>An asynchronous task.</returns>
        Task Move(string username, string joinCode, Move move);

        /// <summary>
        /// Deletes the game the user is playing.
        /// </summary>
        /// <param name="username">A user's username.</param>
        /// <param name="joinCode">A multiplayer game's join code.</param>
        /// <returns>An asynchronous task.</returns>
        Task LeaveGame(string username, string joinCode);
    }
}
