using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;
using ChessMate.Domain.Positions;

namespace ChessMate.Presentation.GraphicsRendering.Renderers
{
    public class PositionRenderer : IShapeRenderer<Position>
    {
        private bool whitePov;

        public PositionRenderer(bool whitePov = true)
        {
            this.whitePov = whitePov;
        }

        public void Draw(Graphics graphics, Position shape)
        {
            switch (shape.Name())
            {
                case "position":
                    _drawRegularShape(graphics, shape);
                    break;
                case "colored-position":
                    _drawColoredShape(graphics, shape);
                    break;
            }
        }

        private void _drawRegularShape(Graphics graphics, Position position)
        {
            var positionX = !this.whitePov ? 7 - position.X : position.X;
            var positionY = !this.whitePov ? 7 - position.Y : position.Y;

            Brush b = new SolidBrush(position.White ? Color.White : Color.DarkSlateGray);
            graphics.FillRectangle(b, positionX * Board.TileSide + Board.OffsetX, positionY * Board.TileSide + Board.OffsetY, Board.TileSide, Board.TileSide);
            graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), positionX * Board.TileSide + Board.OffsetX, positionY * Board.TileSide + Board.OffsetY, Board.TileSide, Board.TileSide);
            b.Dispose();
        }

        private void _drawColoredShape(Graphics graphics, Position position)
        {
            var positionX = !this.whitePov ? 7 - position.X : position.X;
            var positionY = !this.whitePov ? 7 - position.Y : position.Y;

            ColoredPosition coloredPosition = (ColoredPosition)position;
            graphics.FillRectangle(new SolidBrush(coloredPosition.Color), positionX * Board.TileSide + Board.OffsetX, positionY * Board.TileSide + Board.OffsetY, Board.TileSide, Board.TileSide);
        }
    }
}
