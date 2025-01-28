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
        public void Draw(Graphics graphics, Position shape)
        {
            Functions[shape.Name()].Invoke(graphics, shape);
        }

        private delegate void ColorPosition(Graphics graphics, Position position);

        private readonly Dictionary<string, ColorPosition> Functions = new Dictionary<string, ColorPosition> {
            { "position", DrawRegularShape },
            { "colored-position", DrawColoredShape }
        };

        private static ColorPosition DrawRegularShape = (graphics, position) =>
        {
            Brush b = new SolidBrush(position.White ? Color.White : Color.DarkSlateGray);
            graphics.FillRectangle(b, position.X * Board.TILE_SIDE + Board.OFFSET_X, position.Y * Board.TILE_SIDE + Board.OFFSET_Y, Board.TILE_SIDE, Board.TILE_SIDE);
            graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), position.X * Board.TILE_SIDE + Board.OFFSET_X, position.Y * Board.TILE_SIDE + Board.OFFSET_Y, Board.TILE_SIDE, Board.TILE_SIDE);
            b.Dispose();
        };

        private static ColorPosition DrawColoredShape = (graphics, position) =>
        {
            ColoredPosition coloredPosition = (ColoredPosition)position;
            graphics.FillRectangle(new SolidBrush(coloredPosition.Color), coloredPosition.X * Board.TILE_SIDE + Board.OFFSET_X, coloredPosition.Y * Board.TILE_SIDE + Board.OFFSET_Y, Board.TILE_SIDE, Board.TILE_SIDE);
        };
    }
}
