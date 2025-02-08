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
    public class MultiplayerOverlayRenderer : IShapeRenderer<MultiplayerGame>
    {
        public void Draw(Graphics graphics, MultiplayerGame multiplayerGame)
        {
            SolidBrush sb = new SolidBrush(Color.Black);
            SolidBrush sbs = new SolidBrush(Color.Black);

            int textOffsetX = 0;
            int textOffsetY = 0;

            Font f = new Font("Arial", 15);

            graphics.DrawString("Join code: " + multiplayerGame.JoinCode, f, sbs, textOffsetX, textOffsetY);

            if (multiplayerGame != null && !string.IsNullOrEmpty(multiplayerGame.OpponentUsername))
            {
                graphics.DrawString("Opponent: " + multiplayerGame.OpponentUsername, f, sbs, textOffsetX, textOffsetY + 20);
            }
        }
    }
}
