﻿using System;
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
        private readonly bool _whitePov;

        /// <summary>
        /// Initializes the drawer.
        /// </summary>
        /// <param name="whitePov">The color of the player's pieces.</param>
        public Drawer(bool whitePov = true)
        {
            _positionRenderer = new PositionRenderer(whitePov);
            _boardRenderer = new BoardRenderer(whitePov);
            _whitePov = whitePov;
        }

        private readonly IShapeRenderer<string> _overlayRenderer = OpponentMoveMessageOverlayRenderer.Instance;
        private readonly IShapeRenderer<Position> _positionRenderer;
        private readonly IShapeRenderer<Board> _boardRenderer;

        /// <summary>
        /// Draws the chess board.
        /// </summary>
        /// <param name="gameState">A board state.</param>
        /// <param name="graphics">A graphics object.</param>
        public void DrawChessBoard(GameState gameState, Graphics graphics)
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
            if (!(gameState.Board.WhiteTurn == _whitePov))
            {
                _overlayRenderer.Draw(graphics, "Opponent turn...");
            }
        }
    }
}
