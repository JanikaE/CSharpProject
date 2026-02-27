using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AvaloniaApplication;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        // 处理非UI线程异常
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            try
            {
                if (e.ExceptionObject is Exception exception)
                {
                    Trace.TraceError("非UI线程全局异常：" + exception.Message + "\n" + exception.StackTrace);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("非UI线程转换失败的全局异常：" + ex.Message + "\n" + ex.StackTrace);
            }
        };
        // 处理任务未观察异常
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            Trace.TraceError("未观察的任务异常：" + e.Exception.Message + "\n" + e.Exception.StackTrace);
            e.SetObserved();
        };
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            CreateShortcut();
        }

        // 处理UI线程异常
        Dispatcher.UIThread.UnhandledException += (s, e) =>
        {
            Trace.TraceError("UI线程异常：" + e.Exception.Message + "\n" + e.Exception.StackTrace);
            e.Handled = true;
        };

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// 创建桌面快捷方式
    /// </summary>
    private void CreateShortcut()
    {
        string productName = "AvaloniaApplication";

        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
#if WINDOWS
                //获取当前系统用户桌面目录
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
                FileInfo fileDesktop = new(desktopPath + "\\" + productName + ".lnk");

                WshShell shell = new();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(
                    fileDesktop.FullName
                );
                shortcut.TargetPath = Path.Combine(AppContext.BaseDirectory, productName + ".exe");
                shortcut.WorkingDirectory = Assembly.GetEntryAssembly().Location;
                // 1 激活并显示窗口。如果该窗口被最小化或最大化，则系统将其还原到初始大小和位置。
                // 3 激活窗口并将其显示为最大化窗口。
                // 7 最小化窗口并激活下一个顶级窗口。
                shortcut.WindowStyle = 1;
                shortcut.Description = productName;
                shortcut.IconLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Assets", "avalonia-logo.ico");
                shortcut.Save();
#endif
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (string.IsNullOrEmpty(desktopPath) || !Directory.Exists(desktopPath))
                {
                    desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop");
                    if (!Directory.Exists(desktopPath))
                    {
                        desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/desktop");
                    }
                }
                Trace.TraceInformation($"desktopPath: {desktopPath}");
                string shortcutPath = Path.Combine(desktopPath, $"{productName}.desktop");

                if (!Directory.Exists(desktopPath))
                {
                    Directory.CreateDirectory(desktopPath);
                }
                using (StreamWriter writer = new(shortcutPath))
                {
                    writer.WriteLine("[Desktop Entry]");
                    writer.WriteLine($"Name={productName}");
                    writer.WriteLine($"Comment={productName}");
                    writer.WriteLine($"Exec={Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), productName)}");
                    writer.WriteLine($"Icon={Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Assets", "avalonia-logo.ico")}");
                    writer.WriteLine("Terminal=false");
                    writer.WriteLine("Type=Application");
                    writer.WriteLine("Categories=Utility;Application;");
                }

                Utils.SetExecutablePermission(shortcutPath);
            }
        }
        catch (Exception e)
        {
            Trace.TraceError("更新桌面快捷方式失败：" + e.Message + "\n" + e.StackTrace);
        }
    }
}