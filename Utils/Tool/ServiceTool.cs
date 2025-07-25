using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.ServiceProcess;
using System.Threading;

namespace Utils.Tool
{
    public static class ServiceInstaller
    {
        #region DllImport
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenSCManager(
            string lpMachineName,
            string lpSCDB,
            int scParameter);

        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateService(
            IntPtr SC_HANDLE, 
            string lpSvcName, 
            string lpDisplayName,
            int dwDesiredAccess, 
            int dwServiceType,
            int dwStartType, 
            int dwErrorControl, 
            string lpPathName,
            string lpLoadOrderGroup, 
            int lpdwTagId, 
            string lpDependencies, 
            string lpServiceStartName,
            string lpPassword);

        [DllImport("advapi32.dll")]
        private static extern void CloseServiceHandle(
            IntPtr SCHANDLE);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern int StartService(
            IntPtr SVHANDLE, 
            int dwNumServiceArgs, 
            string lpServiceArgVectors);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenService(
            IntPtr SCHANDLE, 
            string lpSvcName, 
            int dwNumServiceArgs);

        [DllImport("advapi32.dll")]
        private static extern int DeleteService(
            IntPtr SVHANDLE);

        [DllImport("kernel32.dll")]
        private static extern int GetLastError();
        #endregion

        #region Constants declaration
        private static readonly int SC_MANAGER_CREATE_SERVICE = 0x0002;
        private static readonly int SERVICE_WIN32_OWN_PROCESS = 0x00000010;
        //private static readonly int SERVICE_DEMAND_START = 0x00000003;
        private static readonly int SERVICE_ERROR_NORMAL = 0x00000001;
        private static readonly int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        private static readonly int SERVICE_QUERY_CONFIG = 0x0001;
        private static readonly int SERVICE_CHANGE_CONFIG = 0x0002;
        private static readonly int SERVICE_QUERY_STATUS = 0x0004;
        private static readonly int SERVICE_ENUMERATE_DEPENDENTS = 0x0008;
        private static readonly int SERVICE_START = 0x0010;
        private static readonly int SERVICE_STOP = 0x0020;
        private static readonly int SERVICE_DELETE = 0x10000;
        private static readonly int SERVICE_PAUSE_CONTINUE = 0x0040;
        private static readonly int SERVICE_INTERROGATE = 0x0080;
        private static readonly int SERVICE_USER_DEFINED_CONTROL = 0x0100;
        private static readonly int SERVICE_ALL_ACCESS = 
            STANDARD_RIGHTS_REQUIRED
            | SERVICE_QUERY_CONFIG
            | SERVICE_CHANGE_CONFIG
            | SERVICE_QUERY_STATUS
            | SERVICE_ENUMERATE_DEPENDENTS
            | SERVICE_START
            | SERVICE_STOP
            | SERVICE_PAUSE_CONTINUE
            | SERVICE_INTERROGATE
            | SERVICE_USER_DEFINED_CONTROL;
        private static readonly int SERVICE_AUTO_START = 0x00000002;
        #endregion

