using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;

namespace ChessMate.Presentation.GraphicsRendering.Renderers
{
    public class OverlayRenderer : IShapeRenderer<string>
    {
        public void Draw(Graphics graphics, string shape)
        {
            SolidBrush sb = new SolidBrush(Color.Black);
            SolidBrush sbs = new SolidBrush(Color.White);

            int boxOffsetX = Board.OffsetX + 3 * Board.TileSide;
            int boxOffsetY = Board.OffsetY + (int)(3.5 * Board.TileSide);

            int boxWidth = 2 * Board.TileSide;
            int boxHeight = Board.TileSide;

            float fontSize = (float)(boxHeight * 0.3);
            int textOffsetX = boxOffsetX;
            int textOffsetY = (int)(boxOffsetY + (boxHeight - fontSize) / 2);

            Font f = new Font("Arial", fontSize);

            graphics.FillRectangle(sb, boxOffsetX, boxOffsetY, boxWidth, boxHeight);
            graphics.DrawString("AI turn...", f, sbs, textOffsetX, textOffsetY - 10);
        }
    }
}
