using System;
using Utils.Mathematical;

namespace Utils.Tool
{
    /// <summary>
    /// 数学工具类
    /// </summary>
    public static class RandomTool
    {
        public static Random Random { get; set; } = Random.Shared;

        /// <summary>
        /// 正态分布
        /// </summary>
        /// <param name="mu">均值</param>
        /// <param name="sigma">标准差</param>
        public static double Gaussian(double mu, double sigma)
        {
            return StdGaussian() * sigma + mu;
        }

        /// <summary>
        /// 标准正态分布
        /// </summary>
        public static double StdGaussian()
        {
            double u = -2 * Math.Log(Random.NextDouble());
            double v = 2 * Math.PI * Random.NextDouble();
            return Math.Sqrt(u) * Math.Cos(v);
        }

        /// <summary>
        /// 指数分布
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">参数不能为0</exception>
        public static double Exponential(double lambda)
        {
            if (lambda == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lambda), "参数不能为0");
            }
            double p = Random.NextDouble(); ;
            return -1 / lambda * Math.Log(p, Math.E);
        }

        public static float Single(int maxValue)
        {
            return Random.NextSingle() * maxValue;
        }

        public static float Single(int minValue, int maxValue)
        {
            return Random.NextSingle() * (maxValue - minValue) + minValue;
        }

        public static double Double(double maxValue)
        {
            return Random.NextDouble() * maxValue;
        }

        public static double Double(double minValue, double maxValue)
        {
            return Random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
