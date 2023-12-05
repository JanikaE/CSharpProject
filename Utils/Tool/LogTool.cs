using System;
using System.IO;

namespace Utils.Tool
{
    /// <summary>
    /// 日志工具类
    /// </summary>
    public static class LogTool
    {
        static LogTool()
        {
            ShowDateTime = true;
            ShowThread = false;
            KeepDays = 3;
        }

        #region 消息方法

        /// <summary>
        /// 是否显示时间戳
        /// </summary>
        public static bool ShowDateTime { get; set; }
        /// <summary>
        /// 是否显示线程ID
        /// </summary>
        public static bool ShowThread { get; set; }

        /// <summary>
        /// 消息（绿色）
        /// </summary>
        public static void Info(string s)
        {
            Log(" INFO: " + s, ConsoleColor.Green);
        }

        /// <summary>
        /// 调试（自定义颜色）
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
            Log(" WARN: " + e.Message + "\n" + e.StackTrace, ConsoleColor.Yellow);
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
            Log(" ERROR: " + e.Message + "\n" + e.StackTrace, ConsoleColor.Red);
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

            WriteToFile(s);
        }

        #endregion

        #region 日志写入文件

        private static bool isWriteToFile = false;
        private static string? logPath = null;

        /// <summary>
        /// 初始化将日志同步写入文件的功能
        /// </summary>
        /// <param name="path">日志根目录</param>
        public static void InitFileWriter(string path)
        {
            isWriteToFile = true;
            logPath = path;
        }

        private static void WriteToFile(string s)
        {
            if (isWriteToFile)
            {
                DateTime now = DateTime.Now;
                int year = now.Year;
                int month = now.Month;
                int day = now.Day;
                string path = logPath + $"\\{year}-{month}\\{year}-{month}-{day}.txt";
                using StreamWriter writer = new(path, true)
                {
                    AutoFlush = true
                };
                try
                {
                    writer.WriteLine(s);
                }
                catch (Exception e)
                {
                    Warn(e);
                }
                writer.Close();
            }
        }

        #endregion

        #region 日志清理

        /// <summary>
        /// 日志保存天数
        /// </summary>
        public static int KeepDays { get; set; }

        /// <summary>
        /// 清理过期日志
        /// </summary>
        public static void ClearOutdatedLogs()
        {
            if (logPath == null || !Directory.Exists(logPath))
            {
                return;
            }

            DateTime now = DateTime.Now;
            string[] files = Directory.GetFiles(logPath, "*.txt", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                FileInfo fileInfo = new(file);
                int interval = (now - fileInfo.LastWriteTime).Days;
                if (interval > KeepDays)
                {
                    FileTool.Delete(file, false);
                }
            }
            DirectoryTool.DeleteEmptyFolders(logPath, false);
        }

        #endregion
    }
}
