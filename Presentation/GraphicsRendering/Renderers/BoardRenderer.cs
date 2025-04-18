﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;
using ChessMate.Domain.Pieces;
using ChessMate.Domain.Positions;

namespace ChessMate.Presentation.GraphicsRendering.Renderers
{
    public class BoardRenderer : IShapeRenderer<Board>
    {
        private bool whitePov;

        public BoardRenderer(bool whitePov)
        {
            _pieceRenderer = new PieceRenderer(whitePov);
            _positionRenderer = new PositionRenderer(whitePov);
        }

        private readonly IShapeRenderer<Piece> _pieceRenderer;
        private readonly IShapeRenderer<Position> _positionRenderer;

        public void Draw(Graphics graphics, Board shape)
        {
            for (int i = 0; i < 64; ++i)
            {
                _positionRenderer.Draw(graphics, new Position(i % 8, i / 8));
            }
            foreach (Position position in shape.PieceByPosition.Keys)
            {
                if (shape.PieceByPosition[position] == null) continue;
                _pieceRenderer.Draw(graphics, shape.PieceByPosition[position]);
            }
        }
    }
}
