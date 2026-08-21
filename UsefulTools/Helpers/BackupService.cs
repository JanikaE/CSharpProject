using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class BackupResultItem : INotifyPropertyChanged
    {
        private string _message;
        private BackupResultType _type;

        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
                }
            }
        }

        public BackupResultType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => Message;
    }

    public enum BackupResultType
    {
        Start,
        End,
        CreateDirectory,
        Copying,
        New,
        Overwrite,
        Ignore,
        DirectoryNotFound,
        Error
    }

    /// <summary>
    /// 文件备份服务，逻辑移植自 FileManager 的 FormExecBackup。
    /// 该方法在调用线程上执行，逐条通过回调产出结果（可能为后台线程），
    /// 大文件复制时先产出"复制中"项，完成后通过 onUpdate 原地更新为"覆盖/新增"。
    /// 文件/目录结果仅显示相对配置根路径的相对路径。
    /// </summary>
    public static class BackupService
    {
        public static void Execute(
            IEnumerable<PathPair> pathPairs,
            bool isShowIgnore,
            Action<BackupResultItem> onResult,
            Action<BackupResultItem, BackupResultType, string> onUpdate)
        {
            foreach (PathPair pathPair in pathPairs)
            {
                switch ((BackupPolicy)pathPair.Policy)
                {
                    case BackupPolicy.Overwrite:
                        onResult(Item(BackupResultType.Start, $"{pathPair.SourcePath} ==> {pathPair.TargetPath}"));
                        Copy(pathPair.SourcePath, pathPair.TargetPath, pathPair.SourcePath, isShowIgnore, onResult, onUpdate);
                        onResult(Item(BackupResultType.End, $"{pathPair.SourcePath} ==> {pathPair.TargetPath}"));
                        break;

                    case BackupPolicy.Append:
                        string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string targetPath = $"{pathPair.TargetPath}_{timeStamp}";
                        onResult(Item(BackupResultType.Start, $"{pathPair.SourcePath} ==> {targetPath}"));
                        Copy(pathPair.SourcePath, targetPath, pathPair.SourcePath, isShowIgnore, onResult, onUpdate);
                        onResult(Item(BackupResultType.End, $"{pathPair.SourcePath} ==> {targetPath}"));
                        break;
                }
            }
        }

        private static void Copy(
            string sourcePath,
            string targetPath,
            string sourceRoot,
            bool isShowIgnore,
            Action<BackupResultItem> onResult,
            Action<BackupResultItem, BackupResultType, string> onUpdate)
        {
            if (!Directory.Exists(sourcePath))
            {
                onResult(Item(BackupResultType.DirectoryNotFound, $"目录不存在:{sourcePath}"));
                return;
            }
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                onResult(Item(BackupResultType.CreateDirectory, $"创建目录:{Path.GetRelativePath(sourceRoot, sourcePath)}"));
            }

            string[] dirs = Directory.GetDirectories(sourcePath);
            foreach (string dir in dirs)
            {
                string childTarget = Path.Combine(targetPath, Path.GetFileName(dir));
                Copy(dir, childTarget, sourceRoot, isShowIgnore, onResult, onUpdate);
            }

            string[] files = Directory.GetFiles(sourcePath);
            foreach (string sourceFile in files)
            {
                string targetFile = Path.Combine(targetPath, Path.GetFileName(sourceFile));
                string relativePath = Path.GetRelativePath(sourceRoot, sourceFile);
                try
                {
                    if (File.Exists(targetFile))
                    {
                        FileInfo target = new(targetFile);
                        FileInfo source = new(sourceFile);
                        if (target.LastWriteTime < source.LastWriteTime)
                        {
                            BackupResultItem item = Item(BackupResultType.Copying, $"复制中:{relativePath}");
                            onResult(item);
                            File.Delete(targetFile);
                            File.Copy(sourceFile, targetFile);
                            onUpdate(item, BackupResultType.Overwrite, $"覆盖:{relativePath}");
                        }
                        else
                        {
                            if (isShowIgnore)
                            {
                                onResult(Item(BackupResultType.Ignore, $"忽略:{relativePath}"));
                            }
                        }
                    }
                    else
                    {
                        BackupResultItem item = Item(BackupResultType.Copying, $"复制中:{relativePath}");
                        onResult(item);
                        File.Copy(sourceFile, targetFile);
                        onUpdate(item, BackupResultType.New, $"新增:{relativePath}");
                    }
                }
                catch (Exception ex)
                {
                    onResult(Item(BackupResultType.Error, $"错误:{relativePath},{ex.Message}"));
                }
            }
        }

        private static BackupResultItem Item(BackupResultType type, string message)
        {
            return new BackupResultItem { Type = type, Message = message };
        }
    }
}