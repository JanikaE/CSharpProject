namespace Stare.Common
{
    /// <summary>
    /// 特定的牌型
    /// </summary>
    public class PokerList
    {
        /// <summary>牌型</summary>
        public PokerListType type;
        /// <summary>顺子或炸弹的长度</summary>
        public int length;
        /// <summary>大小，顺子用最小的一张牌指代</summary>
        public int value;

        /// <summary>双王炸的大小</summary>
        public const double JokerBoomLength = 4.5;

        public PokerList() { }

        /// <summary>
        /// 获取牌型对应的级别。
        /// </summary>
        /// <returns>普通牌型为1，炸弹为2</returns>
        public int GetLevel()
        {
            return type switch
            {
                PokerListType.None => 0,
                PokerListType.Single or PokerListType.Pair or PokerListType.Straight => 1,
                PokerListType.Boom => 2,
                _ => 0,
            };
        }

        /// <summary>
        /// 获取炸弹对应的倍率
        /// </summary>
        /// <returns></returns>
        public int GetBoomRate()
        {
            if (type == PokerListType.Boom)
                return (int)Math.Pow(2, length - 2);
            else if (type == PokerListType.JokerBoom)
                return 4;
            else
                return 1;
        }
    }

    /// <summary>
    /// 牌型
    /// </summary>
    public enum PokerListType
    {
        None,

        ///<summary>单牌</summary>
        Single,

        ///<summary>对子</summary>
        Pair,

        ///<summary>顺子</summary>
        Straight,

        ///<summary>星炸</summary>
        Boom,
        /// <summary>双王炸</summary>
        JokerBoom
    }
}
