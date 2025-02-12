using System;
using System.Diagnostics;
using System.Drawing;

namespace ChessMate.Domain.Positions
{
    [Serializable]
    public class Position : IEquatable<Position>
    {
        public readonly int X;
        public readonly int Y;
        public readonly bool White;

        /// <summary>
        /// Initializes position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public Position(int x, int y)
        {
            X = x;
            Y = y;
            White = (X + Y) % 2 == 0;
        }

        /// <summary>
        /// Initializes position from string coordinates.
        /// </summary>
        /// <param name="stringpos">Coordinates</param>
        public Position(string stringpos)
        {
            //c3
            X = stringpos[0] - 'a';
            Y = 7 - (stringpos[1] - '1');
            White = (X + Y) % 2 == 0;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="p">A position.</param>
        public Position(Position p)
        {
            X = p.X;
            Y = p.Y;
            White = p.White;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position && Equals(position);
        }

        public bool Equals(Position other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        /// <summary>
        /// Converts the position to a hash code for a dictionary.
        /// </summary>
        /// <returns>A hashcode.</returns>
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Name for shape rendering.
        /// </summary>
        /// <returns>A shape name.</returns>
        public virtual String Name() => "position";

        public override string ToString()
        {
            char column = (char)('a' + X);
            char row = (char)('1' + 7 - Y);

            return $"{column}{row}";
        }
    }
}
