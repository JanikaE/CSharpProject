using Utils.Config;


namespace SQLScriptExecTool
{
    public class Config : BaseConfig<Config>
    {
        public string Server { get; set; } = "localhost";

        public string Port { get; set; } = "3306";

        public string User { get; set; }

        public string Password { get; set; }

        public string CurrentVersion { get; set; }

        public string TargetVertion { get; set; }

        public string Directory { get; set; }
    }
}

