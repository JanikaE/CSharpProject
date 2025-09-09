namespace FileManager.Configs
{
    public class DeletePattern
    {
        /// <summary>
        /// 源路径
        /// </summary>
        public string SourcePath { get; set; }

        /// <summary>
        /// 匹配文件
        /// </summary>
        public string MatchFile { get; set; }

        /// <summary>
        /// 匹配文件夹
        /// </summary>
        public string MatchFolder { get; set; }

        /// <summary>
        /// 是否完全匹配
        /// </summary>
        public bool IsFullMatch { get; set; } = true;

        /// <summary>
        /// 是否忽略大小写
        /// </summary>
        public bool IsIgnoreCase { get; set; } = true;
    }
}
