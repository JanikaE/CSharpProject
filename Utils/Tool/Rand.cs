using System;

namespace Utils.Tool
{
    public static class Rand
    {
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
            double u = -2 * Math.Log(Random.Shared.NextDouble());
            double v = 2 * Math.PI * Random.Shared.NextDouble();
            return Math.Sqrt(u) * Math.Cos(v);
        }

        /// <summary>
        /// 指数分布
        /// </summary>
        public static double Exponential(double lambda)
        {
            if (lambda == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lambda), "参数不能为0");
            }
            double p = Random.Shared.NextDouble(); ;
            return -1 / lambda * Math.Log(p, Math.E);
        }
    }
}
