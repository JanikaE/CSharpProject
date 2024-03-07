using System;
using System.Collections.Generic;
using Utils.Extend;

namespace Utils.Tool
{
    public static class ListTool
    {
        /// <summary>
        /// 交集∩，返回中不含重复数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static List<T> Intersection<T>(List<T> left, List<T> right) where T : IEquatable<T>
        {
            List<T> result = new();
            foreach (T item in left)
            {
                if (right.Contains(item))
                {
                    result.Add(item);
                }
            }
            result.Deduplicate();
            return result;
        }

        /// <summary>
        /// 并集∪，返回中不含重复数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static List<T> Union<T>(List<T> left, List<T> right) where T : IEquatable<T>
        {
            List<T> result = new(left);
            result.AddRange(right);
            result.Deduplicate();
            return result;
        }
    }
}
