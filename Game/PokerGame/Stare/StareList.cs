namespace PokerGame.Stare
{
    /// <summary>
    /// 特定的牌型
    /// </summary>
    public class StareList
    {
        /// <summary>牌型</summary>
        public StareListType type;
        /// <summary>顺子或炸弹的长度</summary>
        public int length;
        /// <summary>大小，顺子用最小的一张牌指代</summary>
        public int value;

        /// <summary>双王炸的大小</summary>
        public const double JokerBoomLength = 4.5;

        public StareList() { }

        /// <summary>
        /// 获取牌型对应的级别。
        /// </summary>
        /// <returns>普通牌型为1，炸弹为2</returns>
        public int GetLevel()
        {
            return type switch
            {
                StareListType.None => 0,
                StareListType.Single or StareListType.Pair or StareListType.Straight => 1,
                StareListType.Boom or StareListType.JokerBoom => 2,
                _ => 0,
            };
        }
    }

    /// <summary>
    /// 牌型
    /// </summary>
    public enum StareListType
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
