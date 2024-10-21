namespace BackupTool.Config
{
    public class PathPair
    {
        public string Name { get; set; }

        public string SourcePath { get; set; }

        public string TargetPath { get; set; }

        public override string ToString()
        {
            return $"Name:{Name}\nSource:{SourcePath}\nTarget:{TargetPath}";
        }
    }
}
