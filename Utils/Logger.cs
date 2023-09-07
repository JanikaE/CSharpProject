namespace Utils
{
    public static class Logger
    {
        public static void Info(string s)
        {
            Log("INFO|" + s, LogColor.Green);
        }

        public static void Debug(string s)
        {
            Log("DEBUG|" + s, LogColor.None);
        }

        public static void Warn(string s)
        {
            Log("WARN|" + s, LogColor.Yellow);
        }

        public static void Error(string s)
        {
            Log("ERROR|" + s, LogColor.Red);
        }

        private static void Log(string s, LogColor color = LogColor.None)
        {
            int threadID = Environment.CurrentManagedThreadId;
            switch (color)
            {
                case LogColor.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogColor.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogColor.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    break;
            }
            Console.WriteLine($"{DateTime.Now}|Thread{threadID}|{s}");
            Console.ForegroundColor = ConsoleColor.Gray;
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
