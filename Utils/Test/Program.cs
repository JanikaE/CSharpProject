using Utils.Mathematical;
using Utils.Tool;

internal class Program
{
    private static void Main(string[] args)
    {
        LogTool.InitFileWriter(".\\log.txt", true);
        LogTool.Debug("test2");
        Console.ReadLine();
    }
}