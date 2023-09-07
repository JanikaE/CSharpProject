using Stare.Common;
using Stare.Common.Constant;
using Stare.Common.Dto;

namespace Stare.Logic
{
    public class StareRoom
    {
        public int RoomID { private set; get; }
        public RoomType type;

        public int minGold;
        public int maxGold;
        /// <summary>底分</summary>
        public int baseGold;
        /// <summary>入场暗扣</summary>
        public int admissionFee;
        /// <summary>暗扣赢家(比例)</summary>
        public int secretCost;
        /// <summary>封顶倍数</summary>
        public int topRate;

        public StarePlayer[] players = new StarePlayer[playerNum];
        public RoomState state = RoomState.None;
        /// <summary>牌库</summary>
        public List<Poker> pokerList = new();
        /// <summary>记牌器</summary>
        public List<Poker> pokerCalculater = new();

        public const int playerNum = 4;

        /// <summary>当前出牌玩家</summary>
        private int outIndex;
        /// <summary>上一次出的牌</summary>
        private List<Poker>? lastPokers;
        /// <summary>上一次出牌的玩家</summary>
        private int lastIndex;

        /// <summary>因炸弹累计的奖池</summary>
        private int boomPool;
        /// <summary>炸弹轮数</summary>
        private int boomRound;
        /// <summary>最后一次的炸弹大小</summary>
        private int boomLevel;
        /// <summary>炸弹的底分</summary>
        private int boomBase;

        /// <summary>每一轮是否上楼</summary>
        private bool isUpstair;
        /// <summary>当局上楼次数</summary>
        private int upstairTimes;
        /// <summary>当前是否算绝杀</summary>
        private bool isLore;
        /// <summary>当前回合第一个出牌的玩家（用于判断是否算绝杀）</summary>
        private int startIndex;
        /// <summary>绝杀张数</summary>
        private int loreCnt;

        public StareRoom(int roomID, RoomType type)
        {
            RoomID = roomID;
            this.type = type;

            RoomConfig config = RoomConfig.GetRoomConfig(type);
            minGold = config.minGold;
            maxGold = config.maxGold;
            baseGold = config.baseGold;
            admissionFee = config.admissionFee;
            secretCost = config.secretCost;

            topRate = int.MaxValue;  // 暂定上不封顶
        }

        public void Start()
        {
            state = RoomState.Playing;
            CostAdmissionFee();
            CreatePokerQueue();
            Shuffle();
            for (int i = 0; i < playerNum; i++)
            {
                players[i].Init();
            }
            int index = WhosFirst();
            outIndex = index;
            Deal(5);
            DealOne(players[index]);
        }

