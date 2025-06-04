using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utils.Extend
{
    public static class StringExtend
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

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

        #region 中文判断，Unicode范围：\u4e00-\u9fa5

        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasChinese(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 判断一个字符是否是中文字符  
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsChineseChar(this char c)
        {
            return c >= '\u4e00' && c <= '\u9fa5';
        }

        /// <summary>
        /// 检查是否纯中文字符
        /// </summary>
        /// <param name="chineseCharacters"></param>
        /// <returns></returns>
        public static bool IsPureChinese(string chineseCharacters)
        {
            return Regex.IsMatch(chineseCharacters, "^[\u4e00-\u9fa5]+$");
        }

        #endregion

        /// <summary>
        /// 长度截取，超过用...补充
        /// </summary>
        public static string SubstringWithEllipsis(this string str, int length)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (length <= 3)
                return str.Substring2(length);

            if (str.Length > length)
                return string.Concat(str.AsSpan(0, length - 3), "...");
            else
                return str;
        }

        /// <summary>
        /// 长度截取
        /// </summary>
        public static string Substring2(this string str, int length)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length > length)
                return str[..length];
            else
                return str;
        }

        /// <summary>
        /// 解码Html为可阅读的纯文本
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns></returns>
        public static string StripHtml(this string htmlString)
        {
            if (htmlString.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }
            // 替换所有回车换行符成为单行文本
            htmlString = Regex.Replace(htmlString, "([\r\n])[\\s]+", "", RegexOptions.IgnoreCase);
            // 替换可执行块
            htmlString = Regex.Replace(htmlString, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, "<style[^>]*?>.*?</style>", "", RegexOptions.IgnoreCase);
            // 替换Html标签
            htmlString = Regex.Replace(htmlString, "<(?:[^\"'>]|([\"'])[^\"']*\\1)*>", string.Empty);
            // Html解码为可读文本
            return System.Web.HttpUtility.HtmlDecode(htmlString);
        }
    }
}
