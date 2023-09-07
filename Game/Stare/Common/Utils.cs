using Stare.Common.Constant;

namespace Stare.Common
{
    public static class Utils
    {
        /// <summary>
        /// 计算相对位置
        /// </summary>
        public static RelativePosition CalculatePosition(int self, int another)
        {
            int relative = (another - self) % 4;
            return relative switch
            {
                0 => RelativePosition.Self,
                1 => RelativePosition.Next,
                2 => RelativePosition.Opposite,
                3 => RelativePosition.Previous,
                _ => RelativePosition.None,
            };
        }
    }
}
