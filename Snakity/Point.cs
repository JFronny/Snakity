/*using System;

namespace Snakity
{
    public struct Point
    {
        public bool Equals(Point other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) => obj is Point other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Point left, Point right) => left.X == right.X && left.Y == right.Y;

        public static bool operator !=(Point left, Point right) => left.X != right.X || left.Y != right.Y;

        public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);

        public static Point operator -(Point left, Point right) => new Point(left.X - right.X, left.Y - right.Y);
    }
}*/