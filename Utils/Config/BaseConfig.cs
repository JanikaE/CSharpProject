using Newtonsoft.Json;
using System;
using System.IO;

namespace Utils.Config
{
    public class BaseConfig
    {
        protected static readonly string DefaultFileName = "Config.json";
        protected virtual string FileName => DefaultFileName;

        public static T Load<T>() where T : BaseConfig, new()
        {
            try
            {
                string jsonStr = File.ReadAllText(GetFileName(typeof(T)));
                return JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (FileNotFoundException)
            {
                // 如果文件不存在，创建默认配置
                var config = new T();
                config.Save();
                return config;
            }
            catch (Exception)
            {
                return new T();
            }
        }

        public virtual void Save()
        {
            string jsonStr = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(GetFileName(GetType()), jsonStr);
        }

        protected static string GetFileName(Type configType)
        {
            // 如果类型有自定义文件名属性，使用它
            var fileNameProperty = configType.GetProperty("FileName");
            if (fileNameProperty != null && fileNameProperty.CanRead)
            {
                return fileNameProperty.GetValue(null) as string ?? DefaultFileName;
            }

            return DefaultFileName;
        }
    }
}