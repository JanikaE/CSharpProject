using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using UsefulTools.Tabs;

namespace UsefulTools.Helpers
{
    /// <summary>
    /// 批量解压并发模式。
    /// </summary>
    public enum ExtractParallelMode
    {
        /// <summary>自动：按目标盘类型决定是否并行（机械盘串行，SSD 等并行）。</summary>
        Auto = 0,

        /// <summary>单包：始终只解压一个压缩包。</summary>
        Single = 1,

        /// <summary>并行：按 CPU 核数并行解压多个压缩包。</summary>
        Parallel = 2,
    }

    /// <summary>
    /// 压缩包解压服务：批量解压多个 zip 压缩包，按要求选择串行或受控并行，并逐包汇报进度。
    /// </summary>
    public static class ExtractService
    {
        /// <summary>
        /// 执行批量解压。
        /// </summary>
        public static void Execute(
            IEnumerable<ExtractItem> items,
            string targetFolder,
            bool createNamedSubfolder,
            ExtractParallelMode mode,
            bool deleteArchiveAfterExtraction,
            Action<ExtractItem> onStarted,
            Action<ExtractItem, double> onProgress,
            Action<ExtractItem, ExtractState, string> onCompleted)
        {
            var list = items as List<ExtractItem> ?? items.ToList();
            if (list.Count == 0)
            {
                return;
            }

            int degree = ResolveDegree(mode, targetFolder, list.Count);

            ParallelOptions options = new()
            {
                MaxDegreeOfParallelism = degree,
            };

            Parallel.ForEach(list, options, item =>
                ExtractOne(item, targetFolder, createNamedSubfolder, deleteArchiveAfterExtraction, onStarted, onProgress, onCompleted));
        }

        /// <summary>
        /// 根据并发模式与目标盘类型计算实际并发度。
        /// </summary>
        private static int ResolveDegree(ExtractParallelMode mode, string targetFolder, int itemCount)
        {
            int cores = Math.Max(1, Environment.ProcessorCount);
            int parallel = Math.Min(cores, Math.Max(1, itemCount));

            switch (mode)
            {
                case ExtractParallelMode.Single:
                    return 1;

                case ExtractParallelMode.Parallel:
                    return parallel;

                case ExtractParallelMode.Auto:
                default:
                    // 自动：机械盘存在寻道惩罚，串行更稳；SSD 等并行可提速；判定失败时回退并行
                    bool? mechanical = DiskTypeHelper.IsMechanicalDisk(targetFolder);
                    return mechanical == true ? 1 : parallel;
            }
        }

        private static void ExtractOne(
            ExtractItem item,
            string targetFolder,
            bool createNamedSubfolder,
            bool deleteArchiveAfterExtraction,
            Action<ExtractItem> onStarted,
            Action<ExtractItem, double> onProgress,
            Action<ExtractItem, ExtractState, string> onCompleted)
        {
            onStarted?.Invoke(item);
            try
            {
                if (string.IsNullOrWhiteSpace(item.FilePath) || !File.Exists(item.FilePath))
                {
                    throw new FileNotFoundException("压缩包不存在或无法访问。", item.FilePath);
                }

                // 计算本次解压的目标根目录
                string destRoot = targetFolder;
                if (createNamedSubfolder)
                {
                    destRoot = Path.Combine(targetFolder, Path.GetFileNameWithoutExtension(item.FilePath));
                }
                Directory.CreateDirectory(destRoot);

                string rootPrefix = Path.GetFullPath(destRoot)
                    .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    + Path.DirectorySeparatorChar;

                bool skippedUnsafeEntry = false;
                using (var archive = ZipFile.OpenRead(item.FilePath))
                {
                    int total = archive.Entries.Count;
                    int done = 0;

                    foreach (var entry in archive.Entries)
                    {
                        string entryFull = Path.GetFullPath(Path.Combine(destRoot, entry.FullName));

                        // 防御路径穿越：确保解压目标始终位于目标根目录之内
                        if (!entryFull.StartsWith(rootPrefix, StringComparison.OrdinalIgnoreCase))
                        {
                            skippedUnsafeEntry = true;
                            continue;
                        }

                        if (string.IsNullOrEmpty(entry.Name))
                        {
                            // 目录项
                            Directory.CreateDirectory(entryFull);
                        }
                        else
                        {
                            string parent = Path.GetDirectoryName(entryFull);
                            if (!string.IsNullOrEmpty(parent))
                            {
                                Directory.CreateDirectory(parent);
                            }
                            entry.ExtractToFile(entryFull, overwrite: true);
                        }

                        done++;
                        onProgress?.Invoke(item, total > 0 ? (double)done / total * 100 : 100);
                    }
                }

                string message = $"已解压到 {destRoot}";
                if (skippedUnsafeEntry)
                {
                    message += "；已跳过不安全路径条目，保留原压缩包";
                }
                else if (deleteArchiveAfterExtraction)
                {
                    // 压缩包句柄已释放，才可移至回收站。
                    try
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                            item.FilePath,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                            Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin,
                            Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                        message += "；压缩包已移至回收站";
                    }
                    catch (Exception ex)
                    {
                        message += $"；压缩包移至回收站失败：{ex.Message}";
                    }
                }
                onCompleted?.Invoke(item, ExtractState.Completed, message);
            }
            catch (Exception ex)
            {
                onCompleted?.Invoke(item, ExtractState.Failed, ex.Message);
            }
        }
    }
}