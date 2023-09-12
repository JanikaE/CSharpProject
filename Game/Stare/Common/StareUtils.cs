namespace Stare.Common
{
    public static class StareUtils
    {
        /// <summary>
        /// 获取一组牌的牌型（考虑万能牌）
        /// </summary>
        public static PokerList GetPokerListType(List<Poker> pokers)
        {
            PokerList result = new();
            if (pokers == null)
            {
                result.type = PokerListType.None;
                return result;
            }
            if (HasRogue(pokers))
            {
                if (!ChangeRogue(pokers))
                {
                    result.type = PokerListType.None;
                    ResetRogue(pokers);
                    return result;
                }
            }
            return GetPokerListTypeWithoutRogue(pokers);
        }

        /// <summary>
        /// 获取一组牌的牌型（万能牌已变换）
        /// </summary>
        private static PokerList GetPokerListTypeWithoutRogue(List<Poker> pokers)
        {
            PokerList result = new();
            if (pokers == null)
            {
                result.type = PokerListType.None;
                return result;
            }
            int length = pokers.Count;
            SortPokerSubList(pokers);
            switch (length)
            {
                case 0:
                    result.type = PokerListType.None;
                    break;

                case 1:
                    result.type = PokerListType.Single;
                    result.value = (int)pokers[0].value;
                    break;

                case 2:
                    if (IsListSame(pokers))
                    {
                        result.type = PokerListType.Pair;
                        result.value = (int)(pokers[0].value);
                    }
                    else if (IsJokerBoom(pokers))
                    {
                        result.type = PokerListType.JokerBoom;
                    }
                    else
                    {
                        result.type = PokerListType.None;
                    }
                    break;

                default:
                    if (IsListSame(pokers))
                    {
                        result.type = PokerListType.Boom;
                        result.value = (int)pokers[0].value;
                        result.length = length;
                    }
                    else if (IsListStraight(pokers))
                    {
                        result.type = PokerListType.Straight;
                        result.value = (int)pokers[0].value;
                        result.length = length;
                    }
                    else
                    {
                        result.type = PokerListType.None;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// 比较两组牌的大小（确定了牌型之后的牌组）
        /// </summary>
        /// <returns>p1压过p2返回true</returns>
        private static bool ComparePokerList(PokerList p1, PokerList p2)
        {
            if (p1.type == PokerListType.None || p2.type == PokerListType.None)
                return false;
            if (p1.GetLevel() == 1 && p2.GetLevel() == 1)
            {
                if (p1.type != p2.type)
                    return false;
                else
                {
                    switch (p1.type)
                    {
                        case PokerListType.Single:
                        case PokerListType.Pair:
                            if (p1.value == p2.value + 1 ||
                                (p1.value == (int)PokerValue.Two &&
                                 p2.value != (int)PokerValue.Two))
                                return true;
                            else
                                return false;

                        case PokerListType.Straight:
                            if (p1.length != p2.length)
                                return false;
                            else if (p1.value == p2.value + 1)
                                return true;
                            else
                                return false;

                        default:
                            return false;
                    }
                }
            }
            else if (p1.GetLevel() == 2 && p2.GetLevel() == 2)
            {
                double len1 = p1.type == PokerListType.JokerBoom ? PokerList.JokerBoomLength : p1.length;
                double len2 = p2.type == PokerListType.JokerBoom ? PokerList.JokerBoomLength : p2.length;
                if (len1 > len2)
                    return true;
                else if (len1 == len2)
                    return p1.value > p2.value;
                else
                    return false;
            }
            else if (p1.GetLevel() > p2.GetLevel())
                return true;
            else
                return false;
        }

        /// <summary>
        /// 比较两组牌的大小（原始牌组）
        /// </summary>
        /// <returns>p1压过p2返回true</returns>
        public static bool ComparePokerList(List<Poker> myList, List<Poker> lastList)
        {
            if (myList == null)
                return false;
            if (HasRogue(myList))
            {
                if (!ChangeRogue(myList, GetPokerListType(lastList)))
                {
                    ResetRogue(myList);
                    return false;
                }
            }
            return ComparePokerList(GetPokerListTypeWithoutRogue(myList), GetPokerListType(lastList));
        }

        /// <summary>
        /// 对一组牌进行排序。万能牌在前，其余牌在后
        /// </summary>
        public static void SortPokerList(List<Poker> pokers, bool isAsc = true)
        {
            pokers.Sort(delegate (Poker p1, Poker p2)
            {
                if (isAsc)
                {
                    int r1 = p1.IsRogue() & p1.value == p1.OriginValue ? -1000 : 0;
                    int r2 = p2.IsRogue() & p2.value == p2.OriginValue ? -1000 : 0;
                    return (r1 + (int)p1.value * 10 + (int)p1.type).CompareTo(r2 + (int)p2.value * 10 + (int)p2.type);
                }
                else
                {
                    int r1 = p1.IsRogue() & p1.value == p1.OriginValue ? 1000 : 0;
                    int r2 = p2.IsRogue() & p2.value == p2.OriginValue ? 1000 : 0;
                    return (r2 + (int)p2.value * 10 + (int)p2.type).CompareTo(r1 + (int)p1.value * 10 + (int)p1.type);
                }
            });
        }

        /// <summary>
        /// 对特定的一组牌进行排序
        /// </summary>
        private static void SortPokerSubList(List<Poker> pokers, bool isAsc = true)
        {
            if (isAsc)
            {
                pokers.Sort(delegate (Poker p1, Poker p2)
                {
                    if (p1.value != p2.value)
                    {
                        return (int)p1.value < (int)p2.value ? -1 : 1;
                    }
                    else
                    {
                        return (int)p1.type < (int)p2.type ? -1 : 1;
                    }
                });
            }
            else
            {
                pokers.Sort(delegate (Poker p1, Poker p2)
                {
                    if (p1.value != p2.value)
                    {
                        return (int)p1.value > (int)p2.value ? -1 : 1;
                    }
                    else
                    {
                        return (int)p1.type > (int)p2.type ? -1 : 1;
                    }
                });
            }
        }

        /// <summary>
        /// 变换万能牌
        /// </summary>
        /// <param name="tar">上家的出牌牌型，主要用于顺子判定</param>
        /// <returns>变换后没有相应牌型返回false</returns>
        private static bool ChangeRogue(List<Poker> pokers, PokerList tar = null)
        {
            ResetRogue(pokers);
            int length = pokers.Count;
            int cnt = GetRogueCnt(pokers);
            // 如果是王炸则不用变换
            if (IsJokerBoom(pokers))
            {
                return true;
            }
            if (length == cnt || length == 1)
            {
                return false;
            }

            List<Poker> newPokers = new();
            foreach (Poker poker in pokers)
            {
                if (!poker.IsRogue())
                {
                    newPokers.Add(poker);
                }
            }
            // 对子或炸弹
            if (IsListSame(newPokers))
            {
                PokerValue value = newPokers[0].value;
                foreach (Poker poker in pokers)
                {
                    if (poker.IsRogue())
                    {
                        poker.value = value;
                    }
                }
                return true;
            }
            // 顺子
            else
            {
                if (GetSingleIndex(newPokers, PokerValue.Two).Count > 0)
                {
                    return false;
                }
                SortPokerList(newPokers);
                if (tar != null)
                {
                    if (length != tar.length)
                    {
                        return false;
                    }
                    else
                    {
                        return ToListStraight(pokers, tar.value + 1);
                    }
                }
                else
                {
                    for (int i = 14; i >= Poker.startValue; i--)
                    {
                        if (ToListStraight(pokers, i))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 重置万能牌
        /// </summary>
        public static void ResetRogue(List<Poker> pokers)
        {
            foreach (Poker poker in pokers)
            {
                if (poker.IsRogue())
                {
                    poker.value = poker.OriginValue;
                }
            }
        }

        /// <summary>
        /// 获取一组牌中万能牌的个数
        /// </summary>
        public static int GetRogueCnt(List<Poker> pokers)
        {
            int cnt = 0;
            foreach (Poker poker in pokers)
            {
                if (poker.IsRogue())
                {
                    cnt++;
                }
            }
            return cnt;
        }

        /// <summary>
        /// 一组牌中是否含有万能牌
        /// </summary>
        public static bool HasRogue(List<Poker> pokers)
        {
            int cnt = GetRogueCnt(pokers);
            return cnt > 0 && cnt < pokers.Count;
        }

        /// <summary>
        /// 获取所有万能牌在牌组中的下标
        /// </summary>
        private static List<int> GetRogueIndex(List<Poker> pokers)
        {
            List<int> index = new();
            for (int i = 0; i < pokers.Count; i++)
            {
                if (pokers[i].IsRogue())
                {
                    index.Add(i);
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// 获取牌组中所有的万能牌
        /// </summary>
        private static List<Poker> GetRogue(List<Poker> pokers)
        {
            List<Poker> result = new();
            foreach (Poker poker in pokers)
            {
                if (poker.IsRogue())
                {
                    result.Add(poker);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取某张特定的牌在牌组中的数量
        /// </summary>
        public static int GetSingleCnt(List<Poker> pokers, int target)
        {
            int Cnt = 0;
            for (int i = 0; i < pokers.Count; i++)
            {
                if ((int)pokers[i].value == target)
                {
                    Cnt++;
                }
            }
            return Cnt;
        }

        /// <summary>
        /// 获取某张特定的牌在牌组中的下标
        /// </summary>
        private static List<int> GetSingleIndex(List<Poker> pokers, PokerValue target)
        {
            List<int> index = new();
            for (int i = 0; i < pokers.Count; i++)
            {
                if (pokers[i].value == target)
                {
                    index.Add(i);
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// 获取某张特定的牌在牌组中的下标
        /// </summary>
        private static List<int> GetSingleIndex(List<Poker> pokers, int target)
        {
            List<int> index = new();
            for (int i = 0; i < pokers.Count; i++)
            {
                if ((int)pokers[i].value == target)
                {
                    index.Add(i);
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// 获取牌组中所有特定的牌
        /// </summary>
        private static List<Poker> GetTargetPoker(List<Poker> pokers, int target)
        {
            List<Poker> result = new();
            foreach (Poker poker in pokers)
            {
                if ((int)poker.value == target)
                {
                    result.Add(poker);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取牌组中特定的对子（考虑万能牌），没有则返回null
        /// </summary>
        private static List<Poker> GetPair(List<Poker> pokers, int target)
        {
            ResetRogue(pokers);
            List<Poker> result = new();
            foreach (Poker poker in pokers)
            {
                if ((int)poker.value == target)
                {
                    result.Add(poker);
                }
                if (result.Count == 2)
                {
                    break;
                }
            }
            if (result.Count == 0)
            {
                result = null;
            }
            else if (result.Count == 1)
            {
                List<int> index = GetRogueIndex(pokers);
                if (index.Count == 0)
                {
                    result = null;
                }
                else
                {
                    result.Add(pokers[index[0]]);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取牌组中特定的顺子（考虑万能牌），没有则返回null
        /// </summary>
        /// <param name="target">顺子中最小的点数</param>
        private static List<Poker> GetStraight(List<Poker> pokers, int target, int length)
        {
            SortPokerList(pokers);
            ResetRogue(pokers);
            int rogueCnt = GetRogueCnt(pokers);
            List<Poker> result = new();
            if (target + length - 1 > 14)
                return null;
            for (int i = target; i < target + length; i++)
            {
                List<int> index = GetSingleIndex(pokers, i);
                if (index.Count == 0)
                {
                    rogueCnt--;
                    if (rogueCnt < 0)
                    {
                        return null;
                    }
                    else
                    {
                        result.Add(pokers[rogueCnt]);
                    }
                }
                else
                {
                    result.Add(pokers[index[0]]);
                }
            }
            if (GetRogueCnt(result) >= result.Count - 1)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 获取牌组中较长的顺子
        /// </summary>
        private static List<List<Poker>> GetStraight(List<Poker> pokers, bool hasRogue)
        {
            List<List<Poker>> results = new();
            SortPokerList(pokers);
            if (hasRogue)
            {
                foreach (Poker poker in pokers)
                {
                    List<Poker> result;
                    if (poker.IsRogue())
                        continue;
                    if (poker.value == PokerValue.Two)
                        break;

                    for (int i = pokers.Count; i >= 3; i--)
                    {
                        List<Poker> newPokers = new(pokers);
                        result = GetStraight(newPokers, (int)poker.value, i);
                        if (result != null)
                        {
                            results.Add(result);
                        }
                    }
                }
            }
            else
            {
                List<Poker> result = new();
                foreach (Poker poker in pokers)
                {
                    if (poker.IsRogue())
                        continue;
                    if (poker.value == PokerValue.Two)
                        break;

                    if (result.Count == 0 || result[^1].value + 1 == poker.value)
                        result.Add(poker);

                    if (result[^1].value == poker.value)
                        continue;

                    if (result[^1].value + 1 < poker.value)
                    {
                        if (result.Count >= 3)
                            results.Add(result);
                        result = new List<Poker>() { poker };
                    }
                }
                if (result.Count >= 3)
                    results.Add(result);
            }
            return results;
        }

        /// <summary>
        /// 获取牌组中的最大炸弹的倍率，没有则返回1
        /// </summary>
        public static int GetMaxBoomRate(List<Poker> pokers)
        {
            SortPokerList(pokers);
            ResetRogue(pokers);
            int rogueCnt = GetRogueCnt(pokers);
            int maxCnt = GetSingleCnt(pokers, 5);
            for (int i = Poker.startValue; i < 16; i++)
            {
                if (GetSingleCnt(pokers, i) > maxCnt)
                    maxCnt = GetSingleCnt(pokers, i);
            }
            int BoomLength = rogueCnt + maxCnt;
            if (BoomLength < 3)
            {
                return 1;
            }
            else
            {
                return (int)Math.Pow(2, BoomLength - 2);
            }
        }

        /// <summary>
        /// 获取牌组中所有能压过目标的炸弹，没有则返回null
        /// </summary>
        /// <param name="target">目标，如果不是炸弹就传入null</param>
        private static List<List<Poker>> GetAllBoom(List<Poker> pokers, PokerList target = null)
        {
            List<List<Poker>> result = new();
            ResetRogue(pokers);
            // 先把除了王炸的所有炸弹找到
            List<Poker> rogueList = GetRogue(pokers);
            int rogueCnt = GetRogueCnt(pokers);
            for (int i = Poker.startValue; i < 16; i++)
            {
                if (GetSingleCnt(pokers, i) + rogueCnt >= 3 && GetSingleCnt(pokers, i) > 0)
                {
                    List<Poker> p = GetTargetPoker(pokers, i);
                    p.AddRange(rogueList);
                    if (target != null)
                    {
                        if (ComparePokerList(GetPokerListType(p), target))
                        {
                            result.Add(p);
                        }
                    }
                    else
                    {
                        result.Add(p);
                    }
                }
            }
            // 王炸
            if (target == null || target.length <= 4)
            {
                List<Poker> jokerBoom = GetJokerBoom(pokers);
                if (jokerBoom != null)
                {
                    result.Add(jokerBoom);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取牌组中的王炸，没有则返回null
        /// </summary>
        private static List<Poker> GetJokerBoom(List<Poker> pokers)
        {
            ResetRogue(pokers);
            List<int> jokerS = GetSingleIndex(pokers, PokerValue.SJoker);
            List<int> jokerL = GetSingleIndex(pokers, PokerValue.LJoker);
            if (jokerS.Count > 0 && jokerL.Count > 0)
            {
                return new List<Poker>
                {
                    pokers[jokerS[0]],
                    pokers[jokerL[0]],
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 判断一组牌是否都相同(对子或炸弹)
        /// </summary>
        private static bool IsListSame(List<Poker> pokers)
        {
            if (pokers.Count == 1)
                return true;
            for (int i = 0; i < pokers.Count - 1; i++)
            {
                if (pokers[i].value != pokers[i + 1].value)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断一组牌是否是顺子
        /// </summary>
        private static bool IsListStraight(List<Poker> pokers)
        {
            SortPokerSubList(pokers);
            if (GetSingleIndex(pokers, PokerValue.Two).Count > 0)
                return false;
            for (int i = 0; i < pokers.Count - 1; i++)
            {
                if (pokers[i].value + 1 != pokers[i + 1].value)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断是否为王炸
        /// </summary>
        private static bool IsJokerBoom(List<Poker> pokers)
        {
            if (pokers.Count != 2)
                return false;
            return (pokers[0].value == PokerValue.SJoker && pokers[1].value == PokerValue.LJoker) ||
                    (pokers[0].value == PokerValue.LJoker && pokers[1].value == PokerValue.SJoker);
        }

        /// <summary>
        /// 尝试将一组含万能牌的牌组成相应的顺子
        /// </summary>
        /// <param name="startValue">顺子最小一张牌的大小</param>
        private static bool ToListStraight(List<Poker> pokers, int startValue)
        {
            ResetRogue(pokers);
            SortPokerList(pokers);
            int length = pokers.Count;
            int rogueCnt = GetRogueCnt(pokers);
            int rogueIndex = 0;
            if (startValue + length - 1 > 14)
            {
                return false;
            }
            if (pokers.Count - rogueCnt <= 1)
            {
                return false;
            }
            for (int i = startValue; i < startValue + length; i++)
            {
                if (GetSingleIndex(pokers, i).Count > 0)
                {
                    continue;
                }
                else
                {
                    if (rogueCnt == 0)
                    {
                        return false;
                    }
                    else
                    {
                        pokers[rogueIndex++].value = (PokerValue)i;
                        rogueCnt--;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 提示，包含所有能出的牌
        /// </summary>
        public static List<List<Poker>> SuggestList(List<Poker> pokers, List<Poker> lastPokers)
        {
            List<List<Poker>> result = new();
            List<Poker> list;
            ResetRogue(pokers);
            if (lastPokers != null)
            {
                if (ComparePokerList(pokers, lastPokers))
                {
                    result.Add(new List<Poker>(pokers));
                }
                PokerList target = GetPokerListType(lastPokers);
                switch (target.type)
                {
                    case PokerListType.Single:
                        List<Poker> singles = new();
                        if (target.value != (int)PokerValue.Two)
                        {
                            singles.AddRange(GetTargetPoker(pokers, target.value + 1));
                            singles.AddRange(GetTargetPoker(pokers, (int)PokerValue.Two));
                        }
                        foreach (var single in singles)
                        {
                            list = new List<Poker>()
                            {
                                single
                            };
                            result.Add(list);
                        }
                        break;

                    case PokerListType.Pair:
                        if (target.value != (int)PokerValue.Two)
                        {
                            list = GetPair(pokers, target.value + 1);
                            if (list != null)
                            {
                                result.Add(list);
                            }
                            list = GetPair(pokers, (int)PokerValue.Two);
                            if (list != null)
                            {
                                result.Add(list);
                            }
                        }
                        break;

                    case PokerListType.Straight:
                        list = GetStraight(pokers, target.value + 1, target.length);
                        if (list != null)
                        {
                            result.Add(list);
                        }
                        break;

                    default:
                        break;
                }
                if (target.type == PokerListType.Boom || target.type == PokerListType.JokerBoom)
                {
                    result.AddRange(GetAllBoom(pokers, target));
                }
                else
                {
                    result.AddRange(GetAllBoom(pokers));
                }
            }
            else
            {
                if (GetPokerListType(pokers).type != PokerListType.None)
                {
                    result.Add(new List<Poker>(pokers));
                }
                else
                {
                    result.AddRange(GetStraight(pokers, true));
                    for (int i = Poker.startValue; i <= 15; i++)
                    {
                        list = GetTargetPoker(pokers, i);
                        if (list.Count == 1 || list.Count == 2)
                        {
                            result.Add(list);
                        }
                    }
                    result.AddRange(GetAllBoom(pokers));
                }
                result.Add(new List<Poker>() { pokers[0] });
            }
            return result;
        }

    }
}
