using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AvaloniaApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if WINDOWLESS
            Title += " - OSR mode";
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            CreateNewTab();

            var mainMenu = this.FindControl<Menu>("mainMenu");
            mainMenu.AttachedToVisualTree += MenuAttached;

            Resized += MainWindow_Resized;
            KeyDown += MainWindow_KeyDown;

            SetAutoStart(false);
        }

        private void MainWindow_Resized(object sender, WindowResizedEventArgs e)
        {
            if (ActiveBrowserView?.IsInitialized == true)
            {
                ActiveBrowserView.Refresh();
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F11:
                    // 切换全屏
                    OnSwitchFullScreenMenuItemClick(sender, e);
                    break;
                case Key.F5:
                    // 刷新
                    OnRefreshMenuItemClick(sender, e);
                    break;
                case Key.F12:
                    // 开发者工具
                    OnOpenDevToolsMenuItemClick(sender, e);
                    break;
            }
        }

        private void MenuAttached(object sender, VisualTreeAttachmentEventArgs e)
        {
            if (NativeMenu.GetIsNativeMenuExported(this) && sender is Menu mainMenu)
            {
                mainMenu.IsVisible = false;
            }
        }

        public BrowserView ActiveBrowserView => (BrowserView)this.FindControl<TabControl>("tabControl").SelectedContent;

        private void CreateNewTab()
        {
            var tabControl = this.FindControl<TabControl>("tabControl");

            var view = new BrowserView();
            var tab = new TabItem();

            var headerPanel = new DockPanel();
            tab.Header = headerPanel;

            var closeButton = new Button()
            {
                Content = "X",
                Padding = new Thickness(2),
                Margin = new Thickness(5, 0, 0, 0)
            };
            closeButton.Click += delegate
            {
                view.Dispose();
                tabControl.Items.Remove(tab);
            };
            DockPanel.SetDock(closeButton, Dock.Right);

            var tabTitle = new TextBlock()
            {
                Text = "New Tab"
            };
            headerPanel.Children.Add(tabTitle);
            headerPanel.Children.Add(closeButton);

            view.TitleChanged += title =>
            {
                Dispatcher.UIThread.Post((Action)(() =>
                {
                    tabTitle.Text = title;
                    ToolTip.SetTip(tab, title);
                }));
            };

            tab.Content = view;

            tabControl.Items.Add(tab);
        }

        /// <summary>
        /// 设置开机启动状态
        /// </summary>
        /// <param name="autoStart">true:开启，false：关闭</param>
        public void SetAutoStart(bool autoStart)
        {
            string productName = "AvaloniaApplication";

            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
#if WINDOWS
                    //获取当前系统用户启动目录
                    string startupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), productName + ".lnk");
                    try
                    {
                        // 先尝试将旧的删除
                        File.Delete(startupPath);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    if (autoStart)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(startupPath));

                        IWshRuntimeLibrary.WshShell shell = new();
                        IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(
                            startupPath
                        );
                        shortcut.TargetPath = Path.Combine(AppContext.BaseDirectory, productName + ".exe");
                        shortcut.WorkingDirectory = Assembly.GetEntryAssembly().Location;
                        shortcut.WindowStyle = 7;
                        shortcut.Description = productName;
                        shortcut.IconLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Assets", "favicon.ico");
                        shortcut.Save();
                    }
                    else
                    {
                        File.Delete(startupPath);
                    }
#endif
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    // 获取用户的自启动目录
                    string autostartDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "autostart");
                    // 确保目录存在
                    Directory.CreateDirectory(autostartDir);
                    string autostartFilePath = Path.Combine(autostartDir, $"{productName}.desktop");
                    if (autoStart)
                    {
                        string desktopFileContent = $@"[Desktop Entry]
                            Version=1.0
                            Type=Application
                            Name={productName}
                            Comment={productName}
                            Exec={Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "eWorldCallClient")}
                            Path={Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))}
                            Terminal=false
                            X-GNOME-Autostart-enabled=true";

                        File.WriteAllText(autostartFilePath, desktopFileContent);

                        Utils.SetExecutablePermission(autostartFilePath);
                    }
                    else
                    {
                        File.Delete(autostartFilePath);
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError("启用开机自启动失败：" + e.Message + "\n" + e.StackTrace);
            }
        }

        #region Menu

        private void OnNewTabMenuItemClick(object sender, RoutedEventArgs e)
        {
            CreateNewTab();
        }

        private void OnSwitchFullScreenMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.FullScreen)
            {
                WindowState = WindowState.FullScreen;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void OnRefreshMenuItemClick(object sender, RoutedEventArgs e)
        {
            ActiveBrowserView.Refresh();
        }

        private void OnOpenDevToolsMenuItemClick(object sender, RoutedEventArgs e)
        {
            ActiveBrowserView?.OpenDevTools();
        }

        #endregion
    }
}
