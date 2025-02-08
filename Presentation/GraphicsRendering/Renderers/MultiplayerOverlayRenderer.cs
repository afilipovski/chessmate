using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;
using ChessMate.Service.Implementation;
using ChessMate.Service.Interface;

namespace ChessMate.Presentation.GraphicsRendering.Renderers
{
    public class MultiplayerOverlayRenderer : IShapeRenderer<string>
    {
        private string _username;
        private string _joinCode;

        private MultiplayerGame multiplayerGame;
        private IMultiplayerService _multiplayerService;
        public MultiplayerOverlayRenderer(string username, string joinCode)
        {
            _username = username;
            _joinCode = joinCode;
            _multiplayerService = new MultiplayerService();
        }
        public void Draw(Graphics graphics, string shape)
        {
            SolidBrush sb = new SolidBrush(Color.Black);
            SolidBrush sbs = new SolidBrush(Color.Black);

            int textOffsetX = 0;
            int textOffsetY = 0;

            Font f = new Font("Arial", 15);

            graphics.DrawString("Join code: " + _joinCode, f, sbs, textOffsetX, textOffsetY);

            if (multiplayerGame != null && !string.IsNullOrEmpty(multiplayerGame.Username2))
            {
                graphics.DrawString("Opponent: " + multiplayerGame.Username2, f, sbs, textOffsetX, textOffsetY + 20);
            }
        }

        public async Task GetUsernames()
        {
            while (this.multiplayerGame == null || string.IsNullOrEmpty(multiplayerGame.Username2))
            {
                MultiplayerGame multiplayerGame = await _multiplayerService.GetMultiplayerGame(_username);
                await Task.Delay(1000);
                this.multiplayerGame = multiplayerGame;
            }
        }
    }
}
