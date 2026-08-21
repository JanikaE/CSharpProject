using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace UsefulTools.Helpers
{
    /// <summary>
    /// 文件备份配置，存储于程序目录下的 backup.json
    /// </summary>
    public class BackupConfig
    {
        private static readonly object _lock = new();
        private static BackupConfig _instance;
        private static string _filePath;

        public static string FilePath
        {
            get
            {
                _filePath ??= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup.json");
                return _filePath;
            }
        }

        public static BackupConfig Instance
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

        public List<PathPair> PathPairs { get; set; } = [];

        public bool IsShowIgnore { get; set; }

        public void Save()
        {
            lock (_lock)
            {
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
        }

        private static BackupConfig Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    return JsonSerializer.Deserialize<BackupConfig>(json) ?? new BackupConfig();
                }
            }
            catch (Exception)
            {
                // 配置损坏时回退到默认
            }
            var config = new BackupConfig();
            config.Save();
            return config;
        }
    }
}
