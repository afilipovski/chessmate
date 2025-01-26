using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Presentation.GraphicsRenderers
{
    public interface IShapeRenderer<T>
    {
        void Draw(Graphics graphics, T shape);
    }
}
