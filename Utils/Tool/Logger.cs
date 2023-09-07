using System;

namespace Utils.Tool
{
    /// <summary>
    /// 静态Log类
    /// </summary>
    public static class Logger
    {
        private static ConsoleColor defaultColor = ConsoleColor.Gray;
        /// <summary>
        /// 是否显示时间戳
        /// </summary>
        public static bool ShowDateTime { get; set; } = true;
        /// <summary>
        /// 是否显示线程ID
        /// </summary>
        public static bool ShowThread { get; set; } = true;

        /// <summary>
        /// 设置终端默认颜色
        /// </summary>
        public static void SetDefaultColor(ConsoleColor color)
        {
            defaultColor = color;
        }

        /// <summary>
        /// 消息（绿色）
        /// </summary>
        public static void Info(string s)
        {
            Log("INFO|" + s, LogColor.Green);
        }

        /// <summary>
        /// Debug（默认颜色）
        /// </summary>
        public static void Debug(string s)
        {
            Log("DEBUG|" + s, LogColor.None);
        }

        /// <summary>
        /// 警告（黄色）
        /// </summary>
        public static void Warn(string s)
        {
            Log("WARN|" + s, LogColor.Yellow);
        }

        /// <summary>
        /// 错误（红色）
        /// </summary>
        public static void Error(string s)
        {
            Log("ERROR|" + s, LogColor.Red);
        }

        private static void Log(string s, LogColor color = LogColor.None)
        {
            Console.ForegroundColor = color switch
            {
                LogColor.Red => ConsoleColor.Red,
                LogColor.Green => ConsoleColor.Green,
                LogColor.Yellow => ConsoleColor.Yellow,
                _ => defaultColor,
            };
            string pre = "";
            if (ShowDateTime)
            {
                pre += DateTime.Now + "|";
            }
            if (ShowThread)
            {
                pre += Environment.CurrentManagedThreadId + "|";
            }
            Console.WriteLine(pre + s);
            Console.ForegroundColor = defaultColor;
        }

        private enum LogColor
        {
            None,
            Red,
            Yellow,
            Green
        }
    }
}
