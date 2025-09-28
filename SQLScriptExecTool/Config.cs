using Newtonsoft.Json;
using Utils.Config;


namespace SQLScriptExecTool
{
    public class Config : BaseConfig
    {
        private static Config _instance = null;
        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = Load<Config>();
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

        #region Parameter

        public string Server { get; set; } = "localhost";

        public string Port { get; set; } = "3306";

        public string User { get; set; }

        public string Password { get; set; }

        public string CurrentVersion { get; set; }

        public string TargetVertion { get; set; }

        public string Directory { get; set; }

        #endregion
    }
}

