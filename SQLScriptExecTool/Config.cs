using Newtonsoft.Json;
using System.IO;


namespace SQLScriptExecTool
{
    public class Config
    {
        public static readonly string fileName = "Config.json";

        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = Load();
                    }
                    catch
                    {
                        _instance = new Config();
                        _instance.Save();
                    }
                }
                return _instance;
            }
        }

        private static Config _instance = null;

        #region Parameter

        public string Server { get; set; } = "localhost";

        public string Port { get; set; } = "3306";

        public string User { get; set; }

        public string Password { get; set; }

        #endregion

        public static Config Load()
        {
            string jsonStr = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Config>(jsonStr);
        }

        public void Save()
        {
            string jsonStr = JsonConvert.SerializeObject(this);
            File.WriteAllText(fileName, jsonStr);
        }
    }
}

