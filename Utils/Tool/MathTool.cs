using System;
using Utils.Mathematical;

namespace Utils.Tool
{
    /// <summary>
    /// 数学工具类
    /// </summary>
    public static class MathTool
    {
        /// <summary>
        /// 简单直线运动预判
        /// </summary>
        /// <param name="position">出发位置</param>
        /// <param name="speed">出发速率</param>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="targetVelocity">目标速度</param>
        /// <param name="cnt">迭代次数</param>
        /// <returns>为了抵达目标应当运动的方向</returns>
        public static Vector2D PreJudgeDirection(Vector2D position, float speed, Vector2D targetPosition, Vector2D targetVelocity, int cnt)
        {
            Vector2D tar = targetPosition;
            for (int i = 0; i < cnt; i++)
            {
                float time = (tar - position).Length / speed;
                Vector2D tar_new = targetPosition + targetVelocity * time;
                tar = tar_new;
            }
            return (tar - position).ToNormal();
        }

        #region 随机分布

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
        /// <exception cref="ArgumentOutOfRangeException">参数不能为0</exception>
        public static double Exponential(double lambda)
        {
            if (lambda == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lambda), "参数不能为0");
            }
            double p = Random.Shared.NextDouble(); ;
            return -1 / lambda * Math.Log(p, Math.E);
        }

        #endregion
    }
}
