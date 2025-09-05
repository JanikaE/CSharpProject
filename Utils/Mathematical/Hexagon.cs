using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Mathematical
{
    public struct Hexagon : IEquatable<Hexagon>
    {
        /// <summary>
        /// 轴向坐标 q
        /// </summary>
        public int Q;
        /// <summary>
        /// 轴向坐标 r
        /// </summary>
        public int R;
        /// <summary>
        /// 推导坐标 s（满足 q + r + s = 0）
        /// </summary>
        public readonly int S => -Q - R;

        public Hexagon(int q, int r)
        {
            Q = q;
            R = r;
        }

        public Hexagon(Vector2D vector2D)
        {
            Q = (int)vector2D.X;
            R = (int)vector2D.Y;
        }

        public readonly double DistanceTo(Hexagon other)
        {
            return (Math.Abs(Q - other.Q) +
                    Math.Abs(R - other.R) +
                    Math.Abs(S - other.S)) / 2.0;
        }

        public readonly Hexagon GetNeighbor(RelativePosition_6 direction)
        {
            var dir = RelativePosition.ToHexagon(direction);
            return this + dir;
        }

        /// <summary>
        /// 转换为像素坐标
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public readonly Vector2D ToPixelLocation(float size)
        {
            float x = (float)(size * (3.0 / 2.0 * Q));
            float y = (float)(size * (Math.Sqrt(3) / 2.0 * Q + Math.Sqrt(3) * R));
            return new Vector2D(x, y);
        }

        public static Hexagon operator +(Hexagon a, Hexagon b)
        {
            return new Hexagon(a.Q + b.Q, a.R + b.R);
        }

        public static bool operator ==(Hexagon a, Hexagon b)
        {
            return a.Q == b.Q && a.R == b.R;
        }

        public static bool operator !=(Hexagon a, Hexagon b)
        {
            return !(a == b);
        }

        public readonly bool Equals(Hexagon other)
        {
            return this == other;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Hexagon other && Equals(other);
        }

        public override readonly int GetHashCode()
        {
            return Tuple.Create(Q, R).GetHashCode();
        }
    }
}
