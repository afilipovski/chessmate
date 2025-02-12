using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessMate.Domain;

namespace ChessMate.Presentation.Controllers.Interface
{
    public interface IAiGameController
    {
        /// <summary>
        /// Instantiates a new AI game.
        /// </summary>
        void GenerateGame();

        /// <summary>
        /// Draws the form.
        /// </summary>
        /// <param name="e">A PaintEventArgs object.</param>
        void PaintForm(PaintEventArgs e);

        /// <summary>
        /// Deletes the current game and starts a new one.
        /// </summary>
        void NewGame();

        /// <summary>
        /// Process the coordinates of a left mouse click.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        void SubmitPlayerClick(int x, int y);

        /// <summary>
        /// Save current game.
        /// </summary>
        void SaveGame();

        /// <summary>
        /// Choose file to save game in.
        /// </summary>
        void SaveGameAs();

        /// <summary>
        /// Load game from save file.
        /// </summary>
        void OpenGame();

        /// <summary>
        /// Set the AI difficulty.
        /// </summary>
        /// <param name="difficulty">An AI difficulty.</param>
        void SetDifficulty(OpponentDifficulty difficulty);

        /// <summary>
        /// Quit the current game.
        /// </summary>
        /// <param name="e">Form closing options.</param>
        void ExitGame(FormClosingEventArgs e);

        /// <summary>
        /// Get the current AI difficulty.
        /// </summary>
        /// <returns>An AI difficulty.</returns>
        OpponentDifficulty GetDifficulty();
    }
}
