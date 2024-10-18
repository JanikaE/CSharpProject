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

        #region Parameter

        public List<PathPair> PathPairs { get; set; }

        #region 新增/修改/删除

        public bool AddPathPair(PathPair pathPair)
        {
            int id = 1;
            while (PathPairs.FindIndex(p => p.Id == id) > 0)
            {
                id++;
            }
            pathPair.Id = id;
            PathPairs.Add(pathPair);
            return true;
        }

        public bool EditPathPair(PathPair pathPair)
        {
            PathPair origin = PathPairs.Find(p => p.Id == pathPair.Id);
            if (origin != null)
            {
                origin = pathPair;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletePair(PathPair pathPair)
        {
            PathPair origin = PathPairs.Find(p => p.Id == pathPair.Id);
            if (origin != null)
            {
                PathPairs.Remove(origin);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

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

