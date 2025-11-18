using System;
using System.IO;
using System.Linq;

namespace Utils.Tool
{
    /// <summary>
    /// 目录操作工具类
    /// </summary>
    public static class DirectoryTool
    {
        /// <summary>
        /// 递归复制文件夹及文件夹/文件
        /// </summary>
        /// <param name="sourcePath">源文件夹路径</param>
        /// <param name="targetPath">目的文件夹路径</param>
        /// <param name="searchPatterns">要复制的文件扩展名数组</param>
        public static void Copy(string sourcePath, string targetPath, bool isCover, string[] searchPatterns = null, string[] skipDir = null, string[] skipFile = null)
        {
            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException("递归复制文件夹时源目录不存在。");
            }
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            string[] dirs = Directory.GetDirectories(sourcePath);
            if (dirs.Length > 0)
            {
                foreach (string dir in dirs)
                {
                    if (skipDir != null && skipDir.Any(c => c == dir))
                        continue;
                    Copy(dir, string.Concat(targetPath, dir.AsSpan(dir.LastIndexOf("\\", StringComparison.Ordinal))), isCover);
                }
            }

            if (searchPatterns != null && searchPatterns.Length > 0)
            {
                foreach (string searchPattern in searchPatterns)
                {
                    string[] files = Directory.GetFiles(sourcePath, searchPattern);
                    if (files.Length <= 0)
                    {
                        continue;
                    }
                    foreach (string file in files)
                    {
                        if (skipFile != null && skipFile.Any(c => c == file))
                            continue;
                        FileTool.Copy(file, string.Concat(targetPath, file.AsSpan(file.LastIndexOf("\\", StringComparison.Ordinal))), isCover);
                    }
                }
            }
            else
            {
                string[] files = Directory.GetFiles(sourcePath);
                if (files.Length <= 0)
                {
                    return;
                }
                foreach (string file in files)
                {
                    if (skipFile != null && skipFile.Any(c => c == file))
                        continue;
                    FileTool.Copy(file, string.Concat(targetPath, file.AsSpan(file.LastIndexOf("\\", StringComparison.Ordinal))), isCover);
                }
            }
        }

        /// <summary>
        /// 递归删除目录
        /// </summary>
        /// <param name="directory">目录路径</param>
        /// <param name="isDeleteRoot">是否删除根目录</param>
        /// <returns>删除的文件大小（字节）</returns>
        public static long Delete(string directory, bool isDeleteRoot = true)
        {
            long temp = 0;
            DirectoryInfo dirPathInfo = new(directory);
            if (dirPathInfo.Exists)
            {
                //删除目录下所有文件
                foreach (FileInfo fileInfo in dirPathInfo.GetFiles())
                {
                    temp += fileInfo.Length;
                    fileInfo.Delete();
                }
                //递归删除所有子目录
                foreach (DirectoryInfo subDirectory in dirPathInfo.GetDirectories())
                {
                    temp += Delete(subDirectory.FullName, true);
                }
                //删除目录
                if (isDeleteRoot)
                {
                    dirPathInfo.Attributes = FileAttributes.Normal;
                    dirPathInfo.Delete();
                }
            }
            return temp;
        }

        /// <summary>
        /// 删除空文件夹
        /// </summary>
        /// <param name="rootPath">目录路径</param>
        /// <param name="isDeleteRoot">是否删除根目录</param>
        public static void DeleteEmptyFolders(string rootPath, bool isDeleteRoot)
        {
            if (Directory.Exists(rootPath))
            {
                foreach (string item in Directory.GetFileSystemEntries(rootPath, "*", SearchOption.AllDirectories))
                {
                    //如果是文件夹
                    if (!File.Exists(item))
                    {
                        //如果文件夹存在
                        if (Directory.Exists(item))
                        {
                            //取得文件夹下所有文件
                            string[] nFiles = Directory.GetFiles(item, "*.*", SearchOption.AllDirectories);
                            //如果文件个数为0则删除目录
                            if (nFiles.Length <= 0)
                            {
                                Directory.Delete(item);
                            }
                        }
                    }
                }
            }

            if (isDeleteRoot)
            {
                if (Directory.GetFileSystemEntries(rootPath, "*", SearchOption.AllDirectories).Length <= 0)
                {
                    Directory.Delete(rootPath);
                }
            }
        }

        /// <summary>
        /// 根据文件路径创建其目录
        /// </summary>
        /// <param name="fileFullPath"></param>
        public static void CreateDirectoryByFilePath(string fileFullPath)
        {
            string dirPath = Path.GetDirectoryName(fileFullPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath ?? string.Empty);
            }
        }

        /// <summary>
        /// 设置或取消目录的<see cref="FileAttributes"/>属性。
        /// </summary>
        /// <param name="directory">目录路径</param>
        /// <param name="attribute">要设置的目录属性</param>
        /// <param name="isSet">true为设置，false为取消</param>
        public static void SetAttributes(string directory, FileAttributes attribute, bool isSet)
        {
            DirectoryInfo di = new(directory);
            if (!di.Exists)
            {
                throw new DirectoryNotFoundException("设置目录属性时指定文件夹不存在");
            }
            if (isSet)
            {
                di.Attributes |= attribute;
            }
            else
            {
                di.Attributes &= ~attribute;
            }
        }
    }
}