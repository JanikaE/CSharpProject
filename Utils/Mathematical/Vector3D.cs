using System;

namespace Utils.Mathematical
{
    /// <summary>
    /// 三维矢量
    /// </summary>
    public struct Vector3D : IEquatable<Vector3D>
    {
        public float X;
        public float Y;
        public float Z;

        public float Length
        {
            readonly get
            {
                return (float)Math.Sqrt(LengthSquared);
            }
            set
            {
                Normalize();
                this *= value;
            }
        }

        public readonly float LengthSquared => X * X + Y * Y + Z * Z;

        public static Vector3D Zero => new(0, 0, 0);

        public Vector3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D(float xyz)
        {
            X = xyz;
            Y = xyz;
            Z = xyz;
        }

        public static Vector3D operator -(Vector3D me)
        {
            return new(-me.X, -me.Y, -me.Z);
        }

        public static Vector3D operator -(Vector3D left, Vector3D right)
        {
            return new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3D operator +(Vector3D left, Vector3D right)
        {
            return new(left.X + right.X, left.Y + right.Y, left.X + right.X);
        }

        public static Vector3D operator *(Vector3D vector, float num)
        {
            return new(vector.X * num, vector.Y * num, vector.Z * num);
        }

        public static Vector3D operator *(float num, Vector3D vector)
        {
            return vector * num;
        }

        /// <exception cref="DivideByZeroException">除数不能为0</exception>
        public static Vector3D operator /(Vector3D vector, float num)
        {
            if (num == 0)
                throw new DivideByZeroException("除数不能为0");
            return new(vector.X / num, vector.Y / num, vector.Z / num);
        }

        public static bool operator ==(Vector3D left, Vector3D right)
        {
            return Distance(left, right) < float.Epsilon;
        }

        public static bool operator !=(Vector3D left, Vector3D right)
        {
            return !(left == right);
        }

        public static explicit operator Point3D(Vector3D vector)
        {
            return new((int)vector.X, (int)vector.Y, (int)vector.Z);
        }

        /// <summary>
        /// 点乘（内积）
        /// </summary>
        public static float DotProduct(Vector3D left, Vector3D right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        /// <summary>
        /// 叉乘（外积）
        /// </summary>
        public static Vector3D CrossProduct(Vector3D left, Vector3D right)
        {
            float x = left.Y * right.Z - left.Z * right.Y;
            float y = left.Z * right.X - left.X * right.Z;
            float z = left.X * right.Y - left.Y * right.X;
            return new(x, y, z);
        }

        /// <summary>
        /// 判断两个矢量是否平行
        /// </summary>
        public static bool IsParallel(Vector3D left, Vector3D right)
        {
            return CrossProduct(left, right).LengthSquared == 0;
        }

        public static float Distance(Vector3D left, Vector3D right)
        {
            return (float)Math.Sqrt(DistanceSquare(left, right));
        }

        public static float DistanceSquare(Vector3D left, Vector3D right)
        {
            float x = left.X - right.X;
            float y = left.Y - right.Y;
            float z = left.Z - right.Z;
            return x * x + y * y + z * z;
        }

        /// <summary>
        /// 求两个矢量的夹角（0到π）
        /// </summary>
        public static double AngleAbs(Vector3D left, Vector3D right)
        {
            if (left.Length == 0 || right.Length == 0)
                return 0;
            double cosAngle = DotProduct(left, right) / (left.Length * right.Length);
            return Math.Acos(cosAngle);
        }

        public void Normalize()
        {
            if (Length == 0)
                return;
            float num = 1f / Length;
            X *= num;
            Y *= num;
            Z *= num;
        }

        public readonly Vector3D ToNormal()
        {
            if (Length == 0)
                return Zero;
            float num = 1f / Length;
            return new(X *  num, Y * num, Z * num);
        }

        public readonly bool Equals(Vector3D value)
        {
            return this == value;
        }

        public override readonly bool Equals(object obj)
        {
            return obj is Vector3D o && Equals(o);
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