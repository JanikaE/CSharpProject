namespace Stare.Common.Dto
{
    /// <summary>对局流水</summary>
    public class FlowingDto
    {
        /// <summary>金币来源/去向</summary>
        public int position;
        /// <summary>炸弹轮数</summary>
        public int round;
        /// <summary>炸弹大小（王炸为2）</summary>
        public int level;
        /// <summary>金币数量</summary>
        public int goldNum;

        public FlowingDto(int position, int round, int level, int goldNum) 
        {
            this.position = position;
            this.round = round;                
            this.level = level;
            this.goldNum = goldNum;
        }
    }
}
