using System.Collections.Generic;
using System.Drawing;
using Utils.Config;

namespace FileManager.Configs
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

        #region Backup

        public List<PathPair> PathPairs { get; set; } = [];

        public bool IsShowIgnore { get; set; } = false;

        /// <summary>
        /// 备份策略  0：覆盖；1：追加
        /// </summary>
        public int Policy { get; set; } = 0;

        #endregion

        #region Delete

        public List<DeletePattern> DeletePatterns = [];

        #endregion

        public Dictionary<string, Rectangle> FormRectangle { get; set; } = [];

        #endregion
    }
}

