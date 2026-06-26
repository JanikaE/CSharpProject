using System;

namespace UsefulTools.Helpers
{
    /// <summary>
    /// Base64 编解码工具类
    /// </summary>
    public static class Base64Helper
    {
        /// <summary>
        /// 去除 Data URI 前缀（如 "data:image/jpeg;base64,"），返回纯 Base64 字符串
        /// </summary>
        public static string StripDataUriPrefix(string input)
        {
            int base64Index = input.IndexOf(";base64,", StringComparison.OrdinalIgnoreCase);
            if (base64Index >= 0)
            {
                return input.Substring(base64Index + ";base64,".Length);
            }
            return input;
        }

        /// <summary>
        /// 将 Base64 字符串解码为字节数组
        /// </summary>
        public static byte[] Decode(string base64)
        {
            return Convert.FromBase64String(base64);
        }

        /// <summary>
        /// 尝试将 Base64 字符串解码为字节数组，失败返回 null
        /// </summary>
        public static byte[] TryDecode(string base64)
        {
            try
            {
                return Convert.FromBase64String(base64);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
