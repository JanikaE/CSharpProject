using System;
using System.IO;
using AutoUpdateTool.Model;
using Utils.Tool;

namespace AutoUpdateTool.Core;

public class RollbackService : IUpdateService
{
    private static readonly string BakFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BakFolder");

    private readonly string _targetFolder;

    private readonly UpdateCmdArg _updateCmdArg;

    public event EventHandler<UpdateStartedArgs> UpdateStarted;

    public event EventHandler<UpdateProgressArgs> UpdateProgressChanged;

    public event EventHandler<UpdateEndedArgs> UpdateEnded;

    public RollbackService(UpdateCmdArg updateCmdArg)
    {
        _targetFolder = AppDomain.CurrentDomain.BaseDirectory;
        _updateCmdArg = updateCmdArg;
    }

    public void UpdateNow()
    {
        RaiseUpdateStarted("正在回退版本...");
        float percent = 0f;
        try
        {
            RaiseUpdateProgress("回退中... ", percent += 0.1f);
            LogTool.Debug("1.开始回退");
            if (!Directory.Exists(BakFolder))
            {
                RaiseUpdateEnded("备份目录不存在，退出本次回退", null);
                return;
            }
            DirectoryTool.Copy(BakFolder, AppDomain.CurrentDomain.BaseDirectory, true);
            DirectoryTool.Delete(BakFolder, true);
            RaiseUpdateProgress("回退完成. ", percent += 0.9f);
            LogTool.Debug("回退完成");
            RaiseUpdateEnded();
        }
        catch (Exception ex)
        {
            string msg = "回滚失败";
            RaiseUpdateEnded(msg, ex);
            LogTool.Error(ex);
        }
    }

    private void RaiseUpdateStarted(string text)
    {
        UpdateStarted?.BeginInvoke(this, new UpdateStartedArgs
        {
            Text = text
        }, null, null);
    }

    private void RaiseUpdateEnded()
    {
        UpdateEnded?.BeginInvoke(this, new UpdateEndedArgs(), null, null);
    }

    private void RaiseUpdateEnded(string errorMessage, Exception errorException)
    {
        UpdateEnded?.BeginInvoke(this, new UpdateEndedArgs(errorMessage, errorException), null, null);
    }

    private void RaiseUpdateProgress(string text, float percent)
    {
        UpdateProgressChanged?.BeginInvoke(this, new UpdateProgressArgs
        {
            Text = text,
            ProgressPercent = percent
        }, null, null);
    }
}
