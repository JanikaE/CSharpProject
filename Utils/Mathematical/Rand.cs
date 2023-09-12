using System;

namespace Utils.Mathematical
{
    public static class Rand
    {
        public static readonly Random random = new();

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
            lock (random)
            {
                double u = -2 * Math.Log(random.NextDouble());
                double v = 2 * Math.PI * random.NextDouble();
                return Math.Sqrt(u) * Math.Cos(v);
            }
        }

        /// <summary>
        /// 指数分布
        /// </summary>
        public static double Exponential(double lambda)
        {
            if (lambda == 0)
            {
                throw new InvalidOperationException("参数不能为0");
            }
            lock (random)
            {
                double p = random.NextDouble(); ;
                return -1 / lambda * Math.Log(p, Math.E);
            }
        }
    }
}
