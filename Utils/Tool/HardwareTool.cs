using System.Linq;
using System.Management;
using System.Runtime.Versioning;

namespace Utils.Tool
{
    public static class HardwareTool
    {
        /// <summary>
        /// 获取CPU序列号
        /// </summary>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        public static string GetCPUSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new("SELECT * FROM Win32_Processor");
                string cpuSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get().Cast<ManagementObject>())
                {
                    cpuSerialNumber = mo["ProcessorId"].ToString().Trim();
                    break;
                }
                return cpuSerialNumber;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns></returns>
        [SupportedOSPlatform ("windows")]
        public static string GetBIOSSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new("SELECT * FROM Win32_BIOS");
                string biosSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get().Cast<ManagementObject>())
                {
                    biosSerialNumber = mo.GetPropertyValue("SerialNumber").ToString().Trim();
                    break;
                }
                return biosSerialNumber;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        public static string GetHardDiskSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new("SELECT * FROM Win32_PhysicalMedia");
                string hardDiskSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get().Cast<ManagementObject>())
                {
                    hardDiskSerialNumber = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return hardDiskSerialNumber;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取网卡地址
        /// </summary>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        public static string GetNetCardMACAddress()
        {
            try
            {
                ManagementObjectSearcher searcher = new("SELECT * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))");
                string netCardMACAddress = "";
                foreach (ManagementObject mo in searcher.Get().Cast<ManagementObject>())
                {
                    netCardMACAddress = mo["MACAddress"].ToString().Trim();
                    break;
                }
                return netCardMACAddress;
            }
            catch
            {
                return "";
            }
        }
    }
}
