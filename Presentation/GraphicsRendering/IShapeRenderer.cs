using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Presentation.GraphicsRendering
{
    public interface IShapeRenderer<T>
    {
        /// <summary>
        /// Draws a shape that belongs to the chess board.
        /// </summary>
        /// <param name="graphics">A graphics object.</param>
        /// <param name="shape">A shape.</param>
        void Draw(Graphics graphics, T shape);
    }
}
