using System;

namespace Utils.Tool
{
    /// <summary>
    /// 控制台操作工具类
    /// </summary>
    public static class ConsoleTool
    {
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
