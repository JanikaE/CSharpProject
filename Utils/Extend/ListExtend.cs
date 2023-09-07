using System.Collections.Generic;

namespace Utils.Extend
{
    public static class ListExtend
    {
        /// <summary>
        /// 去除重复数据并从小到大排序
        /// </summary>
        public static void Deduplicate(this List<int> list)
        {
            list.Sort();
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i] == list[i + 1])
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
