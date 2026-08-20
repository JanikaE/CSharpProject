using System;
using System.Collections.Generic;
using System.IO;

namespace UsefulTools.Helpers
{
    public enum BackupPolicy
    {
        /// <summary>
        /// 覆盖
        /// </summary>
        Overwrite,

        /// <summary>
        /// 追加
        /// </summary>
        Append
    }

    /// <summary>
    /// 备份过程中的一条结果记录
    /// </summary>
    public class BackupResultItem
    {
        public string Message { get; set; }

        public BackupResultType Type { get; set; }

        public override string ToString() => Message;
    }

    public enum BackupResultType
    {
        Start,
        End,
        CreateDirectory,
        New,
        Overwrite,
        Ignore,
        DirectoryNotFound,
        Error
    }

    /// <summary>
    /// 文件备份服务
    /// </summary>
    public static class BackupService
    {
        public static List<BackupResultItem> Execute(IEnumerable<PathPair> pathPairs, int policy, bool isShowIgnore)
        {
            var results = new List<BackupResultItem>();
            foreach (PathPair pathPair in pathPairs)
            {
                switch ((BackupPolicy)policy)
                {
                    case BackupPolicy.Overwrite:
                        results.Add(Item(BackupResultType.Start, $"{pathPair.SourcePath} ==> {pathPair.TargetPath}"));
                        Copy(pathPair.SourcePath, pathPair.TargetPath, isShowIgnore, results);
                        results.Add(Item(BackupResultType.End, $"{pathPair.SourcePath} ==> {pathPair.TargetPath}"));
                        break;

                    case BackupPolicy.Append:
                        string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string targetPath = $"{pathPair.TargetPath}_{timeStamp}";
                        results.Add(Item(BackupResultType.Start, $"{pathPair.SourcePath} ==> {targetPath}"));
                        Copy(pathPair.SourcePath, targetPath, isShowIgnore, results);
                        results.Add(Item(BackupResultType.End, $"{pathPair.SourcePath} ==> {targetPath}"));
                        break;
                }
            }
            return results;
        }

        private static void Copy(string sourcePath, string targetPath, bool isShowIgnore, List<BackupResultItem> results)
        {
            if (!Directory.Exists(sourcePath))
            {
                results.Add(Item(BackupResultType.DirectoryNotFound, $"目录不存在:{sourcePath}"));
                return;
            }
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                results.Add(Item(BackupResultType.CreateDirectory, $"创建目录:{targetPath}"));
            }

            string[] dirs = Directory.GetDirectories(sourcePath);
            foreach (string dir in dirs)
            {
                string childTarget = Path.Combine(targetPath, Path.GetFileName(dir));
                Copy(dir, childTarget, isShowIgnore, results);
            }

            string[] files = Directory.GetFiles(sourcePath);
            foreach (string sourceFile in files)
            {
                string targetFile = Path.Combine(targetPath, Path.GetFileName(sourceFile));
                string msg = $"{sourceFile} ==> {targetFile}";
                try
                {
                    if (File.Exists(targetFile))
                    {
                        FileInfo target = new(targetFile);
                        FileInfo source = new(sourceFile);
                        if (target.LastWriteTime < source.LastWriteTime)
                        {
                            File.Delete(targetFile);
                            File.Copy(sourceFile, targetFile);
                            results.Add(Item(BackupResultType.Overwrite, $"覆盖:{msg}"));
                        }
                        else
                        {
                            if (isShowIgnore)
                            {
                                results.Add(Item(BackupResultType.Ignore, $"忽略:{msg}"));
                            }
                        }
                    }
                    else
                    {
                        File.Copy(sourceFile, targetFile);
                        results.Add(Item(BackupResultType.New, $"新增:{msg}"));
                    }
                }
                catch (Exception ex)
                {
                    results.Add(Item(BackupResultType.Error, $"错误:{msg},{ex.Message}"));
                }
            }
        }

        private static BackupResultItem Item(BackupResultType type, string message)
        {
            return new BackupResultItem { Type = type, Message = message };
        }
    }
}
