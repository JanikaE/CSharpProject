using System;

namespace Utils.Mathematical
{
    public struct Point3D : IEquatable<Point3D>
    {
        public int X;
        public int Y;
        public int Z;

        public static Point3D Zero => new(0, 0, 0);

        public Point3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(int xyz)
        {
            X = xyz;
            Y = xyz;
            Z = xyz;
        }

        public static Point3D operator -(Point3D me)
        {
            return new(-me.X, -me.Y, -me.Z);
        }

        public static Point3D operator -(Point3D left, Point3D right)
        {
            return new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Point3D operator +(Point3D left, Point3D right)
        {
            return new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Point3D operator *(Point3D point, int num)
        {
            return new(point.X * num, point.Y * num, point.Z * num);
        }

        public static Point3D operator *(int num, Point3D point)
        {
            return new(point.X * num, point.Y * num, point.Z * num);
        }

        /// <exception cref="DivideByZeroException">除数不能为0</exception>
        public static Point3D operator /(Point3D point, int num)
        {
            if (num == 0)
                throw new DivideByZeroException("除数不能为0");
            return new(point.X / num, point.Y / num, point.Z / num);
        }

        public static bool operator ==(Point3D left, Point3D right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        public static bool operator !=(Point3D left, Point3D right)
        {
            return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
        }

        public static explicit operator Vector3D(Point3D point)
        {
            return new(point.X, point.Y, point.Z);
        }

        /// <summary>
        /// 曼哈顿距离
        /// </summary>
        public static int Manhattan(Point3D left, Point3D right)
        {
            return Math.Abs(left.X - right.X) + Math.Abs(left.Y - right.Y) + Math.Abs(left.Z - right.Z);
        }

        /// <summary>
        /// 切比雪夫距离
        /// </summary>
        public static int Chebyshev(Point3D left, Point3D right)
        {
            int x = Math.Abs(left.X - right.X);
            int y = Math.Abs(left.Y - right.Y);
            int z = Math.Abs(left.Z - right.Z);
            return Math.Max(Math.Max(x, y), z);
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

            if (Z < 0)
            {
                Z = -1;
            }
            else if (Z > 0)
            {
                Z = 1;
            }
        }

        public readonly bool Equals(Point3D other)
        {
            return this == other;
        }

        public override readonly bool Equals(object obj)
        {
            return obj is Point3D p && Equals(p);
        }

        public override readonly int GetHashCode()
        {
            return Tuple.Create(X, Y, Z).GetHashCode();
        }

        public override readonly string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}
