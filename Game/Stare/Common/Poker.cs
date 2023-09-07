namespace Stare.Common
{
    [Serializable]
    public class Poker
    {
        /// <summary>花色</summary>
        public PokerType type;
        /// <summary>点数，以及万能牌所代替的数字</summary>
        public PokerValue value;
        /// <summary>万能牌原来的数字</summary>
        public PokerValue originValue;
        // tip: 大小比较一律用value，originValue只用于判断万能牌的状态


        public Poker(PokerType type, PokerValue value)
        {
            this.type = type;
            this.value = value;
            originValue = value;
        }

        public override string ToString()
        {
            return type.ToString() + value.ToString();
        }

        /// <summary>
        /// 判断万能牌
        /// </summary>
        public bool IsRogue()
        {
            if (originValue == PokerValue.Three ||
                originValue == PokerValue.Four ||
                originValue == PokerValue.SJoker ||
                originValue == PokerValue.LJoker) 
                return true;
            else 
                return false;
        }
        
        /// <summary>
        /// 万能牌是否有指代
        /// </summary>
        /// <returns>不是万能牌返回null，还没指代返回false，已经有指代返回true</returns>
        public bool? HasChange()
        {
            if (!IsRogue())
                return null;
            else
            {
                return originValue != value;
            }
            
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
