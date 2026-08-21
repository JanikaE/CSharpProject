namespace UsefulTools.Helpers
{
    public class PathPair
    {
        public string Name { get; set; }

        public string SourcePath { get; set; }

        public string TargetPath { get; set; }

        /// <summary>
        /// 备份策略 0：覆盖；1：追加（对应 BackupPolicy）
        /// </summary>
        public int Policy { get; set; }

        public override string ToString()
        {
            return $"Name:{Name}\nSource:{SourcePath}\nTarget:{TargetPath}";
        }
    }
}