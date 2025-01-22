using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace AvaloniaApplication;

public partial class App : Application
{
    private TrayIcon _trayIcon;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();

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
        }
        base.OnFrameworkInitializationCompleted();
    }

    #region 托盘菜单

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