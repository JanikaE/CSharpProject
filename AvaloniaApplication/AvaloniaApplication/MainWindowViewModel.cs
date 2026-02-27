using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace AvaloniaApplication
{
    public class MainWindowViewModel
    {
        public ICommand SwitchFullScreenCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand OpenDevToolsCommand { get; set; }
        public ICommand ExitApplicationCommand { get; set; }

        public MainWindowViewModel()
        {
            //初始化命令
            SwitchFullScreenCommand = new RelayCommand(SwitchFullScreen);
            RefreshCommand = new RelayCommand(Refresh);
            OpenDevToolsCommand = new RelayCommand(OpenDevTools);
            ExitApplicationCommand = new RelayCommand(ExitApplication);
        }

        /// <summary>
        /// 切换全屏显示
        /// </summary>
        private void SwitchFullScreen()
        {
            var lifetime = Avalonia.Application.Current.ApplicationLifetime;
            if (lifetime is IClassicDesktopStyleApplicationLifetime desktop)
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
            var lifetime = Avalonia.Application.Current.ApplicationLifetime;
            if (lifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (desktop.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.ActiveBrowserView.OpenDevTools();
                }
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            var lifetime = Avalonia.Application.Current.ApplicationLifetime;
            if (lifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (desktop.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.ActiveBrowserView.Refresh();
                }
            }
        }

        /// <summary>
        /// 退出应用程序
        /// </summary>
        private void ExitApplication()
        {
            var lifetime = Avalonia.Application.Current.ApplicationLifetime;
            if (lifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    }
}