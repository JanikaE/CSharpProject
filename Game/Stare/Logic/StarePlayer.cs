using Stare.Common;
using Stare.Common.Dto;

namespace Stare.Logic
{
    public class StarePlayer
    {
        public int id;
        public string name;
        public int gold;

        /// <summary>手牌</summary>
        public List<Poker> handPokerList = new();
        /// <summary>出牌次数</summary>
        public int outTimes;
        /// <summary>是否首出</summary>
        public bool isFirst;
        /// <summary>是否中途破产</summary>
        public bool isFail;
        /// <summary>出牌记录</summary>
        public List<List<Poker>> outPokerList = new();
        /// <summary>记牌器</summary>
        public Dictionary<int, int> pokerCalculation = new();
        /// <summary>对局流水记录</summary>
        public List<FlowingDto> flowings = new();
        /// <summary>单次流水暂存</summary>
        public int flowing;

        public void Init()
        {
            handPokerList.Clear();
            outPokerList.Clear();
            pokerCalculation.Clear();
            flowings.Clear();
            flowing = 0;
            outTimes = 0;
            isFirst = false;
            isFail = false;
        }

        public void AddPoker(Poker poker)
        {
            handPokerList.Add(poker);
        }

        /// <summary>出牌</summary>
        public void OutPoker(List<Poker> pokers)
        {
            // 移除手牌
            handPokerList.RemoveAll(p => pokers.Contains(p));
            // 在出牌记录中加上
            outPokerList.Add(pokers);
        }
    }
}
