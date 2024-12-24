using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication.ViewModels;
using AvaloniaApplication.Views;
using CefNet;
using System;
using System.IO;

namespace AvaloniaApplication;

public partial class App : Application
{
    internal static CefAppImpl? app;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            desktop.Startup += Desktop_Startup;
            desktop.Exit += Desktop_Exit;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void Desktop_Startup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
        var settings = new CefSettings
        {
            MultiThreadedMessageLoop = true,
            NoSandbox = true,
            WindowlessRenderingEnabled = true,
            LocalesDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cef", "Resources", "locales"),
            ResourcesDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cef", "Resources"),
            LogSeverity = CefLogSeverity.Warning,
            UncaughtExceptionStackSize = 8
        };

        app = new CefAppImpl();
        app.Initialize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cef", "Release"), settings);
    }

    private void Desktop_Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        app?.Shutdown();
    }
}