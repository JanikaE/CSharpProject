using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BackupTool.Config
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

        private Config()
        {
            PathPairs = new List<PathPair>();
        }

        #region Parameter

        public List<PathPair> PathPairs { get; set; }

        public bool IsShowIgnore { get; set; }

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

