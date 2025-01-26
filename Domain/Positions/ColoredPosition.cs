using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessMate.Domain;

namespace ChessMate.Domain.Positions
{
    public static class PositionColor
    {
        public static Color Green = Color.FromArgb(50, 0, 255, 0);
        public static Color Red = Color.FromArgb(50, 255, 0, 0);
        public static Color Blue = Color.FromArgb(50, 0, 0, 255);
	}

    [Serializable]
    public class ColoredPosition : Position
    {
        public Color Color { get; set; } = PositionColor.Green;
        
        public ColoredPosition(string stringpos) : base(stringpos)
        {
        }

        public ColoredPosition(Position p) : base(p)
        {
        }

        public ColoredPosition(int x, int y) : base(x, y)
        {
        }

        public ColoredPosition(Position p, Color c) : base(p)
        {
            Color = c;
        }

        public override String Name() => "colored-position";
    }
}
