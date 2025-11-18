namespace AutoUpdateTool.Model;

public class UpdateCmdArg
{
    public bool IsRollback { get; set; }

    public string DownLoadUrl { get; set; }

    public string FileHash { get; set; }

    public string ManagedExeFileName { get; set; }

    public string ManagedExeArguments { get; set; }
}
