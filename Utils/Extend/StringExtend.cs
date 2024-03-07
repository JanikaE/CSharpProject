using System.Collections.Generic;

namespace Utils.Extend
{
    public static class StringExtend
    {
        /// <summary>
        /// 获取字符串中所有的整数，以字符串形式返回，无长度限制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> GetIntegerList(this string str)
        {
            List<string> nums = new();
            string num = string.Empty;
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    num += c;
                }
                else
                {
                    if (num.Length > 0)
                    {
                        nums.Add(num);
                        num = string.Empty;
                    }
                }
            }
            return nums;
        }

        /// <summary>
        /// 获取字符串中所有的整数和小数，以字符串形式返回，无长度限制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> GetDecimalList(this string str)
        {
            List<string> nums = new();
            string num = string.Empty;
            bool point = false;
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    num += c;
                }
                else if (c == '.' && !point)
                {
                    num += c;
                    point = true;
                }
                else
                {
                    if (num.Length > 0)
                    {
                        num = num.TrimEnd('.');
                        nums.Add(num);
                        num = string.Empty;
                        point = false;
                    }
                }
            }
            return nums;
        }
    }
}
