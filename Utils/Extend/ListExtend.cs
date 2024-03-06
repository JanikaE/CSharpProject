using System;
using System.Collections.Generic;

namespace Utils.Extend
{
    /// <summary>
    /// List拓展
    /// </summary>
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
        /// 排序并去除重复数据
        /// </summary>
        /// <param name="comparison">排序方法</param>
        public static void SortAndDeduplicate<T>(this List<T> list, Comparison<T> comparison) where T : IEquatable<T>
        {
            list.Sort(comparison);
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

        /// <summary>
        /// 根据数据内容转变为字符串
        /// </summary>
        /// <param name="separator">数据之间的间隔符，默认为;</param>
        public static string ToStringByItem<T>(this List<T> list, string separator = ";")
        {
            string s = "";
            foreach (T item in list)
            {
                if (item == null)
                {
                    s += "null" + separator;
                }
                else
                {
                    s += item.ToString() + separator;
                }
            }
            return s;
        }

        /// <summary>
        /// 根据数据内容转变为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="func">自定ToString方法</param>
        /// <param name="separator">数据之间的间隔符，默认为;</param>
        /// <returns></returns>
        public static string ToStringByItem<T>(this List<T> list, Func<T, string> func, string separator = ";")
        {
            string s = "";
            foreach (T item in list)
            {
                if (item == null)
                {
                    s += "null" + separator;
                }
                else
                {
                    s += func(item) + separator;
                }
            }
            return s;
        }

        public static List<T> Clone<T>(this List<T> list) 
        { 
            List<T> newList = new(list);
            return newList; 
        }

        /// <summary>
        /// List中是否含有另一个List中的所有元素
        /// </summary>
        /// <param name="subList">另一个List</param>
        /// <returns></returns>
        public static bool Contains<T>(this List<T> list, List<T> subList)
        {
            foreach (T item in subList)
            {
                if (!list.Contains(item))
                    return false;
            }
            return true;
        }

        public static T GetRandomOne<T>(this List<T> list)
        {
            Random random = Random.Shared;
            int index = random.Next(0, list.Count);
            return list[index];
        }

        public static T GetRandomOne<T>(this List<T> list, Random random)
        {
            int index = random.Next(0, list.Count);
            return list[index];
        }
    }
}
