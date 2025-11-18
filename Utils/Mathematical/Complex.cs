using System;

namespace Utils.Mathematical
{
    /// <summary>
    /// 复数
    /// </summary>
    public struct Complex : IEquatable<Complex>
    {
        private double a;
        private double b;

        /// <summary>
        /// 实部
        /// </summary>
        public double Real
        {
            readonly get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }

        /// <summary>
        /// 虚部
        /// </summary>
        public double Imaginary
        {
            readonly get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }

        /// <summary>
        /// 模
        /// </summary>
        public readonly double Modulu => Math.Sqrt(ModuluSquared);

        /// <summary>
        /// 模的平方
        /// </summary>
        public readonly double ModuluSquared => a * a + b * b;

        /// <summary>
        /// 辐角
        /// </summary>
        public readonly double ArgumentAngle => Math.Atan2(b, a);

        public static Complex Zero => new(0, 0);

        public Complex()
        {
            a = 0;
            b = 0;
        }

        /// <param name="a">实部</param>
        /// <param name="b">虚部</param>
        public Complex(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public static Complex operator -(Complex me)
        {
            return new(-me.a, -me.b);
        }

        public static Complex operator -(Complex left, Complex right)
        {
            return new(left.a - right.a, left.a - right.b);
        }

        public static Complex operator +(Complex left, Complex right)
        {
            return new(left.a + right.a, left.a + right.b);
        }

        public static Complex operator *(Complex left, Complex right)
        {
            double a = left.a * right.a - left.b * right.b;
            double b = left.a * right.b + left.b * right.a;
            return new(a, b);
        }

        public static Complex operator *(Complex left, double right)
        {
            return new(left.a * right, left.b * right);
        }

        public static Complex operator *(double left, Complex right)
        {
            return new(left * right.a, left * right.b);
        }

        /// <exception cref="DivideByZeroException">除数不能为0</exception>
        public static Complex operator /(Complex left, Complex right)
        {
            if (right.Modulu == 0)
                throw new DivideByZeroException("除数不能为0");
            return left * right.ToConjugate() / right.ModuluSquared;
        }

        /// <exception cref="DivideByZeroException">除数不能为0</exception>
        public static Complex operator /(Complex left, double right)
        {
            if (right == 0)
                throw new DivideByZeroException("除数不能为0");
            return new(left.a / right, left.b / right);
        }

        /// <exception cref="DivideByZeroException">除数不能为0</exception>
        public static Complex operator /(double left, Complex right)
        {
            if (right.Modulu == 0)
                throw new DivideByZeroException("除数不能为0");
            return left * right.ToConjugate() / right.ModuluSquared;
        }

        public static bool operator ==(Complex left, Complex right)
        {
            return left.a == right.a && left.b == right.b;
        }

        public static bool operator !=(Complex left, Complex right)
        {
            return !(left == right);
        }

        /// <summary>
        /// 共轭复数
        /// </summary>
        public readonly Complex ToConjugate()
        {
            return new(a, -b);
        }

        public readonly bool Equals(Complex other)
        {
            return this == other;
        }

        public override readonly bool Equals(object obj)
        {
            return obj is Complex c && Equals(c);
        }

        public override readonly int GetHashCode()
        {
            return Tuple.Create(a, b).GetHashCode();
        }

        public override readonly string ToString()
        {
            if (b == 0)
                return a.ToString();
            else
                return $"{a} + {b}i";
        }
    }
}
