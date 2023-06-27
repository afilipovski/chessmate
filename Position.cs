using System;
using System.Diagnostics;
using System.Drawing;

namespace ChessMate
{
    public class Position : IEquatable<Position>
    {
        public int X;
        public int Y;
        public bool White;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
            White = (X + Y) % 2 == 0;
        }

        public Position(string stringpos)
        {
            //c3
            X = stringpos[0] - 'a';
            Y = stringpos[1] - '1';
            White = (X + Y) % 2 == 0;

        }

        // Copy constructor
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

        public virtual void Draw(Graphics g)
        {
            Brush b = new SolidBrush(White ? Color.White : Color.DarkSlateGray);
            g.FillRectangle(b, X * Board.HEIGHT + Board.OFFSET, Y * Board.HEIGHT, Board.HEIGHT, Board.HEIGHT);
            g.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), X * Board.HEIGHT + Board.OFFSET, Y * Board.HEIGHT, Board.HEIGHT, Board.HEIGHT);
            b.Dispose();
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
