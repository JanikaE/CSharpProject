using CefNet;
using System.Runtime.InteropServices;

namespace AvaloniaApplication;

public class CefAppImpl : CefNetApplication
{
    protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
    {
        base.OnBeforeCommandLineProcessing(processType, commandLine);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            commandLine.AppendSwitch("no-zygote");
            commandLine.AppendSwitch("no-sandbox");
        }
    }
}