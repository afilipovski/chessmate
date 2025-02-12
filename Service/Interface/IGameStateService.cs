using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;
using ChessMate.Presentation;

namespace ChessMate.Service.Interface
{
    public interface IGameStateService
    {
        /// <summary>
        /// Saves a game state to a .sav file.
        /// </summary>
        /// <param name="gameState">A game state.</param>
        /// <param name="filePath">File path of a .sav file.</param>
        void SaveToFile(GameState gameState, string filePath);

        /// <summary>
        /// Load a game state from a file.
        /// </summary>
        /// <param name="fileStream">A file.</param>
        /// <returns>A game state.</returns>
        GameState LoadFromFile(Stream fileStream);
    }
}
