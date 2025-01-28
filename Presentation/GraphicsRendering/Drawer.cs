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
        private readonly IShapeRenderer<Position> _positionRenderer = new PositionRenderer();
        private readonly IShapeRenderer<Board> _boardRenderer = new BoardRenderer();

        public void DrawChessBoardForm(GameState gameState, Graphics graphics)
        {
            _boardRenderer.Draw(graphics, gameState.Board);
            foreach (Board sb in gameState.successiveBoards)
            {
                _positionRenderer.Draw(graphics, sb.NewPos);
            }
            if (gameState.checkPosition is ColoredPosition)
            {
                _positionRenderer.Draw(graphics, gameState.checkPosition);
            }
            if (!gameState.Board.WhiteTurn)
            {
                _overlayRenderer.Draw(graphics, "AI turn...");
            }
        }
    }
}
