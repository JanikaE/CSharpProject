using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication
{
    internal static class Utils
    {
        /// <summary>
        /// 赋予可执行权限
        /// </summary>
        /// <param name="filePath"></param>
        public static void SetExecutablePermission(string filePath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                try
                {
                    Process process = new();
                    process.StartInfo.FileName = "chmod";
                    process.StartInfo.Arguments = $"+x {filePath}";
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                }
                catch (Exception ex)
                {
                    Trace.TraceError($"设置可执行权限失败: {ex.Message}");
                }
            }
        }
    }
}
