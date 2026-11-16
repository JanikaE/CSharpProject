using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UsefulTools.Helpers
{
    /// <summary>
    /// 磁盘类型检测：用于“自动”并发模式时，判断目标路径所在磁盘是否为机械硬盘（存在寻道惩罚）。
    /// </summary>
    public static class DiskTypeHelper
    {
        private static readonly ConcurrentDictionary<char, bool?> Cache = new();

        /// <summary>
        /// 判断指定路径所在磁盘是否为机械硬盘。
        /// 返回 null 表示无法判定（此时调用方应回退到默认行为）。
        /// </summary>
        public static bool? IsMechanicalDisk(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    return null;
                }

                string root = Path.GetPathRoot(path);
                if (string.IsNullOrEmpty(root))
                {
                    return null;
                }

                char letter = char.ToUpperInvariant(root[0]);
                if (!char.IsLetter(letter))
                {
                    return null;
                }

                if (Cache.TryGetValue(letter, out bool? cached))
                {
                    return cached;
                }

                bool? result = QuerySeekPenalty(letter);
                Cache[letter] = result;
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 通过 PowerShell 查询指定盘符对应物理盘的 SeekPenalty（True=机械盘，False=无寻道惩罚的 SSD 等）。
        /// </summary>
        private static bool? QuerySeekPenalty(char driveLetter)
        {
            string script =
                $"try {{ $d = Get-Partition -DriveLetter '{driveLetter}' -ErrorAction Stop; " +
                "$disk = $d | Get-Disk; if ($null -eq $disk) { $null } else { $disk.SeekPenalty } }" +
                " catch { $null }";

            try
            {
                ProcessStartInfo psi = new()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -NonInteractive -Command \"{script}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                psi.StandardOutputEncoding = Encoding.UTF8;
                psi.StandardErrorEncoding = Encoding.UTF8;

                using var proc = Process.Start(psi);
                if (proc == null)
                {
                    return null;
                }

                string output = proc.StandardOutput.ReadToEnd().Trim();
                if (!proc.WaitForExit(5000))
                {
                    try { proc.Kill(); } catch { /* ignore */ }
                    return null;
                }

                return bool.TryParse(output, out bool seek) ? seek : null;
            }
            catch
            {
                return null;
            }
        }
    }
}