namespace AutoUpdateTool.Model;

public enum UpdateStatus
{
    NoNewVersion,
    Started,
    Upgrading,
    Succeed,
    Fail,
    Cancel,
    RollbackSucceed
}
