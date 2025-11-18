using System;
using System.Drawing;

namespace Utils.Mathematical
{
    public struct Point2D : IEquatable<Point2D>
    {
        public int X;
        public int Y;

        public static Point2D Zero => new(0, 0);

        public Point2D()
        {
            X = 0;
            Y = 0;
        }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point2D(int xy)
        {
            X = xy;
            Y = xy;
        }

        public static Point2D operator -(Point2D me)
        {
            return new(-me.X, -me.Y);
        }

        public static Point2D operator -(Point2D left, Point2D right)
        {
            return new(left.X - right.X, left.Y - right.Y);
        }

        public static Point2D operator +(Point2D left, Point2D right)
        {
            return new(left.X + right.X, left.Y + right.Y);
        }

        public static Point2D operator *(Point2D point, int num)
        {
            return new(point.X * num, point.Y * num);
        }

        public static Point2D operator *(int num, Point2D point)
        {
            return new(point.X * num, point.Y * num);
        }

        /// <exception cref="DivideByZeroException">除数不能为0</exception>
        public static Point2D operator /(Point2D point, int num)
        {
            if (num == 0)
                throw new DivideByZeroException("除数不能为0");
            return new(point.X / num, point.Y / num);
        }

        public static bool operator ==(Point2D left, Point2D right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Point2D left, Point2D right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        public static explicit operator Vector2D(Point2D point)
        {
            return new(point.X, point.Y);
        }

        public static explicit operator Point(Point2D point)
        {
            return new(point.X, point.Y);
        }

        /// <summary>
        /// 曼哈顿距离
        /// </summary>
        public static int Manhattan(Point2D left, Point2D right)
        {
            return Math.Abs(left.X - right.X) + Math.Abs(left.Y - right.Y);
        }

        /// <summary>
        /// 切比雪夫距离
        /// </summary>
        public static int Chebyshev(Point2D left, Point2D right)
        {
            int x = Math.Abs(left.X - right.X);
            int y = Math.Abs(left.Y - right.Y);
            return Math.Max(x, y);
        }

        public static Point2D RandomPoint(int maxX, int maxY)
        {
            return RandomPoint(0, maxX, 0, maxY);
        }

        public static Point2D RandomPoint(int minX, int maxX, int minY, int maxY)
        {
            int x = Random.Shared.Next(minX, maxX);
            int y = Random.Shared.Next(minY, maxY);
            return new(x, y);
        }

        public void Normalize()
        {
            if (X < 0)
            {
                X = -1;
            }
            else if (X > 0)
            {
                X = 1;
            }

            if (Y < 0)
            {
                Y = -1;
            }
            else if (Y > 0)
            {
                Y = 1;
            }
        }

        public readonly bool Equals(Point2D other)
        {
            return this == other;
        }

        public override readonly bool Equals(object obj)
        {
            return obj is Point2D p && Equals(p);
        }

        public override readonly int GetHashCode()
        {
            return Tuple.Create(X, Y).GetHashCode();
        }

        public override readonly string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
