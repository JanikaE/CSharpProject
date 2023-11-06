using Utils.Mathematical;
using Utils.Tool;

internal class Program
{
    private static void Main(string[] args)
    {
        Logger.InitFileWriter(".\\log.txt", true);
        Logger.Debug("test2");
        Console.ReadLine();
    }
}