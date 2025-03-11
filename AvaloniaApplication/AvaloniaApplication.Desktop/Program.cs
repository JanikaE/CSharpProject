using Avalonia;
using Avalonia.Media.Fonts;
using Avalonia.ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xilium.CefGlue;
using Xilium.CefGlue.Common;

namespace AvaloniaApplication.Desktop;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {        
        BuildAvaloniaApp()
            .UseReactiveUI()
            .StartWithClassicDesktopLifetime(args); ;
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        try
        {
            Uri fontName = new("fonts:AppFontFamilies", UriKind.Absolute);
            Uri fontSource = new("avares://AvaloniaOnlyApp/Res/Fonts", UriKind.Absolute);
            var ap = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .ConfigureFonts(manager => manager.AddFontCollection(new EmbeddedFontCollection(fontName, fontSource)))
            .AfterSetup(_ => CefRuntimeLoader.Initialize(new CefSettings()
            {
                WindowlessRenderingEnabled = true,
                //BackgroundColor = new CefColor(0x00, 0xff, 0xff, 0xff),
                Locale = "zh-CN",
                RootCachePath = AppDomain.CurrentDomain.BaseDirectory + "CefCache",
                LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log", "CefLog.log"),
                LogSeverity = CefLogSeverity.Debug,
            },
                flags: new Dictionary<string, string> {
                    {"--ignore-urlfetcher-cert-requests", "1" },
                    {"--ignore-certificate-errors", "1" },
                    {"--disable-web-security", "1" },
                    {"--no-sandbox","1"},
                    {"disable-keyring-access","1" },
                    {"disable-gpu", "1" },
                    {"disable-gpu-compositing", "1" },
                    {"enable-begin-frame-scheduling", "1" },
                    {"disable-gpu-vsync", "1" }
                    //{"--disable-gpu-sandbox","1" },
                }.ToArray()
            ));

            return ap;
        }
        catch (Exception ex)
        {
            Trace.TraceError("Program 异常：" + ex.Message + "\n" + ex.StackTrace);
            throw;
        }
    }
}
