namespace Stare.Common.Constant
{
    public class RoomConfig
    {
        public int minGold;
        public int maxGold;
        /// <summary>底分</summary>
        public int baseGold;
        /// <summary>入场暗扣</summary>
        public int admissionFee;
        /// <summary>暗扣赢家</summary>
        public int secretCost;

        public RoomConfig(int minGold, int maxGold, int baseGold, int admissionFee, int secretCost)
        {
            this.minGold = minGold;
            this.maxGold = maxGold;
            this.baseGold = baseGold;
            this.admissionFee = admissionFee;
            this.secretCost = secretCost;
        }

        public static RoomConfig GetRoomConfig(RoomType type)
        {
            return type switch
            {
                RoomType.Junior => new(100000, 5000000, 1000, 0, 0),
                RoomType.High => new(500000, 20000000, 10000, 1000, 5),
                RoomType.Master => new(5000000, int.MaxValue, 20000, 5000, 5),
                RoomType.WeekendHigh => new(500000, 100000000, 10000, 1000, 5),
                RoomType.WeekendSupreme => new(10000000, int.MaxValue, 50000, 10000, 5),
                _ => throw new Exception("Invalid Room Type"),
            };
        }
    }

    public enum RoomType
    {
        Junior,
        High,
        Master,
        WeekendHigh,
        WeekendSupreme
    }
}
