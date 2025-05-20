using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Presentation.Controllers.Interface
{
    public interface IJoinMultiplayerController
    { 
        /// <summary>
        /// Validates if the user can join a game and joins if valid.
        /// </summary>
        /// <param name="username">A user's username.</param>
        /// <param name="joinCode">A multiplayer game's join code.</param>
        /// <returns>An asynchronous task.</returns>
        Task ValidateForm(string joinCode);
    }
}