        /// <summary>
        /// 安装和运行
        /// </summary>
        /// <param name="svcPath">程序路径</param>
        /// <param name="svcName">服务名</param>
        /// <param name="svcDispName">服务显示名称</param>
        /// <returns>服务安装是否成功</returns>
        public static bool InstallService(string svcPath, string svcName, string svcDispName)
        {
            IntPtr sc_handle = OpenSCManager(string.Empty, string.Empty, SC_MANAGER_CREATE_SERVICE);
            if (sc_handle.ToInt32() != 0)
            {
                IntPtr sv_handle = CreateService(sc_handle, svcName, svcDispName, SERVICE_ALL_ACCESS, SERVICE_WIN32_OWN_PROCESS, SERVICE_AUTO_START, SERVICE_ERROR_NORMAL, svcPath, string.Empty, 0, string.Empty, string.Empty, string.Empty);
                if (sv_handle.ToInt32() == 0)
                {
                    CloseServiceHandle(sv_handle);
                    CloseServiceHandle(sc_handle);
                    return false;
                }
                else
                {
                    CloseServiceHandle(sv_handle);
                    CloseServiceHandle(sc_handle);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        [SupportedOSPlatform("windows")]
        public static bool ServiceIsInstall(string svcName)
        {
            ServiceController[] service = ServiceController.GetServices();
            foreach (ServiceController s in service)
            {
                if (s.ServiceName == svcName)
                {
                    return true;
                }
            }

            return false;
        }

        [SupportedOSPlatform("windows")]
        public static bool ServiceCanStart(string svcName)
        {
            ServiceController sc = new(svcName);
            if (sc.Status.Equals(ServiceControllerStatus.Stopped) 
                || sc.Status.Equals(ServiceControllerStatus.StopPending))
            {
                // Start the service if the current status is stopped.
                return true;
            }
            return false;
        }

        [SupportedOSPlatform("windows")]
        public static bool ServiceIsRun(string svcName)
        {
            ServiceController sc = new(svcName);
            if (sc.Status.Equals(ServiceControllerStatus.Stopped) 
                || sc.Status.Equals(ServiceControllerStatus.StopPending))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 启动/关闭服务 等待
        /// </summary>
        /// <param name="svcName"></param>
        /// <param name="timeOut">最大超时（秒）</param>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        public static bool ServiceClose(string svcName, int timeOut = 60)
        {
            ServiceController sc = new(svcName);

            int timeIndex = 1;

            if (sc.Status.Equals(ServiceControllerStatus.Stopped) ||
                 sc.Status.Equals(ServiceControllerStatus.StopPending))
            {
                // Start the service if the current status is stopped.
                sc.Start();

                while (sc.Status == ServiceControllerStatus.Stopped)
                {
                    Thread.Sleep(1000);
                    sc.Refresh();

                    timeIndex++;
                    if (timeOut < timeIndex)
                    {
                        return false;
                    }
                }
            }
            else
            {
                // Stop the service if its status is not set to "Stopped".
                sc.Stop();

                while (sc.Status != ServiceControllerStatus.Stopped)
                {
                    if (timeOut < timeIndex)
                    {
                        return false;
                    }
                    Thread.Sleep(1000);
                    sc.Refresh();

                    timeIndex++;
                    if (timeOut < timeIndex)
                    {
                        return false;
                    }
                }
            }
            sc.Refresh();
            return true;
        }

        /// <summary>
        /// 重启Windows Service
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        public static bool RestartService(string serviceName)
        {
            ServiceController service = new(serviceName);
            service.Refresh();
            if (service.Status == ServiceControllerStatus.Running || service.Status == ServiceControllerStatus.StartPending)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMinutes(2)); // 等待服务停止
            }
            if (service.Status == ServiceControllerStatus.Stopped || service.Status == ServiceControllerStatus.StopPending)
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(2)); // 等待服务启动
                return true;
            }
            return false;
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="svcName"></param>
        /// <returns></returns>
        public static bool UnInstallService(string svcName)
        {
            int GENERIC_WRITE = 0x40000000;
            IntPtr sc_hndl = OpenSCManager(string.Empty, string.Empty, GENERIC_WRITE);
            if (sc_hndl.ToInt32() != 0)
            {
                int DELETE = SERVICE_DELETE | SERVICE_STOP;
                IntPtr svc_hndl = OpenService(sc_hndl, svcName, DELETE);
                if (svc_hndl.ToInt32() != 0)
                {
                    int i = DeleteService(svc_hndl);
                    if (i != 0)
                    {
                        CloseServiceHandle(svc_hndl);
                        CloseServiceHandle(sc_hndl);
                        GC.Collect();
                        return true;
                    }
                    else
                    {
                        CloseServiceHandle(svc_hndl);
                        CloseServiceHandle(sc_hndl);
                        GC.Collect();
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取服务安装路径
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        public static string GetWindowsServiceInstallPath(string ServiceName)
        {
            string key = @"SYSTEM\CurrentControlSet\Services\" + ServiceName;
            using RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(key) ?? throw new InvalidOperationException($"Registry key not found: {key}");
            object? imagePathValue = registryKey.GetValue("ImagePath") ?? throw new InvalidOperationException($"ImagePath value not found for service: {ServiceName}");
            string path = imagePathValue.ToString() ?? throw new InvalidOperationException("ImagePath value is null.");
            // 替换掉双引号   
            path = path.Replace("\"", string.Empty);
            FileInfo fi = new(path);
            return fi.Directory?.ToString() ?? throw new InvalidOperationException("Service install directory is null.");
        }
    }
}

