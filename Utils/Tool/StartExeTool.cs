using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Utils.Tool
{
    public class StartExeTool
    {
        /// <summary>
        /// 打开exe
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="outputLogAct"></param>
        /// <param name="errorMsg"></param>
        public static void Open(string exePath, Action<string>? outputLogAct, out string errorMsg)
        {
            errorMsg = "";
            if (string.IsNullOrEmpty(exePath) == true)
            {
                errorMsg = $"启动exe,传入的exe路径 空";
                outputLogAct?.Invoke(errorMsg);
                return;
            }

            var exeName = Path.GetFileNameWithoutExtension(exePath);
            var title = $"启动{exeName}";

            if (File.Exists(exePath) == false)
            {
                errorMsg = $"{title},传入的路径 不存在:{exePath}";
                outputLogAct?.Invoke(errorMsg);
                return;
            }

            IntPtr ppSessionInfo = IntPtr.Zero;
            uint SessionCount = 0;

            if (Win32Help.WTSEnumerateSessions(
                                    Win32Help.WTS_CURRENT_SERVER_HANDLE,
                                    0,  //must be zero
                                    1,  //must be 1
                                    ref ppSessionInfo,
                                    ref SessionCount
                                    ))
            {

                //循环 用能获取到的用户列表 打开exe
                for (int nCount = 0; nCount < SessionCount; nCount++)
                {
                    Win32Help.WTS_SESSION_INFO tSessionInfo = (Win32Help.WTS_SESSION_INFO)Marshal.PtrToStructure(ppSessionInfo + nCount * Marshal.SizeOf(typeof(Win32Help.WTS_SESSION_INFO)),
                        typeof(Win32Help.WTS_SESSION_INFO));

                    var sessionid = tSessionInfo.SessionID;
                    var userState = tSessionInfo.State;

                    var holderMsg = $"{title},第{nCount + 1}次,用户id:{sessionid},状态:{userState}";
                    outputLogAct?.Invoke(holderMsg);
                    if (Win32Help.WTS_CONNECTSTATE_CLASS.WTSActive == userState)
                    {
                        IntPtr hToken = IntPtr.Zero;  //令牌
                        IntPtr hDupedToken = IntPtr.Zero;  //令牌拷贝
                        IntPtr lpEnvironment = IntPtr.Zero;  //区块

                        bool useProcess = Environment.OSVersion.Version.Major >= 6;  //通过找具有足够权限的进程来复制令牌启动 vista之上系统使用

                        if (useProcess == true)
                        {
                            //获取 当前进程里，用户id 一样，且具有特权的进程，取到pid
                            uint proPid = 0;
                            string proName = "";

                            uint nonSystemProPid = 0;
                            string nonSystemPName = "";

                            Process[] processes = Process.GetProcesses();

                            foreach (Process p in processes)
                            {
                                string processUser = GetProcessUser(p, outputLogAct);
                                bool processUserQualified = !string.IsNullOrEmpty(processUser);
                                if (processUserQualified)
                                {
                                    outputLogAct?.Invoke($"进程名称：{p.ProcessName},SessionId={p.SessionId},获取到的用户为：{processUser}");
                                }
                                bool isUserSystem = string.Equals(processUser, "SYSTEM", StringComparison.OrdinalIgnoreCase);

                                if ((uint)p.SessionId == sessionid)
                                {
                                    try
                                    {
                                        //判断是否具有管理员的权限
                                        var hasElevated = IsProcessElevated(p.Handle);
                                        if (!hasElevated) continue;

                                        //需屏蔽的进程名
                                        var noList = new List<string>()
                                                {
                                                    "ServiceHub",
                                                    Process.GetCurrentProcess().ProcessName,
                                                    "Ingress",
                                                    "imsg_agent"
                                                };

                                        if (noList.Where(s => p.ProcessName.Contains(s)).Any() == true)
                                        {
                                            continue;
                                        }

                                        if (!isUserSystem)
                                        {
                                            proName = p.ProcessName;
                                            proPid = (uint)p.Id;
                                        }
                                        else
                                        {
                                            nonSystemProPid = (uint)p.Id;
                                            nonSystemPName = p.ProcessName;
                                            if (proPid != 0) break;
                                        }
                                    }
                                    catch { }
                                }
                            }

                            #region 优先使用用户名为当前登录系统用户的进程

                            if (nonSystemProPid != 0)
                            {
                                proPid = nonSystemProPid;
                                proName = nonSystemPName;
                            }
                            if (nonSystemProPid != 0)
                            {
                                outputLogAct?.Invoke($"{holderMsg},SYSTEM进程：{proName} - {proPid}，用户进程：{nonSystemPName} - {nonSystemProPid}");
                            }

                            #endregion

                            outputLogAct?.Invoke($"{holderMsg},获取到进程名{proName}");
                            if (proPid == 0)
                            {
                                errorMsg = $"{title},未获取到有效的 pid 进程";
                                outputLogAct?.Invoke($"{holderMsg},{errorMsg}");
                                return;
                            }

                            //请求获取 进程 最大(MAXIMUM_ALLOWED) 所有的权限
                            var hProcess = Win32Help.OpenProcess(Win32Help.MAXIMUM_ALLOWED, false, proPid);
                            if (hProcess == IntPtr.Zero)
                            {
                                errorMsg = $"{title},获取句柄 无效,代码:{GetLastErrorMsg()}";
                                outputLogAct?.Invoke($"{title},获取句柄 无效,代码:{GetLastErrorMsg()}");
                                return;
                            }

                            //获取 令牌
                            if (!Win32Help.OpenProcessToken(hProcess, Win32Help.TOKEN_DUPLICATE, ref hToken))
                            {
                                errorMsg = $"{title},未获取到令牌,代码:{GetLastErrorMsg()}";
                                outputLogAct?.Invoke($"{title},未获取到令牌,代码:{GetLastErrorMsg()}");

                                if (hProcess != IntPtr.Zero)
                                {
                                    Win32Help.CloseHandle(hProcess);
                                }

                                //释放 
                                try
                                {
                                    if (hToken != IntPtr.Zero)
                                    {
                                        Win32Help.CloseHandle(hToken);
                                    }
                                }
                                catch { }

                                try
                                {
                                    if (lpEnvironment != IntPtr.Zero)
                                    {
                                        Win32Help.CloseHandle(lpEnvironment);
                                    }
                                }
                                catch { }

                                try
                                {
                                    if (hDupedToken != IntPtr.Zero)
                                    {
                                        Win32Help.CloseHandle(hDupedToken);
                                    }
                                }
                                catch { }

                                return;
                            }
                        }
                        else
                        {
                            if (Win32Help.WTSQueryUserToken(sessionid, out hToken) == false)
                            {
                                errorMsg = $"{title},未获当前用户id:{sessionid}的令牌,代码:{GetLastErrorMsg()}";
                                outputLogAct?.Invoke($"{title},未获当前用户id:{sessionid}的令牌,代码:{GetLastErrorMsg()}");
                                //释放 
                                try
                                {
                                    if (hToken != IntPtr.Zero)
                                    {
                                        Win32Help.CloseHandle(hToken);
                                    }
                                }
                                catch { }

                                try
                                {
                                    if (lpEnvironment != IntPtr.Zero)
                                    {
                                        Win32Help.CloseHandle(lpEnvironment);
                                    }
                                }
                                catch { }

                                try
                                {
                                    if (hDupedToken != IntPtr.Zero)
                                    {
                                        Win32Help.CloseHandle(hDupedToken);
                                    }
                                }
                                catch { }

                                return;
                            }
                        }

                        outputLogAct?.Invoke($"{holderMsg},成功获取到令牌");
                        Win32Help.PROCESS_INFORMATION tProcessInfo = new();

                        var sa = new Win32Help.SECURITY_ATTRIBUTES();
                        sa.Length = Marshal.SizeOf(sa);

                        Win32Help.STARTUPINFO si = new()
                        {
                            cb = Marshal.SizeOf(typeof(Win32Help.STARTUPINFO)),
                            lpDesktop = @"winsta0\default" //指定在 winsta0=主桌面 的默认位置 打开exe
                        };

                        //指定流程优先级和创建方法的标志
                        uint NORMAL_PRIORITY_CLASS = 0x20;
                        uint CREATE_NEW_CONSOLE = 0x00000010;
                        uint dwCreationFlags = NORMAL_PRIORITY_CLASS | CREATE_NEW_CONSOLE;

                        try
                        {
                            //复制令牌
                            bool result = Win32Help.DuplicateTokenEx(
                                         hToken,
                                         //Win32Help.GENERIC_ALL_ACCESS,
                                         Win32Help.MAXIMUM_ALLOWED,
                                         ref sa,
                                         (int)Win32Help.SECURITY_IMPERSONATION_LEVEL.SecurityIdentification,
                                         (int)Win32Help.TOKEN_TYPE.TokenPrimary,
                                         ref hDupedToken
                                         );

                            if (result == false)
                            {
                                outputLogAct?.Invoke($"{holderMsg},复制令牌失败,代码:{GetLastErrorMsg()},跳过");
                                continue;
                            }

                            //创建区块
                            result = Win32Help.CreateEnvironmentBlock(out lpEnvironment, hDupedToken, false);

                            if (result == false)
                            {
                                outputLogAct?.Invoke($"{holderMsg},复制令牌失败,代码:{GetLastErrorMsg()},跳过");
                                continue;
                            }

                            //打开
                            outputLogAct?.Invoke($"{holderMsg},开始CreateProcessAsUser提权");
                            bool ChildProcStarted = ChildProcStarted = Win32Help.CreateProcessAsUser(
                                                                        hDupedToken,
                                                                        exePath,
                                                                        null,
                                                                        ref sa,
                                                                        ref sa,
                                                                        false,
                                                                        //0,
                                                                        dwCreationFlags,
                                                                        null,
                                                                        null,
                                                                        ref si,
                                                                        out tProcessInfo
                                                     );

                            if (ChildProcStarted == false) //失败，继续
                            {
                                outputLogAct?.Invoke($"{holderMsg},失败,代码:{GetLastErrorMsg()},跳过");
                                continue;
                            }

                            //清空错误信息
                            errorMsg = "";

                            break;
                        }
                        catch (Exception ex)
                        {
                            var tempMsg = $"异常,{ex.Message},堆栈:{ex.StackTrace}";
                            outputLogAct?.Invoke($"异常,{ex.Message},堆栈:{ex.StackTrace}");
                            errorMsg += tempMsg + "  ";
                        }
                        finally
                        {
                            outputLogAct?.Invoke($"{holderMsg},释放资源");
                            //释放
                            try
                            {
                                if (tProcessInfo.hThread != IntPtr.Zero)
                                {
                                    Win32Help.CloseHandle(tProcessInfo.hThread);
                                }
                            }
                            catch { }

                            try
                            {

                                if (tProcessInfo.hProcess != IntPtr.Zero)
                                {
                                    Win32Help.CloseHandle(tProcessInfo.hProcess);
                                }
                            }
                            catch { }

                            try
                            {
                                if (hToken != IntPtr.Zero)
                                {
                                    Win32Help.CloseHandle(hToken);
                                }
                            }
                            catch { }

                            try
                            {
                                if (lpEnvironment != IntPtr.Zero)
                                {
                                    Win32Help.CloseHandle(lpEnvironment);
                                }
                            }
                            catch { }

                            try
                            {
                                if (hDupedToken != IntPtr.Zero)
                                {
                                    Win32Help.CloseHandle(hDupedToken);
                                }
                            }
                            catch { }

                        }
                    }
                    else
                    {  
                        outputLogAct?.Invoke($"{holderMsg},状态不符,跳过");
                    }
                }

                if (ppSessionInfo != IntPtr.Zero)
                {
                    _ = Win32Help.WTSFreeMemory(ppSessionInfo);
                }
            }
            else
            {
                outputLogAct?.Invoke($"{title},未能获取到");
            }
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <returns></returns>
        private static string GetLastErrorMsg()
        {
            var va = Marshal.GetLastWin32Error();
            string msg = $"错误代码:{va}";
            switch (va)
            {
                case 1314:
                    {
                        msg += ",权限不足";
                    }
                    break;
                default: { }; break;
            }

            return msg;
        }

        /// <summary>
        /// 是否具有管理员权限
        /// </summary>
        /// <param name="hProcess"></param>
        /// <returns></returns>
        private static bool IsProcessElevated(IntPtr hProcess)
        {
            if (!Win32Help.OpenProcessToken(hProcess, Win32Help.TOKEN_QUERY | Win32Help.TOKEN_ADJUST_PRIVILEGES, out IntPtr hToken))
            {
                return false;
            }

            var elevation = default(Win32Help.TOKEN_ELEVATION);  // 初始化 elevation

            try
            {
                if (!Win32Help.GetTokenInformation(hToken, Win32Help.TOKEN_INFORMATION_CLASS.TokenElevation,
                    ref elevation, Marshal.SizeOf(typeof(Win32Help.TOKEN_ELEVATION)), out var returnLength))
                {
                    return false;
                }

                return elevation.TokenIsElevated != 0;
            }
            finally
            {
                Win32Help.CloseHandle(hToken);
            }
        }

        /// <summary>
        /// 获取进程所属用户名
        /// </summary>
        /// <param name="process"></param>
        /// <param name="outputLogAct"></param>
        /// <returns></returns>
        public static string GetProcessUser(Process process, Action<string>? outputLogAct)
        {
            IntPtr processHandle = IntPtr.Zero;
            IntPtr tokenHandle = IntPtr.Zero;
            string msg = $"进程名：{process.ProcessName}, SessionID:{process.SessionId}, PID:{process.Id}";
            try
            {
                // 打开进程句柄
                processHandle = Win32Help.OpenProcess(Win32Help.PROCESS_QUERY_INFORMATION, false, (uint)process.Id);
                if (processHandle == IntPtr.Zero)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    outputLogAct?.Invoke($"{msg},OpenProcess 失败,错误代码:{errorCode}");
                    return string.Empty;
                }

                // 打开进程令牌
                if (!Win32Help.OpenProcessToken(processHandle, Win32Help.TOKEN_QUERY, out tokenHandle))
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    outputLogAct?.Invoke($"{msg},OpenProcessToken 失败,错误代码:{errorCode}");
                    return string.Empty;
                }

                // 获取令牌的用户信息
                var tokenUser = new Win32Help.TOKEN_USER();
                uint tokenUserSize = (uint)Marshal.SizeOf(tokenUser);
                if (!Win32Help.GetTokenInformation(tokenHandle, Win32Help.TOKEN_INFORMATION_CLASS.TokenUser, out tokenUser, tokenUserSize, out uint returnLength))
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    outputLogAct?.Invoke($"{msg},GetTokenInformation 失败,错误代码:{errorCode}");
                    return string.Empty;
                }

                // 将 SID 转换为字符串
                IntPtr sid = tokenUser.User.Sid;
                if (!Win32Help.ConvertSidToStringSid(sid, out string sidString))
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    outputLogAct?.Invoke($"{msg},ConvertSidToStringSid 失败,错误代码:{errorCode}");
                    return string.Empty;
                }

                // 查询用户名
                var account = new Win32Help.SID_NAME_USE();
                StringBuilder name = new();
                uint nameSize = 256;
                StringBuilder domain = new();
                uint domainSize = 256;
                if (!Win32Help.LookupAccountSid(null, sid, name, ref nameSize, domain, ref domainSize, out account))
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    outputLogAct?.Invoke($"{msg},LookupAccountSid 失败,错误代码:{errorCode}");
                    return string.Empty;
                }

                // 处理特殊进程
                if (string.IsNullOrEmpty(name.ToString()) && string.IsNullOrEmpty(domain.ToString()))
                {
                    return "SYSTEM";
                }

                return $"{domain}\\{name}";
            }
            finally
            {
                if (tokenHandle != IntPtr.Zero)
                {
                    Win32Help.CloseHandle(tokenHandle);
                }
                if (processHandle != IntPtr.Zero)
                {
                    Win32Help.CloseHandle(processHandle);
                }
            }
        }

        private class Win32Help
        {
            /// <summary>
            /// 所有请求权限  
            /// 可以对对象进行任何可能的操作（读取、写入、删除等）
            /// </summary>
            public const int GENERIC_ALL_ACCESS = 0x10000000;

            /// <summary>
            /// 最大权限
            /// 根据目标对象的权限和当前用户的权限来确定你能获得的最大权限
            /// </summary>
            public const int MAXIMUM_ALLOWED = 0x2000000;


            /// <summary>
            /// OpenProcessToken 或 DuplicateToken 等 API 时，标识需要复制现有令牌的操作
            /// </summary>
            public const int TOKEN_DUPLICATE = 0x0002;


            /// <summary>
            /// toekn 调整权限，即特权
            /// </summary>
            public const int TOKEN_ADJUST_PRIVILEGES = 0x0008;

            /// <summary>
            /// token 查询权限
            /// </summary>
            public const int TOKEN_QUERY = 0x20;

            /// <summary>
            /// 
            /// </summary>
            public static readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="pMemory"></param>
            /// <returns></returns>
            [DllImport("WTSAPI32.DLL", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern int WTSFreeMemory(
                IntPtr pMemory);

            /// <summary>
            /// 关闭句柄
            /// </summary>
            /// <param name="hObject"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CloseHandle(
                IntPtr hObject);

            /// <summary>
            /// 获取进程句柄
            /// </summary>
            /// <param name="dwDesiredAccess"></param>
            /// <param name="bInheritHandle"></param>
            /// <param name="dwProcessId"></param>
            /// <returns></returns>

            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(
                uint dwDesiredAccess,
                bool bInheritHandle,
                uint dwProcessId);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ProcessHandle"></param>
            /// <param name="DesiredAccess"></param>
            /// <param name="TokenHandle"></param>
            /// <returns></returns>

            [DllImport("advapi32.dll", SetLastError = true), SuppressUnmanagedCodeSecurityAttribute]
            public static extern bool OpenProcessToken(
                IntPtr ProcessHandle,
                int DesiredAccess,
                ref IntPtr TokenHandle);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="processHandle"></param>
            /// <param name="desiredAccess"></param>
            /// <param name="tokenHandle"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool OpenProcessToken(
                IntPtr processHandle,
                uint desiredAccess,
                out IntPtr tokenHandle);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sessionId">指定要查询的会话的 ID。每个用户会话在 Windows 系统中都有一个唯一的会话ID</param>
            /// <param name="Token">指向一个 IntPtr 的引用，用于接收查询到的用户访问令牌的句柄</param>
            /// <returns>false 表示调用失败。可以通过 Marshal.GetLastWin32Error()</returns>                  
            [DllImport("WTSAPI32.DLL", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern bool WTSQueryUserToken(
                uint sessionId,
                out IntPtr Token);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hServer"></param>
            /// <param name="Reserved"></param>
            /// <param name="Version"></param>
            /// <param name="ppSessionInfo"></param>
            /// <param name="pSessionInfoCount"></param>
            /// <returns></returns>
            [DllImport("WTSAPI32.DLL", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern bool WTSEnumerateSessions(
                IntPtr hServer,
                [MarshalAs(UnmanagedType.U4)] uint Reserved,
                [MarshalAs(UnmanagedType.U4)] uint Version,
                ref IntPtr ppSessionInfo,
                [MarshalAs(UnmanagedType.U4)] ref uint pSessionInfoCount);

            /// <summary>
            /// 创建一个新的进程及其主线程。新进程将在指定用户的安全上下文中运行。
            /// </summary>
            /// <param name="hToken">指向要在其上下文中创建进程的用户的访问令牌的句柄。该令牌通常通过登录操作获得。</param>
            /// <param name="lpApplicationName">要执行的模块的名称。可以是可执行文件的完整路径。如果该参数为 <c>null</c>，则 <paramref name="lpCommandLine"/> 必须指定模块名称。</param>
            /// <param name="lpCommandLine">要执行的命令行。包括应用程序名称及任何参数。如果 <paramref name="lpApplicationName"/> 被指定，则此参数可以设置为 <c>null</c>。</param>
            /// <param name="lpProcessAttributes">指向 SECURITY_ATTRIBUTES 结构的指针，确定返回的句柄是否可以被子进程继承。如果此参数为 <c>null</c>，则句柄不可被继承。</param>
            /// <param name="lpThreadAttributes">指向 SECURITY_ATTRIBUTES 结构的指针，确定线程的返回句柄是否可以被子进程继承。如果此参数为 <c>null</c>，则句柄不可被继承。</param>
            /// <param name="bInheritHandles">布尔值，指定新进程是否继承调用进程的句柄。如果此参数为 <c>true</c>，则新进程继承句柄；否则不继承。</param>
            /// <param name="dwCreationFlags">控制进程创建的标志。这些标志可以指定选项，例如创建新的控制台窗口、挂起进程等。</param>
            /// <param name="lpEnvironment">指向新进程环境块的指针。如果此参数为 <c>null</c>，则新进程使用调用进程的环境。</param>
            /// <param name="lpCurrentDirectory">新进程的当前目录的完整路径。如果此参数为 <c>null</c>，则新进程将使用调用进程的当前目录。</param>
            /// <param name="lpStartupInfo">指向 STARTUPINFO 结构的引用，指定新进程的窗口站、桌面、标准句柄及主窗口的外观。</param>
            /// <param name="lpProcessInformation">输出参数，接收新进程及其主线程的标识信息。这将包括进程和线程的句柄及 ID。</param>
            /// <returns>
            /// 如果函数成功，返回值为 <c>true</c>。如果函数失败，返回值为 <c>false</c>。要获取扩展的错误信息，请调用 Marshal.GetLastWin32Error()。
            /// </returns>
            [DllImport("ADVAPI32.DLL", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool CreateProcessAsUser(
                IntPtr hToken,
                string lpApplicationName,
                string lpCommandLine,
                ref SECURITY_ATTRIBUTES lpProcessAttributes,
                ref SECURITY_ATTRIBUTES lpThreadAttributes,
                bool bInheritHandles,
                uint dwCreationFlags,
                string lpEnvironment,
                string lpCurrentDirectory,
                ref STARTUPINFO lpStartupInfo,
                out PROCESS_INFORMATION lpProcessInformation);

            /// <summary>
            /// 拷贝令牌
            /// </summary>
            /// <param name="hExistingToken"></param>
            /// <param name="dwDesiredAccess"></param>
            /// <param name="lpThreadAttributes"></param>
            /// <param name="ImpersonationLevel"></param>
            /// <param name="dwTokenType"></param>
            /// <param name="phNewToken"></param>
            /// <returns></returns>
            [DllImport("advapi32.dll", SetLastError = true)]
            public static extern bool DuplicateTokenEx(
                IntPtr hExistingToken,
                int dwDesiredAccess,
                ref SECURITY_ATTRIBUTES lpThreadAttributes,
                int ImpersonationLevel,
                int dwTokenType,
                ref IntPtr phNewToken);

            /// <summary>
            /// 创建区块
            /// </summary>
            /// <param name="lpEnvironment"></param>
            /// <param name="hToken"></param>
            /// <param name="bInherit"></param>
            /// <returns></returns>
            [DllImport("userenv.dll", SetLastError = true)]
            public static extern bool CreateEnvironmentBlock(
                out IntPtr lpEnvironment,
                IntPtr hToken,
                bool bInherit);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="tokenHandle"></param>
            /// <param name="tokenInformationClass"></param>
            /// <param name="TokenInformation"></param>
            /// <param name="tokenInformationLength"></param>
            /// <param name="returnLength"></param>
            /// <returns></returns>
            [DllImport("advapi32.dll", SetLastError = true)]
            public static extern bool GetTokenInformation(
                IntPtr tokenHandle,
                TOKEN_INFORMATION_CLASS tokenInformationClass,
                ref TOKEN_ELEVATION TokenInformation,
                int tokenInformationLength,
                out int returnLength);

            public enum TOKEN_INFORMATION_CLASS
            {
                TokenUser = 1,
                TokenElevation = 20,
                TokenElevationType
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct TOKEN_ELEVATION
            {
                public int TokenIsElevated;
            }

            /// <summary>
            /// 设置对象的安全描述符和继承属性
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct SECURITY_ATTRIBUTES
            {
                public int Length;
                public IntPtr lpSecurityDescriptor;
                public bool bInheritHandle;
            }

            public enum SECURITY_IMPERSONATION_LEVEL
            {
                SecurityAnonymous,
                SecurityIdentification,
                SecurityImpersonation,
                SecurityDelegation
            }

            public enum TOKEN_TYPE
            {
                TokenPrimary = 1,
                TokenImpersonation
            }

            /// <summary>
            /// 
            /// </summary>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct PROCESS_INFORMATION
            {
                /// <summary>
                /// 进程的句柄
                /// </summary>
                public IntPtr hProcess;

                /// <summary>
                /// 主线程的句柄
                /// </summary>
                public IntPtr hThread;

                /// <summary>
                /// 进程的 ID
                /// </summary>
                public uint dwProcessId;

                /// <summary>
                /// 主线程的 ID
                /// </summary>
                public uint dwThreadId;
            }

            /// <summary>
            /// 描述新进程在启动时的各种配置和属性
            /// </summary>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct STARTUPINFO
            {
                /// <summary>
                /// 结构体大小
                /// </summary>
                public int cb;

                /// <summary>
                /// 保留，必须为 NULL
                /// </summary>
                [MarshalAs(UnmanagedType.LPWStr)]
                public string lpReserved;

                /// <summary>
                /// 指定桌面名称
                /// </summary>.
                [MarshalAs(UnmanagedType.LPWStr)]
                public string lpDesktop;

                /// <summary>
                /// 窗口标题
                /// </summary>
                [MarshalAs(UnmanagedType.LPWStr)]
                public string lpTitle;

                /// <summary>
                /// 窗口左上角的 X 坐标
                /// </summary>
                public int dwX;

                /// <summary>
                /// 窗口左上角的 Y 坐标
                /// </summary>
                public int dwY;

                /// <summary>
                /// 窗口的宽度
                /// </summary>
                public int dwXSize;

                /// <summary>
                /// 窗口的高度
                /// </summary>
                public int dwYSize;

                /// <summary>
                /// 窗口的字符宽度
                /// </summary>
                public int dwXCountChars;

                /// <summary>
                /// 窗口的字符高度
                /// </summary>
                public int dwYCountChars;

                /// <summary>
                /// 窗口的填充属性
                /// </summary>
                public int dwFillAttribute;

                /// <summary>
                /// 启动标志
                /// </summary>
                public int dwFlags;

                /// <summary>
                /// 窗口显示状态
                /// </summary>
                public short wShowWindow;

                /// <summary>
                /// 保留，必须为 0
                /// </summary>
                public short cbReserved2;

                /// <summary>
                /// 保留，必须为 NULL
                /// </summary>
                public IntPtr lpReserved2;

                /// <summary>
                /// 标准输入的句柄
                /// </summary>
                public IntPtr hStdInput;

                /// <summary>
                /// 标准输出的句柄
                /// </summary>
                public IntPtr hStdOutput;

                /// <summary>
                /// 标准错误的句柄
                /// </summary>
                public IntPtr hStdError;
            }

            /// <summary>
            /// 
            /// </summary>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct WTS_SESSION_INFO
            {
                /// <summary>
                /// 
                /// </summary>
                public uint SessionID;

                /// <summary>
                /// 
                /// </summary>
                public string pWinStationName;

                /// <summary>
                /// 
                /// </summary>
                public WTS_CONNECTSTATE_CLASS State;
            }

            /// <summary>
            /// 会话状态
            /// </summary>
            public enum WTS_CONNECTSTATE_CLASS
            {
                /// <summary>
                /// 会话处于活动状态，用户正在使用
                /// </summary>
                WTSActive,

                /// <summary>
                /// 会话已连接但未处于活动状态
                /// </summary>
                WTSConnected,

                /// <summary>
                /// 会话正在连接，但尚未完全连接
                /// </summary>
                WTSConnectQuery,

                /// <summary>
                /// 会话正在被监视（shadowing）
                /// </summary>
                WTSShadow,

                /// <summary>
                /// 会话已断开连接
                /// </summary>
                WTSDisconnected,

                /// <summary>
                /// 会话处于空闲状态，没有用户活动
                /// </summary>
                WTSIdle,

                /// <summary>
                /// 会话正在等待连接
                /// </summary>
                WTSListen,

                /// <summary>
                /// 会话正在重置
                /// </summary>
                WTSReset,

                /// <summary>
                /// 会话已终止
                /// </summary>
                WTSDown,

                /// <summary>
                /// 会话正在初始化
                /// </summary>
                WTSInit
            }

            #region 获取进程的用户

            public const uint PROCESS_QUERY_INFORMATION = 0x0400;

            // 参照：TOKEN_ELEVATION
            [StructLayout(LayoutKind.Sequential)]
            public struct TOKEN_USER
            {
                public SID_AND_ATTRIBUTES User;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct SID_AND_ATTRIBUTES
            {
                public IntPtr Sid;
                public uint Attributes;
            }

            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool ConvertSidToStringSid(
                IntPtr sid,
                out string stringSid);

            public enum SID_NAME_USE
            {
                SidTypeUser = 1,
                SidTypeGroup,
                SidTypeDomain,
                SidTypeAlias,
                SidTypeWellKnownGroup,
                SidTypeDeletedAccount,
                SidTypeInvalid,
                SidTypeUnknown,
                SidTypeComputer,
                SidTypeLabel
            }

            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool LookupAccountSid(
                string lpSystemName,
                IntPtr sid,
                StringBuilder name,
                ref uint nameSize,
                StringBuilder domain,
                ref uint domainSize,
                out SID_NAME_USE use
            );

            [DllImport("advapi32.dll", SetLastError = true)]
            public static extern bool GetTokenInformation(
                IntPtr tokenHandle,
                TOKEN_INFORMATION_CLASS tokenInformationClass,
                out TOKEN_USER tokenInformation,
                uint tokenInformationLength,
                out uint returnLength
            );

            #endregion
        }
    }
}