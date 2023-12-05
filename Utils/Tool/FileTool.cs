using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Utils.Tool
{
    /// <summary>
    /// 文件操作工具类
    /// </summary>
    public static class FileTool
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">要删除的文件名</param>
        /// <param name="isSendToRecycleBin">是否删除到回收站</param>
        public static void Delete(string fileName, bool isSendToRecycleBin)
        {
            if (isSendToRecycleBin)
            {
                FileSystem.DeleteFile(fileName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            }
            else
            {
                File.Delete(fileName);
            }
        }

        /// <summary>
        /// 将文件移动到指定目录
        /// </summary>
        /// <param name="fileName">源文件名</param>
        /// <param name="targetDirectory">目标目录</param>
        /// <param name="isCover">是否覆盖</param>
        public static bool Move(string fileName, string targetDirectory, bool isCover)
        {
            FileInfo fileInfo = new(fileName);
            return Move(fileInfo, targetDirectory, isCover);
        }

        /// <summary>
        /// 将文件移动到指定目录
        /// </summary>
        /// <param name="fileInfo">源文件</param>
        /// <param name="targetDirectory">目标目录</param>
        /// <param name="isCover">是否覆盖</param>
        public static bool Move(FileInfo fileInfo, string targetDirectory, bool isCover)
        {
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }
            string fullname = targetDirectory + "\\" + fileInfo.Name;
            if (File.Exists(fullname))
            {
                if (isCover)
                {
                    File.Delete(fullname);
                }
                else
                {
                    return false;
                }
            }
            File.Move(fileInfo.FullName, fullname);
            return true;
        }

        /// <summary>
        /// 将指定文件复制到新位置
        /// </summary>
        /// <param name="fileName">源文件名</param>
        /// <param name="targetFileName">新文件名</param>
        /// <param name="isCover">是否覆盖</param>
        public static void Copy(string fileName, string targetFileName, bool isCover)
        {
            FileInfo fileInfo = new(fileName);
            Copy(fileInfo, targetFileName, isCover);
        }

        /// <summary>
        /// 将指定文件复制到新位置
        /// </summary>
        /// <param name="fileInfo">源文件</param>
        /// <param name="targetFileName">新文件名</param>
        /// <param name="isCover">是否覆盖</param>
        public static void Copy(FileInfo fileInfo, string targetFileName, bool isCover)
        {
            if (isCover)
            {
                Delete(targetFileName, false);
            }
            string? destDir = Path.GetDirectoryName(targetFileName);
            if (destDir != null && !Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }
            fileInfo.CopyTo(targetFileName);
        }

        /// <summary>
        /// 设置或取消文件的指定<see cref="FileAttributes"/>属性
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="attribute">要设置的文件属性</param>
        /// <param name="isSet">true为设置，false为取消</param>
        public static void SetAttribute(string fileName, FileAttributes attribute, bool isSet)
        {
            FileInfo fi = new(fileName);
            if (!fi.Exists)
            {
                throw new FileNotFoundException("要设置属性的文件不存在。", fileName);
            }
            if (isSet)
            {
                fi.Attributes |= attribute;
            }
            else
            {
                fi.Attributes &= ~attribute;
            }
        }

        /// <summary>
        /// 获取文件版本号
        /// </summary>
        /// <param name="fileName"> 完整文件名 </param>
        /// <returns> 文件版本号 </returns>
        public static string? GetVersion(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(fileName);
                return fvi.FileVersion;
            }
            return null;
        }

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="fileName"> 文件名 </param>
        /// <returns> 32位MD5 </returns>
        public static string GetMD5(string fileName)
        {
            FileStream fs = new(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            const int bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize];

            MD5 md5 =  MD5.Create();
            md5.Initialize();

            long offset = 0;
            while (offset < fs.Length)
            {
                long readSize = bufferSize;
                if (offset + readSize > fs.Length)
                {
                    readSize = fs.Length - offset;
                }
                fs.Read(buffer, 0, (int)readSize);
                if (offset + readSize < fs.Length)
                {
                    md5.TransformBlock(buffer, 0, (int)readSize, buffer, 0);
                }
                else
                {
                    md5.TransformFinalBlock(buffer, 0, (int)readSize);
                }
                offset += bufferSize;
            }
            fs.Close();

            if (md5.Hash != null)
            {
                byte[] result = md5.Hash;
                md5.Clear();
                StringBuilder sb = new(32);
                foreach (byte b in result)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取文本文件的编码方式
        /// </summary>
        public static Encoding GetFileEncoding(string fileName)
        {
            using FileStream fs = new(fileName, FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new(fs);
            byte[] buffer = reader.ReadBytes(2);
            if (buffer[0] >= 0xEF)
            {
                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                {
                    return Encoding.UTF8;
                }
                if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                {
                    return Encoding.BigEndianUnicode;
                }
                if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                {
                    return Encoding.Unicode;
                }
                return Encoding.Default;
            }
            return Encoding.Default;
        }

        /// <summary>
        /// 判断文件是否被占用
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool FileIsReady(string fileName)
        {
            FileInfo fileInfo = new(fileName);
            return FileIsReady(fileInfo);
        }

        /// <summary>
        /// 判断文件是否被占用
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static bool FileIsReady(FileInfo fileInfo)
        {
            FileStream? fs = null;
            try
            {
                fs = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// 判断文件是否被占用
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="sleep">重试间隔（ms）</param>
        /// <returns></returns>
        public static bool FileIsReady(string fileName, int retryCount, int sleep = 1000)
        {
            int index = 0;
            while (index <= retryCount)
            {
                index++;

                FileInfo fi = new(fileName);
                FileStream? fs = null;
                try
                {
                    fs = fi.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                    return true;
                }
                catch (IOException)
                {
                    if (index < retryCount) // 还会继续检测，则休息
                    {
                        Thread.Sleep(sleep);
                    }
                    continue;
                }
                finally
                {
                    fs?.Close();
                }
            }
            return false;
        }

        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="newName">新文件名(不包含路径和扩展名)</param>
        /// <param name="isCover">是否覆盖</param>
        public static bool RenameFile(string fileName, string newName, bool isCover)
        {
            string fileNew = Path.GetDirectoryName(fileName) + "\\" + newName + Path.GetExtension(fileName);
            if (File.Exists(fileNew))
            {
                if (isCover)
                {
                    Delete(fileNew, false);
                }
                else
                {
                    return false;
                }                
            }
            File.Move(fileName, fileNew);
            return true;
        }
    }
}