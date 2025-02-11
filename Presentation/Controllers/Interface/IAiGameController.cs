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
        void GenerateGame();
        void PaintForm(PaintEventArgs e);
        void NewGame();
        void SubmitPlayerClick(int x, int y);
        void SaveGame();
        void SaveGameAs();
        void OpenGame();
        void SetDifficulty(OpponentDifficulty difficulty);
        void ExitGame(FormClosingEventArgs e);
        OpponentDifficulty GetDifficulty();
    }
}
