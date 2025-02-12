using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessMate.Presentation.Controllers.Interface
{
    public interface IMultiplayerGameController
    {
        /// <summary>
        /// Draws the form.
        /// </summary>
        /// <param name="e">A PaintEventArgs object.</param>
        void PaintForm(PaintEventArgs e);

        /// <summary>
        /// Process the coordinates of a left mouse click.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        void SubmitPlayerClick(int x, int y);

        /// <summary>
        /// Leave the current game.
        /// </summary>
        /// <param name="e">Form closing options.</param>
        void QuitGame(FormClosingEventArgs e);
    }
}
