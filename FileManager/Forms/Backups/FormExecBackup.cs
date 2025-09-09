using FileManager.Configs;
using System;
using System.Drawing;
using System.IO;

namespace FileManager.Forms.Backups
{
    public class FormExecBackup : FormExec
    {
        public FormExecBackup() : base()
        {
            InitTag();
            SetRectangle();
        }

        public override void Execute()
        {
            foreach (PathPair pathPair in Config.Instance.PathPairs)
            {
                WriteProcess($"Start:{pathPair.SourcePath} ==> {pathPair.TargetPath}\n", Color.Black);
                Copy(pathPair.SourcePath, pathPair.TargetPath);
                WriteProcess($"End:{pathPair.SourcePath} ==> {pathPair.TargetPath}\n\n", Color.Black);
            }
            base.Execute();
        }

        private void Copy(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(sourcePath))
            {
                WriteProcess($"DirectoryNotFound:{sourcePath}\n\n", Color.Red);
            }
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                WriteProcess($"CreateDirectory:{targetPath}\n", Color.Yellow);
            }
            string[] dirs = Directory.GetDirectories(sourcePath);
            if (dirs.Length > 0)
            {
                foreach (string dir in dirs)
                {
                    Copy(dir, targetPath + dir.Substring(dir.LastIndexOf('\\')));
                }
            }

            string[] files = Directory.GetFiles(sourcePath);
            if (files.Length > 0)
            {
                foreach (string sourceFile in files)
                {
                    string targetFile = targetPath + sourceFile.Substring(sourceFile.LastIndexOf('\\'));
                    FileInfo source = new(sourceFile);
                    string msg = $"{sourceFile} ==> {targetFile}";
                    try
                    {
                        if (File.Exists(targetFile))
                        {
                            FileInfo target = new(targetFile);
                            if (target.LastWriteTime < source.LastWriteTime)
                            {
                                File.Delete(targetFile);
                                File.Copy(sourceFile, targetFile);
                                WriteProcess($"Overwrite:{msg}\n", Color.Blue);
                            }
                            else
                            {
                                if (Config.Instance.IsShowIgnore)
                                {
                                    WriteProcess($"Ignore:{msg}\n", Color.Gray);
                                }
                            }
                        }
                        else
                        {
                            File.Copy(sourceFile, targetFile);
                            WriteProcess($"New:{msg}\n", Color.Green);
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteProcess($"Error:{msg},{ex.Message}\n", Color.Red);
                    }
                }
            }
        }
    }
}
