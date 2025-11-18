using AutoUpdateTool.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Utils.Tool;

namespace AutoUpdateTool.Core;

public static class UpdateCore
{
    public static UpdateStatus UpdateSign { get; set; }

    private static UpdateCmdArg _updateCmdArg;

    private static Mutex _updaterMutex;

    public static bool IsUpdaterRunning()
    {
        _updaterMutex = new Mutex(initiallyOwned: true, "eWorld.AutoUpdater", out bool notUpdating);
        return !notUpdating;
    }

    public static bool TryResolveUpdateService(out IUpdateService updateService)
    {
        updateService = null;
        List<string> runArgs = Environment.GetCommandLineArgs().ToList();
        runArgs.RemoveAt(0);
        if (runArgs.Count == 0)
        {
            return false;
        }
        try
        {
            byte[] argData = Convert.FromBase64String(runArgs[0]);
            _updateCmdArg = XmlTool.ToObject<UpdateCmdArg>(argData);
            if (_updateCmdArg.IsRollback)
            {
                updateService = new RollbackService(_updateCmdArg);
            }
            else
            {
                updateService = new UpdateService(_updateCmdArg);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static void RunManagedExe()
    {
        _updaterMutex.ReleaseMutex();
        _updaterMutex.Dispose();
        if (_updateCmdArg != null)
        {
            UpdateStatus sign = UpdateSign;
            if (_updateCmdArg.IsRollback && UpdateSign == UpdateStatus.Succeed)
            {
                sign = UpdateStatus.RollbackSucceed;
            }
            //startInfo.FileName = _updateCmdArg.ManagedExeFileName;
            //startInfo.Arguments = _updateCmdArg.ManagedExeArguments + " " + sign;
            //MessageBox.Show(startInfo.FileName + " " + startInfo.Arguments);
            Process.Start(_updateCmdArg.ManagedExeFileName, _updateCmdArg.ManagedExeArguments + " " + sign);
        }
    }
}
