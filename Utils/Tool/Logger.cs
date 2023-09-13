using System;

namespace Utils.Tool
{
    /// <summary>
    /// 静态Log类
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// 是否显示时间戳
        /// </summary>
        public static bool ShowDateTime { get; set; } = true;
        /// <summary>
        /// 是否显示线程ID
        /// </summary>
        public static bool ShowThread { get; set; } = true;

        /// <summary>
        /// 消息（绿色）
        /// </summary>
        public static void Info(string s)
        {
            Log(" INFO " + s, ConsoleColor.Green);
        }

        /// <summary>
        /// Debug（自定义颜色）
        /// </summary>
        public static void Debug(string s, ConsoleColor color = ConsoleColor.Gray)
        {
            Log(" DEBUG " + s, color);
        }

        /// <summary>
        /// 警告（黄色）
        /// </summary>
        public static void Warn(string s)
        {
            Log(" WARN " + s, ConsoleColor.Yellow);
        }

        /// <summary>
        /// 错误（红色）
        /// </summary>
        public static void Error(string s)
        {
            Log(" ERROR " + s, ConsoleColor.Red);
        }

        private static void Log(string s, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            string pre = "#";
            if (ShowDateTime)
            {
                pre += " " + DateTime.Now + " ";
            }
            if (ShowThread)
            {
                pre += " " + Environment.CurrentManagedThreadId + " ";
            }
            pre += ">> ";
            Console.WriteLine(pre + s);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
