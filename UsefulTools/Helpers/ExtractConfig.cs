using System;
using System.IO;
using System.Text.Json;

namespace UsefulTools.Helpers
{
    /// <summary>
    /// 文件解压配置，存储于程序目录下的 extract.json
    /// </summary>
    public class ExtractConfig
    {
        private static readonly object _lock = new();
        private static ExtractConfig _instance;
        private static string _filePath;

        public static string FilePath
        {
            get
            {
                _filePath ??= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "extract.json");
                return _filePath;
            }
        }

        public static ExtractConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= Load();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 是否解压到与压缩包同名的子文件夹，默认为启用。
        /// </summary>
        public bool CreateNamedSubfolder { get; set; } = true;

        /// <summary>成功解压后将原压缩包移至回收站，默认关闭。</summary>
        public bool DeleteArchiveAfterExtraction { get; set; }

        /// <summary>
        /// 并发模式：0=自动（按磁盘类型），1=单包顺序，2=多包并行。默认自动。
        /// </summary>
        public int ParallelMode { get; set; }

        public void Save()
        {
            lock (_lock)
            {
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
        }

        private static ExtractConfig Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    // 反序列化结果可能为 null；若缺省属性则保留默认值
                    return JsonSerializer.Deserialize<ExtractConfig>(json) ?? new ExtractConfig();
                }
            }
            catch (Exception)
            {
                // 配置损坏时回退到默认
            }
            var config = new ExtractConfig();
            config.Save();
            return config;
        }
    }
}