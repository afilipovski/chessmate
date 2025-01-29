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
            _functions[shape.Name()].Invoke(graphics, shape);
        }

        private delegate void ColorPosition(Graphics graphics, Position position);

        private readonly Dictionary<string, ColorPosition> _functions = new Dictionary<string, ColorPosition> {
            { "position", _drawRegularShape },
            { "colored-position", _drawColoredShape }
        };

        private static ColorPosition _drawRegularShape = (graphics, position) =>
        {
            Brush b = new SolidBrush(position.White ? Color.White : Color.DarkSlateGray);
            graphics.FillRectangle(b, position.X * Board.TileSide + Board.OffsetX, position.Y * Board.TileSide + Board.OffsetY, Board.TileSide, Board.TileSide);
            graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), position.X * Board.TileSide + Board.OffsetX, position.Y * Board.TileSide + Board.OffsetY, Board.TileSide, Board.TileSide);
            b.Dispose();
        };

        private static ColorPosition _drawColoredShape = (graphics, position) =>
        {
            ColoredPosition coloredPosition = (ColoredPosition)position;
            graphics.FillRectangle(new SolidBrush(coloredPosition.Color), coloredPosition.X * Board.TileSide + Board.OffsetX, coloredPosition.Y * Board.TileSide + Board.OffsetY, Board.TileSide, Board.TileSide);
        };
    }
}
