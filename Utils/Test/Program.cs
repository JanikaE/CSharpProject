using Utils.Tool;

internal class Program
{
    private static void Main()
    {
        string path = "D:\\Users\\1\\Code\\CS\\Utils\\Test\\test\\log";
        LogTool.InitFileWriter(path);

        string sourceDirectory = "D:\\Users\\1\\Code\\CS\\Utils\\Test\\test\\source";
        string zip = "D:\\Users\\1\\Code\\CS\\Utils\\Test\\test\\Z.zip";
        string target = "D:\\Users\\1\\Code\\CS\\Utils\\Test\\test\\target";
        try
        {
            ZipTool.Compress(sourceDirectory, zip, true);
            LogTool.Debug("压缩" + zip);
            ZipTool.Decompress(zip, target, true);
            LogTool.Debug("解压" + target);
            LogTool.Debug(FileTool.GetMD5(zip));
            //LogTool.ClearOutdatedLogs();
        }
        catch (Exception e)
        {
            LogTool.Error(e);
        }
    }
}