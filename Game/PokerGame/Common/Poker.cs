namespace PokerGame.Common
{
    public class Poker
    {
        public string name;
        /// <summary>花色</summary>
        public PokerType type;
        /// <summary>点数</summary>
        public PokerValue value;

        /// <summary>万能牌原来的数字</summary>
        public PokerValue OriginValue
        {
            get
            {
                if (name == "SJoker") return PokerValue.SJoker;
                else if (name == "LJoker") return PokerValue.LJoker;

                if (!IsRogue())
                    return value;
                else
                {
                    string str = name.Split(',')[1];
                    return (PokerValue)Enum.Parse(typeof(PokerValue), str);
                }
            }
        }

        public Poker(PokerType type, PokerValue value)
        {
            this.type = type;
            this.value = value;
            if (value == PokerValue.SJoker || value == PokerValue.LJoker)
            {
                name = value.ToString();
            }
            else
            {
                name = type.ToString() + "," + value.ToString();
            }
        }

        public bool Equals(Poker poker)
        {
            return poker.name.Equals(name);
        }

        /// <summary>
        /// 最小的非万能牌的值
        /// </summary>
        internal const int startValue = 5;

        /// <summary>
        /// 判断万能牌
        /// </summary>
        public bool IsRogue()
        {
            if (OriginValue == PokerValue.Three ||
                OriginValue == PokerValue.Four ||
                OriginValue == PokerValue.SJoker ||
                OriginValue == PokerValue.LJoker)
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// 花色
    /// </summary>
    public enum PokerType
    {
        None = 0,
        /// <summary>方块</summary>
        Square = 1,
        /// <summary>梅花</summary>
        Club = 2,
        /// <summary>红桃</summary>
        Heart = 3,
        /// <summary>黑桃</summary>
        Spade = 4
    }

    /// <summary>
    /// 点数
    /// </summary>
    public enum PokerValue
    {
        None = 0,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        One = 14,
        Two = 15,
        SJoker = 16,
        LJoker = 17
    }
}
