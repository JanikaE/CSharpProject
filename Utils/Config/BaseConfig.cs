using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace Utils.Config
{
    public abstract class BaseConfig<T> where T : BaseConfig<T>, new()
    {
        protected virtual string FileName { get; }
        private static readonly Lazy<T> _lazy = new(() => LoadCore());

        public static T Instance => _lazy.Value;

        private static T LoadCore()
        {
            string path = GetFullPath(GetFileName());
            try
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (JsonException) { }
            // 返回默认并保存
            var config = new T();
            config.Save();
            return config;
        }

        public virtual void Save()
        {
            string path = GetFullPath(GetFileName());
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        private static string GetFullPath(string fileName) =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        private static string GetFileName() =>
            (typeof(T).GetProperty("FileName", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) as string)
            ?? "Config.json";
    }
}