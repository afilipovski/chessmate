using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ChessMate.Domain;
using ChessMate.Domain.Exceptions;

namespace ChessMate.Presentation.Controllers
{
    /// <summary>
    /// Utility class for messages, dialogs, etc.
    /// </summary>
	public class UserInteractionUtils
	{
        /// <summary>
        /// Shows a message and executes a callback after the message.
        /// </summary>
        /// <param name="text">Message content.</param>
        /// <param name="title">A message title.</param>
        /// <param name="callback">A callback.</param>
        public static void ShowMessage(string text, string title, Action callback)
        {
            if (MessageBox.Show(text, title) == DialogResult.OK)
            {
                callback();
            }
        }

        /// <summary>
        /// Shows a safe file dialog.
        /// </summary>
        /// <returns>A file path.</returns>
        /// <exception cref="FilePathNotChosenException">If no file path is chosen.</exception>
        public static string ShowSaveFileDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "sav",
                Filter = "Saved Games (*.sav)|*.sav"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            throw new FilePathNotChosenException();
        }

        /// <summary>
        /// Shows a file open dialog.
        /// </summary>
        /// <returns>A file and file path.</returns>
        /// <exception cref="FileNotChosenException">If no file is chosen.</exception>
        public static OpenFileResult ShowOpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                DefaultExt = "sav",
                Filter = "Saved Games (*.sav)|*.sav"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return new OpenFileResult(ofd.FileName, ofd.OpenFile());
            }
            throw new FileNotChosenException();
        }

        /// <summary>
        /// Allows the user to confirm a task execution and executes a callback after the confirmation.
        /// </summary>
        /// <param name="text">A prompt content.</param>
        /// <param name="title">A prompt title.</param>
        /// <param name="callback">A callback.</param>
        /// <returns>A boolean on whether the confirmation was positive.</returns>
        public static bool ShowConfirmDialog(string text, string title, Action callback)
        {
            if (MessageBox.Show(text, title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                callback();
                return true;
            }
            return false;
        }
    }
}
