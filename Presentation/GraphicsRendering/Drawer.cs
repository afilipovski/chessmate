using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain.Positions;
using ChessMate.Domain;
using ChessMate.Presentation.GraphicsRendering.Renderers;

namespace ChessMate.Presentation.GraphicsRendering
{
    public class Drawer
    {
        private readonly IShapeRenderer<string> _overlayRenderer = new OverlayRenderer();
        private readonly IShapeRenderer<Position> _positionRenderer = new PositionRenderer(false);
        private readonly IShapeRenderer<Board> _boardRenderer = new BoardRenderer(false);

        public void DrawChessBoardForm(GameState gameState, Graphics graphics)
        {
            _boardRenderer.Draw(graphics, gameState.Board);
            foreach (Board sb in gameState.SuccessiveBoards)
            {
                _positionRenderer.Draw(graphics, sb.NewPos);
            }
            if (gameState.CheckPosition is ColoredPosition)
            {
                _positionRenderer.Draw(graphics, gameState.CheckPosition);
            }
            if (!gameState.Board.WhiteTurn)
            {
                _overlayRenderer.Draw(graphics, "AI turn...");
            }
        }
    }
}
