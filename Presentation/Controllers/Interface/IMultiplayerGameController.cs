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
        void PaintForm(PaintEventArgs e);
        void SubmitPlayerClick(int x, int y);
        void QuitGame(FormClosingEventArgs e);
    }
}
