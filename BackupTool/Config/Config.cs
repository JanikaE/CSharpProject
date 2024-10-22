using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
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
                if (_instance.PathPairs == null)
                    _instance.PathPairs = new List<PathPair>();
                if (_instance.FormRectangle == null)
                    _instance.FormRectangle = new Dictionary<string, Rectangle>();
                return _instance;
            }
        }

        private static Config _instance = null;

        #region Parameter

        public List<PathPair> PathPairs { get; set; }

        public bool IsShowIgnore { get; set; }

        public Dictionary<string, Rectangle> FormRectangle { get; set; }

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

