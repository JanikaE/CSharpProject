using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using Avalonia.Threading;
using Avalonia.Logging;
using IWshRuntimeLibrary;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace AvaloniaApplication;

public partial class App : Application
{
    //private TrayIcon _trayIcon;

    public App()
    {
        //初始化命令
        SwitchFullScreenCommand = ReactiveCommand.Create(SwitchFullScreen);
        RefreshCommand = ReactiveCommand.Create(Refresh);
        OpenDevToolsCommand = ReactiveCommand.Create(OpenDevTools);
        ExitApplicationCommand = ReactiveCommand.Create(ExitApplication);

        DataContext = this;
    }

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
            desktop.MainWindow = new MainWindow(this);
            CreateShortcut();

            /* 创建托盘图标和菜单
            // 创建托盘图标
            _trayIcon = new TrayIcon
            {
                Icon = new("Assets/avalonia-logo.ico"),
                ToolTipText = "AvaloniaApplication"
            };

            // 创建菜单
            var menu = new NativeMenu();
            menu.Items.Add(new NativeMenuItem("刷新")
            {
                Command = ReactiveCommand.Create(Refresh)
            });
            menu.Items.Add(new NativeMenuItem("开发者工具")
            {
                Command = ReactiveCommand.Create(OpenDevTools),
            });
            menu.Items.Add(new NativeMenuItem("退出")
            {
                Command = ReactiveCommand.Create(ExitApplication)
            });

            _trayIcon.Menu = menu;
            _trayIcon.IsVisible = true;
            */
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

                // 赋予可执行权限
                Process process = new();
                process.StartInfo.FileName = "chmod";
                process.StartInfo.Arguments = $"+x {shortcutPath}";
                process.StartInfo.UseShellExecute = false;
                process.Start();
            }
        }
        catch (Exception e)
        {
            Trace.TraceError("更新桌面快捷方式失败：" + e.Message + "\n" + e.StackTrace);
        }
    }

    #region 托盘菜单

    private string _switchFullScreenHeader;

    public string SwitchFullScreenHeader
    {
        get => _switchFullScreenHeader;
        set
        {
            if (_switchFullScreenHeader != value)
            {
                _switchFullScreenHeader = value;
                Initialize();
            }
        }
    }

    public ICommand SwitchFullScreenCommand { get; set; }
    public ICommand RefreshCommand { get; set; }
    public ICommand OpenDevToolsCommand { get; set; }
    public ICommand ExitApplicationCommand { get; set; }

    /// <summary>
    /// 切换全屏显示
    /// </summary>
    private void SwitchFullScreen()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow != null)
            {
                if (desktop.MainWindow.WindowState != WindowState.FullScreen)
                {
                    desktop.MainWindow.WindowState = WindowState.FullScreen;
                }
                else
                {
                    desktop.MainWindow.WindowState = WindowState.Normal;
                }
            }
        }
    }

    /// <summary>
    /// 开发者工具
    /// </summary>
    private void OpenDevTools()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow is MainWindow window)
            {
                window.ActiveBrowserView?.OpenDevTools();
            }
        }
    }    
    
    /// <summary>
    /// 刷新
    /// </summary>
    private void Refresh()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow is MainWindow window)
            {
                window.ActiveBrowserView?.Refresh();
            }
        }
    }

    /// <summary>
    /// 退出应用程序
    /// </summary>
    private void ExitApplication()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow is MainWindow window)
            {
                window.ActiveBrowserView?.Dispose();
            }
            desktop.Shutdown();
        }
    }

    #endregion
}