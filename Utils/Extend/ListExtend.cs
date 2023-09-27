using System;
using System.Collections.Generic;

namespace Utils.Extend
{
    public static class ListExtend
    {
        /// <summary>
        /// 排序并去除重复数据
        /// </summary>
        /// <param name="isAsc">是否升序</param>
        public static void SortAndDeduplicate<T>(this List<T> list, bool isAsc = true) where T : IEquatable<T>, IComparable<T>
        {
            list.Sort((a, b) => 
            {
                if (isAsc)
                {
                    return a.CompareTo(b);
                }
                else
                {
                    return b.CompareTo(a);
                }
            });
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].Equals(list[i + 1]))
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// 去除重复数据
        /// </summary>
        public static void Deduplicate<T>(this List<T> list) where T : IEquatable<T>
        {
            List<T> newList = new();
            foreach (T item in list)
            {
                if (!newList.Contains(item))
                    newList.Add(item);
            }
            list.Clear();
            foreach (T item in newList)
            {
                list.Add(item);
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
