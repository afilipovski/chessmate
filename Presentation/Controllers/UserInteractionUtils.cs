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
	public class UserInteractionUtils
	{
        public static void ShowMessage(string text, string title, Action callback)
        {
            if (MessageBox.Show(text, title) == DialogResult.OK)
            {
                callback();
            }
        }

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
