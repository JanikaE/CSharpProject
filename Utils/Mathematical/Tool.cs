namespace Utils.Mathematical
{
    public static class Tool
    {
        /// <summary>
        /// 简单直线运动预判
        /// </summary>
        /// <param name="position">出发位置</param>
        /// <param name="speed">出发速率</param>
        /// <param name="targetPosition">目标位置</param>
        /// <param name="targetVelocity">目标速度</param>
        /// <param name="cnt">迭代次数</param>
        /// <returns>抵达目标应当运动的方向</returns>
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
    }
}
