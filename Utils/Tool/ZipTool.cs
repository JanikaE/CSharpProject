using System;
using System.IO;
using System.IO.Compression;

namespace Utils.Tool
{
    /// <summary>
    /// Zip压缩包操作工具类
    /// </summary>
    public static class ZipTool
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="sourceDirectory">源文件夹（不包括文件夹本身）</param>
        /// <param name="targetZip">目标zip文件</param>
        /// <param name="isCover">是否覆盖</param>
        public static bool Compress(string sourceDirectory, string targetZip, bool isCover)
        {
            if (File.Exists(targetZip)) 
            {
                if (isCover) 
                {
                    File.Delete(targetZip);
                }
                else
                {
                    return false;
                }
            }
            ZipFile.CreateFromDirectory(sourceDirectory, targetZip);
            return true;
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="sourceZip">源zip文件</param>
        /// <param name="targetDirectory">目标文件夹</param>
        /// <param name="isCover">是否覆盖</param>
        public static void Decompress(string sourceZip, string targetDirectory, bool isCover)
        {
            string tempPath = targetDirectory + "\\temp" + DateTime.Now.ToString("yyyyMMddhhmmss");
            Directory.CreateDirectory(tempPath);
            // 先解压到一个临时文件夹
            ZipFile.ExtractToDirectory(sourceZip, tempPath);
            // 再从临时文件夹复制到目标文件夹
            DirectoryTool.Copy(tempPath, targetDirectory, isCover);
            // 删除临时文件夹
            DirectoryTool.Delete(tempPath, true);
        }
    }
}
