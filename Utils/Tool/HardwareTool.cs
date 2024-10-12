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
                    var o = mo["ProcessorId"].ToString();
                    if (o != null)
                    {
                        cpuSerialNumber = o.Trim();
                        break;
                    }
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
                    var o = mo.GetPropertyValue("SerialNumber").ToString();
                    if (o != null) 
                    {
                        biosSerialNumber = o.Trim();
                        break;
                    }                    
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
                    var o = mo["SerialNumber"].ToString();
                    if (o != null)
                    {
                        hardDiskSerialNumber = o.Trim();
                        break;
                    }
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
                    var o = mo["MACAddress"].ToString();
                    if (o != null) { 
                        netCardMACAddress = o.Trim();
                        break;
                    }
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
