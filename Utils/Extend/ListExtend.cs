using System;
using System.Collections.Generic;
using Utils.Mathematical;

namespace Utils.Extend
{
    public static class ListExtend
    {
        /// <summary>
        /// 去除重复数据并排序
        /// </summary>
        public static void Deduplicate<T>(this List<T> list) where T : IEquatable<T>, IComparable<T>
        {
            list.Sort();
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].Equals(list[i + 1]))
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }

        public static string ToStringByItem<T>(this List<T> list)
        {
            string s = "";
            foreach (T p in list)
            {
                if (p == null)
                {
                    s += "null; ";
                }
                else
                {
                    s += p.ToString() + "; ";
                }
            }
            return s;
        }
    }
}
