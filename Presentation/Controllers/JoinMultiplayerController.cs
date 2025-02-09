using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;
using System.Windows.Forms;
using ChessMate.Presentation.Interface;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;

namespace ChessMate.Presentation.Controllers
{
    public class JoinMultiplayerController
    {
        private readonly IMultiplayerService _multiplayerService = MultiplayerService.Instance;
        private readonly JoinMpGameForm _form;

        public JoinMultiplayerController(JoinMpGameForm form)
        {
            _form = form;
        }

        public async Task ValidateForm(string username, string joinCode)
        {
            if (username == "")
            {
                UserInteractionUtils.ShowMessage("Please enter a username!", "Error", () => {});
                return;
            }

            if (joinCode != "" && joinCode.Length != 5)
            {
                UserInteractionUtils.ShowMessage("Please enter a valid join code!", "Error", () => {});
                return;
            }

            MultiplayerGame response;
            try
            {
                if (string.IsNullOrEmpty(joinCode))
                {
                    response = await _multiplayerService.CreateGame(username);
                    joinCode = response.JoinCode;
                }
                else
                {
                    response = await _multiplayerService.JoinGame(username, joinCode);
                }
            }
            catch (Exception exception)
            {
                UserInteractionUtils.ShowMessage(exception.Message, "Error", () => {});
                return;
            }

            _form.Hide();
            bool whitePov = _form.IsJoinCodeNull();
            MultiplayerGameForm multiplayerGame = new MultiplayerGameForm(whitePov, response);
            multiplayerGame.ShowDialog();
            _form.Show();

            await _multiplayerService.LeaveGame(username, joinCode);
        }
    }
}
