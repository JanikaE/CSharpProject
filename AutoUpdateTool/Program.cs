using AutoUpdateTool.Core;
using System;
using System.Threading;

namespace AutoUpdateTool;

internal class Program
{
    [STAThread]
    public static void Main()
    {
        if (!UpdateCore.IsUpdaterRunning())
        {
            Thread.Sleep(1000);
            new UpdateForm().ShowDialog();
            UpdateCore.RunManagedExe();
        }
    }
}