        /// <summary>
        /// 入场暗扣
        /// </summary>
        private void CostAdmissionFee()
        {
            foreach (StarePlayer player in players)
            {
                player.gold -= admissionFee;
            }
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="index">需要提示的玩家</param>
        public List<List<Poker>> GetSuggest(int index)
        {
            return StareUtils.SuggestList(players[index].handPokerList, lastPokers);
        }

        /// <summary>
        /// 尝试出牌
        /// </summary>
        /// <returns>出牌是否成功</returns>
        public bool TryOutPokers(List<Poker> outPokers)
        {
            if (lastPokers != null)
            {
                return StareUtils.ComparePokerList(outPokers, lastPokers);
            }
            else
            {
                PokerList pokerList = StareUtils.GetPokerListType(outPokers);
                return pokerList.type != PokerListType.None;
            }
        }

        /// <summary>
        /// 出牌（之后）
        /// </summary>
        /// <param name="outPokers">出的牌，不出则传入null</param>
        /// <param name="outPlayer">出牌的玩家</param>
        public void OutPokers(List<Poker>? outPokers, int outPlayer)
        {
            // 出牌的情况
            if (outPokers != null)
            {
                players[outPlayer].OutPoker(outPokers);
                pokerCalculater.RemoveAll(n => outPokers.Contains(n));
                UpdatePokerCalculation();
                // 新的一回合的情况
                if (lastPokers == null)
                {
                    isUpstair = true;
                    isLore = true;
                    startIndex = outPlayer;
                }
                // 压别家牌的情况
                else
                {
                    isUpstair = false;
                    // 当前回合走完一轮，不算绝杀
                    if (outPlayer == startIndex)
                    {
                        isLore = false;
                    }
                }
                lastPokers = outPokers;
                lastIndex = outPlayer;
                players[outPlayer].outTimes++;

                PokerList pokerList = StareUtils.GetPokerListType(outPokers);
                // 出的是炸弹
                if (pokerList.type == PokerListType.Boom || pokerList.type == PokerListType.JokerBoom)
                {
                    int rate = pokerList.GetBoomRate();
                    boomRound++;
                    boomLevel = pokerList.type == PokerListType.JokerBoom ? 2 : pokerList.length;
                    SettleBoom(outPlayer, rate);
                }

                // 牌出完了
                if (players[outPlayer].handPokerList.Count == 0)
                {
                    if (isLore)
                    {
                        loreCnt = outPokers.Count;
                    }
                    int winGold = 0;
                    // 结算输家
                    for (int i = 0; i < playerNum; i++)
                    {
                        if (i == outPlayer)
                            continue;
                        winGold += SettleLoser(i);
                    }
                    // 结算赢家（包含暗扣）
                    players[outPlayer].gold += winGold * (100 - secretCost) / 100;
                    state = RoomState.End;
                }
            }
            // 不出的情况
            else
            {

            }

            Turn();
            // 无人出牌，准备新的一回合
            if (lastIndex == outIndex)
            {
                lastPokers = null;
                if (isUpstair)
                {
                    upstairTimes++;
                }
                if (boomPool > 0)
                {
                    SettleBoomPool(lastIndex);
                }

                // 发牌
                Deal(1);
            }
        }

        public void EnterPlayer(StarePlayer player, int index)
        {
            players[index] = player;
        }

        /// <summary>
        /// 转换出牌玩家
        /// </summary>
        private void Turn()
        {
            while (true)
            {
                outIndex = (outIndex + 1) % playerNum;
                if (!players[outIndex].isFail)
                    break;
            }
        }

        /// <summary>
        /// 发牌（所有人，从出牌者开始发起，发完就不发了）
        /// </summary>
        /// <param name="num">开局发5张，之后发1张</param>
        private void Deal(int num)
        {
            for (int i = 0; i < num; i++)
            {
                //foreach (StarePlayer player in players)
                //{
                //    if (pokerQueue.Count == 0)
                //        return;
                //    player.AddPoker(pokerQueue.Dequeue());
                //}
                for (int j = 0; j < playerNum; j++)
                {
                    if (pokerList.Count == 0)
                        return;
                    int index = (j + outIndex) % playerNum;
                    if (players[index].isFail)
                        continue;
                    DealOne(players[index]);
                }
            }
        }

        /// <summary>
        /// 发牌（单个人）
        /// </summary>
        private void DealOne(StarePlayer player) 
        {
            player.AddPoker(pokerList[0]);
            pokerList.RemoveAt(0);
        }

        /// <summary>
        /// 确定先出牌的玩家
        /// </summary>
        private int WhosFirst()
        {
            int index;
            //// 持有最小的牌的玩家先出
            //List<PokerStare> pokers = new();
            //foreach (StarePlayer player in players)
            //{
            //    PokerStare poker = StareUtils.GetSmallestPoker(player.pokerList,out index);
            //    pokers.Add(poker); 
            //}
            //StareUtils.GetSmallestPoker(pokers,out index);

            // 随机指定首出的玩家
            index = new Random().Next(playerNum);
            for (int i = 0; i < playerNum; i++)
            {
                players[i].isFirst = i == index;
            }
            return index;
        }

        /// <summary>
        /// 创造初始的一副牌
        /// </summary>
        private void CreatePokerQueue()
        {
            pokerList.Clear();
            foreach (PokerValue value in Enum.GetValues(typeof(PokerValue)))
            {
                if (value == PokerValue.None)
                    continue;
                else if (value == PokerValue.SJoker || value == PokerValue.LJoker)
                {
                    PokerType type = PokerType.None;
                    pokerList.Add(new(type, value));
                }
                else
                {
                    foreach (PokerType type in Enum.GetValues(typeof(PokerType)))
                    {
                        if (type == PokerType.None)
                            continue;
                        pokerList.Add(new(type, value));
                    }
                }
            }
            pokerCalculater = pokerList;
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        private void Shuffle()
        {
            List<Poker> newList = new();
            Random rand = new();
            foreach (Poker poker in pokerList)
            {
                int index = rand.Next(0, newList.Count + 1);
                newList.Insert(index, poker);
            }
            pokerList.Clear();

            foreach (Poker poker in newList)
            {
                pokerList.Add(poker);
            }
        }

        /// <summary>
        /// 结算炸弹
        /// </summary>
        /// <param name="index">出炸弹的玩家</param>
        /// <param name="rate">炸弹的倍率</param>
        private void SettleBoom(int index, int rate)
        {
            int Gold = boomBase * rate;
            for (int i = 0; i < playerNum; i++)
            {
                if (i == index || players[i].isFail)
                    continue;
                players[i].gold -= Gold;
                players[i].flowing -= Gold;
                boomPool += Gold;
                // 玩家中途破产的情况
                if (players[i].gold < 0)
                {
                    players[i].gold = 0;
                    players[i].isFail = true;
                }
            }
            boomBase = Gold;
        }

        /// <summary>
        /// 结算炸弹奖池
        /// </summary>
        /// <param name="index">获得奖池的玩家</param>
        private void SettleBoomPool(int index)
        {
            players[index].gold += boomPool;
            players[index].flowing += boomPool;
            // 记录流水
            foreach (StarePlayer player in players)
            {
                FlowingDto flowing = new(index, boomRound, boomLevel, player.flowing);
                player.flowings.Add(flowing);
                player.flowing = 0;
            }
            boomBase = baseGold;
            boomPool = 0;
            boomRound = 0;
            boomLevel = 0;
        }

        /// <summary>
        /// 结算输家
        /// </summary>
        /// <returns>输的金币数量</returns>
        private int SettleLoser(int index)
        {
            StarePlayer player = players[index];

            int upstairRate = (int)Math.Pow(2, upstairTimes);
            int holdCardRate = player.handPokerList.Count;
            int holdBoomRate = StareUtils.GetMaxBoomRate(player.handPokerList);
            int closeRate = (player.isFirst && player.outTimes == 1) || (!player.isFirst && player.outTimes == 0) ? 4 : 1;
            int loreRate = isLore && type == RoomType.Master ? loreCnt * 2 : 1;

            int rate = upstairRate * holdCardRate * holdBoomRate * closeRate * loreRate;
            if (rate > topRate)
            {
                rate = topRate;
            }

            // 不论输家是否破产，赢家该赢多少赢多少
            int gold = rate * baseGold;
            player.gold -= gold;
            if (player.gold < 0)
                player.gold = 0;
            return gold;
        }

        /// <summary>
        /// 更新玩家的记牌器。用房间的记牌器减去玩家的手牌。
        /// </summary>
        private void UpdatePokerCalculation()
        {
            for (int i = 0; i < playerNum; i++)
            {
                if (players[i].isFail)
                    continue;
                Dictionary<int, int> result = new();
                List<Poker> pokers = pokerCalculater;
                pokers.RemoveAll(n => players[i].handPokerList.Contains(n));
                for (int j = 3; j <= 17; j++)
                {
                    int num = StareUtils.GetSingleCnt(pokers, j);
                    result.Add(j, num);
                }
                players[i].pokerCalculation = result;
            }
        }
    }

    public enum RoomState
    {
        None,
        Matching,
        Playing,
        End
    }
}
