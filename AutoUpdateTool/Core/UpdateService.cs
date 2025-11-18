using System;
using System.IO;
using System.Linq;
using System.Net;
using AutoUpdateTool.Model;
using Utils.Tool;

namespace AutoUpdateTool.Core;

public class UpdateService : IUpdateService
{
    private static readonly string BakFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BakFolder");

    private static readonly string TempFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");

    private static readonly string UpdaterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UpdaterTemp");

    private static readonly string AutoUpdaterFileName = "AutoUpdateTool";

    private readonly string _targetFolder;

    private readonly UpdateCmdArg _updateCmdArg;

    public event EventHandler<UpdateStartedArgs> UpdateStarted;

    public event EventHandler<UpdateProgressArgs> UpdateProgressChanged;

    public event EventHandler<UpdateEndedArgs> UpdateEnded;

    public UpdateService(UpdateCmdArg updateCmdArg)
    {
        _targetFolder = AppDomain.CurrentDomain.BaseDirectory;
        _updateCmdArg = updateCmdArg;
    }

    public void UpdateNow()
    {
        RaiseUpdateStarted("检测到新版本，正在升级...");
        float percent = 0f;
        string newZipName = _updateCmdArg.FileHash + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".zip";
        string newZipPath = Path.Combine(_targetFolder, newZipName);

        try
        {
            LogTool.Debug("1.开始下载安装包");

            // 使用 HttpClient 下载文件
            HttpRequestTool.DownloadFileWithProgress(
                _updateCmdArg.DownLoadUrl,
                newZipPath,
                onProgress: (downloaded, total, percent) =>
                {
                    double downloadM = Math.Round(downloaded / 1024.0 / 1024.0, 2, MidpointRounding.AwayFromZero);
                    double totalM = Math.Round(total / 1024.0 / 1024.0, 2, MidpointRounding.AwayFromZero);
                    float progressPercent = totalM != 0.0 ? (float)(downloadM / totalM * 0.9) : 0f;
                    RaiseUpdateProgress($"正在下载文件... {downloadM}M /{totalM}M ", progressPercent);
                },
                validateHeaders: (headers) =>
                {
                    string contentDesc = headers.GetValues("Content-Disposition")?.FirstOrDefault();
                    if (!string.IsNullOrEmpty(contentDesc) && !contentDesc.Contains(".zip"))
                    {
                        throw new Exception("服务安装包格式异常，跳过本次升级");
                    }
                }
            ).GetAwaiter().GetResult();

            RaiseUpdateProgress("下载完成，开始安装...", 0.9f);
        }
        catch (Exception ex)
        {
            string msg = ex.Message.StartsWith("服务安装包格式异常") ? ex.Message : "下载失败";
            RaiseUpdateEnded(msg, ex);
            LogTool.Error(ex);
            return;
        }

        try
        {
            LogTool.Debug("2.开始校验安装包hash");
            if (string.IsNullOrEmpty(_updateCmdArg.FileHash))
            {
                LogTool.Debug("未提供预期的哈希值，跳过校验");
            }
            else
            {
                if (!string.Equals(FileTool.GetMD5(newZipPath), _updateCmdArg.FileHash))
                {
                    RaiseUpdateEnded("文件校验出错，疑似服务端文件被修改,退出此次升级", null);
                    return;
                }
            }
            RaiseUpdateProgress("文件对比完成，正在安装... ", percent += 0.01f);
        }
        catch (Exception ex2)
        {
            RaiseUpdateEnded("文件校验出错,退出此次升级", ex2);
            LogTool.Error(ex2);
            return;
        }

        try
        {
            LogTool.Debug("3.开始解压安装包");
            ZipTool.Decompress(newZipPath, TempFolder, true);
            // todo: 需要兼容压缩包内还套了一层文件夹的情况
            RaiseUpdateProgress("解压成功... ", percent += 0.02f);
        }
        catch (Exception ex3)
        {
            RaiseUpdateEnded("解压缩失败" + ex3.Message + ex3.StackTrace, ex3);
            LogTool.Error(ex3);
            return;
        }
        finally
        {
            File.Delete(newZipPath);
        }

        try
        {
            LogTool.Debug("4.开始寻找解压目录下的升级程序");
            DirectoryTool.Delete(UpdaterFolder);
            if (File.Exists(Path.Combine(TempFolder, AutoUpdaterFileName + ".exe")))
            {
                Directory.CreateDirectory(UpdaterFolder);
                File.Copy(Path.Combine(TempFolder, AutoUpdaterFileName + ".exe"), Path.Combine(UpdaterFolder, AutoUpdaterFileName + ".exe"), overwrite: true);
                File.Delete(Path.Combine(TempFolder, AutoUpdaterFileName + ".exe"));
            }
        }
        catch (Exception error)
        {
            LogTool.Error(error);
            DirectoryTool.Delete(UpdaterFolder);
        }

        try
        {
            LogTool.Debug("5.开始备份升级程序");
            //FileHelper.DeleteFiles(BakFolder);
            DirectoryTool.Copy(_targetFolder, BakFolder, true,
                new string[] {
                    TempFolder,
                    UpdaterFolder,
                },
                new string[] {
                    Path.Combine(_targetFolder, AutoUpdaterFileName + ".exe"),
                    newZipPath,
                }
            );
            RaiseUpdateProgress("备份当前版本成功，正在安装... ", percent += 0.02f);
        }
        catch (Exception ex4)
        {
            string msg2 = "备份当前版本异常";
            RaiseUpdateEnded(msg2, ex4);
            LogTool.Error(ex4);
            return;
        }

        try
        {
            LogTool.Debug("6.开始安装");
            DirectoryTool.Copy(TempFolder, _targetFolder, true);
            RaiseUpdateProgress("安装成功. ", percent += 0.05f);
        }
        catch (Exception ex5)
        {
            try
            {
                RaiseUpdateProgress("安装失败，正在回滚... ", percent += 0.01f);
                DirectoryTool.Copy(BakFolder, _targetFolder, true);
            }
            catch (Exception errorException)
            {
                RaiseUpdateEnded("备份当前版本异常", errorException);
            }
            RaiseUpdateEnded("新版本覆盖出错", ex5);
            LogTool.Error(ex5);
            return;
        }
        finally
        {
            DirectoryTool.Delete(TempFolder);
        }

        LogTool.Debug("安装成功");
        RaiseUpdateEnded();
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
