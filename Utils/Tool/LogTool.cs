using System;
using System.IO;

namespace Utils.Tool
{
    /// <summary>
    /// 静态Log类
    /// </summary>
    public static class LogTool
    {
        /// <summary>
        /// 是否显示时间戳
        /// </summary>
        public static bool ShowDateTime { get; set; } = true;
        /// <summary>
        /// 是否显示线程ID
        /// </summary>
        public static bool ShowThread { get; set; } = true;

        private static StreamWriter? fileWriter = null;

        /// <summary>
        /// 初始化，将Log同步写入文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="isCover">是否覆盖文件内原有内容</param>
        public static void InitFileWriter(string path, bool isCover = false)
        {
            if (isCover)
            {
                FileStream fs = new(path, FileMode.Create);
                fs.Close();
            }

            fileWriter = new(path, true)
            {
                AutoFlush = true
            };
            Info("Start writing logs to the file.");
        }

        /// <summary>
        /// 消息（绿色）
        /// </summary>
        public static void Info(string s)
        {
            Log(" INFO: " + s, ConsoleColor.Green);
        }

        /// <summary>
        /// Debug（自定义颜色）
        /// </summary>
        public static void Debug(string s, ConsoleColor color = ConsoleColor.Gray)
        {
            Log(" DEBUG: " + s, color);
        }

        /// <summary>
        /// 警告（黄色）
        /// </summary>
        public static void Warn(string s)
        {
            Log(" WARN: " + s, ConsoleColor.Yellow);
        }

        /// <summary>
        /// 警告（黄色）, 包含调用堆栈
        /// </summary>
        /// <param name="e">异常</param>
        public static void Warn(Exception e)
        {
            Log(" WARN: " + e.Message + "\n" + Environment.StackTrace, ConsoleColor.Yellow);
        }

        /// <summary>
        /// 错误（红色）
        /// </summary>
        public static void Error(string s)
        {
            Log(" ERROR: " + s, ConsoleColor.Red);
        }

        /// <summary>
        /// 错误（红色）, 包含调用堆栈
        /// </summary>
        /// <param name="e">异常</param>
        public static void Error(Exception e)
        {
            Log(" ERROR: " + e.Message + "\n" + Environment.StackTrace, ConsoleColor.Red);
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

            if (fileWriter != null)
            {
                try
                {
                    fileWriter.WriteLine(pre + s);
                }
                catch (Exception e)
                {
                    fileWriter = null;
                    Warn(e);
                }
            }
        }
    }
}
