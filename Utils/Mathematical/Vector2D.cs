using System;

namespace Utils.Mathematical
{
    /// <summary>
    /// 二维矢量
    /// </summary>
    public struct Vector2D : IEquatable<Vector2D>
    {
        public float X;
        public float Y;

        public float Length
        {
            readonly get
            {
                return (float)Math.Sqrt(LengthSquared);
            }
            set
            {
                double angle = Angle;
                X = (float)Math.Cos(angle) * value;
                Y = (float)Math.Sin(angle) * value;
            }
        }

        public readonly float LengthSquared => X * X + Y * Y;

        public double Angle
        {
            readonly get
            {
                return Math.Atan2(Y, X);
            }
            set
            {
                float len = Length;
                X = (float)Math.Cos(value) * len;
                Y = (float)Math.Sin(value) * len;
            }
        }

        public static Vector2D Zero => new(0, 0);

        public Vector2D()
        {
            X = 0; 
            Y = 0;
        }

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2D(float xy)
        {
            X = xy;
            Y = xy;
        }

        public Vector2D(double angle, float length)
        {
            X = (float)(Math.Cos(angle) * length);
            Y = (float)(Math.Sin(angle) * length);
        }

        public static Vector2D operator -(Vector2D me)
        {
            me.X = -me.X;
            me.Y = -me.Y;
            return me;
        }

        public static Vector2D operator -(Vector2D left, Vector2D right)
        {
            return new(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            return new(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2D operator *(Vector2D vector, float scale)
        {
            vector.X *= scale;
            vector.Y *= scale;
            return vector;
        }

        public static Vector2D operator *(float scale, Vector2D vector)
        {
            return vector * scale;
        }

        public static bool operator ==(Vector2D left, Vector2D right)
        {
            return DistanceSquare(left, right) < float.Epsilon;
        }

        public static bool operator !=(Vector2D left, Vector2D right)
        {
            return !(left == right);
        }

        public static explicit operator Point2D(Vector2D vector)
        {
            return new Point2D((int)vector.X, (int)vector.Y);
        }

        /// <summary>
        /// 点乘（内积）
        /// </summary>
        public static float DotProduct(Vector2D left, Vector2D right)
        {
            return left.X * right.X + left.Y * right.Y;
        }

        /// <summary>
        /// 叉乘（外积）
        /// </summary>
        public static float CrossProduct(Vector2D left, Vector2D right)
        {
            return left.Length * right.Length * (float)Math.Sin(AngleAbs(left, right));
        }

        public static float Distance(Vector2D left, Vector2D right)
        {
            return (float)Math.Sqrt(DistanceSquare(left, right));
        }

        public static float DistanceSquare(Vector2D left, Vector2D right)
        {
            float x = left.X - right.X;
            float y = left.Y - right.Y;
            return x * x + y * y;
        }

        /// <summary>
        /// 求两个矢量的夹角（0到π）
        /// </summary>
        public static double AngleAbs(Vector2D left, Vector2D right)
        {
            double angle = Math.Abs(left.Angle - right.Angle);
            if (angle > Math.PI)
                angle = Math.PI * 2 - angle;
            return angle;
        }

        public readonly Vector2D ToVertical()
        {
            return new(Angle + Math.PI / 2, Length);
        }

        public void Normalize()
        {
            if (Length == 0)
                return;
            float num = 1f / Length;
            X *= num;
            Y *= num;
        }

        public readonly Vector2D ToNormal()
        {
            if (Length == 0)
                return Zero;
            float num = 1f / Length;
            return new(X * num, Y * num);
        }

        public readonly bool Equals(Vector2D value)
        {
            return this == value;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Vector2D o && Equals(o);
        }

        public override readonly int GetHashCode()
        {
            return Tuple.Create(X, Y).GetHashCode();
        }

        public override readonly string ToString()
        {
            return $"({X},{Y})";
        }
    }
}