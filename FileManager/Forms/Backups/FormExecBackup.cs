using FileManager.Configs;
using FileManager.Configs.Enum;
using System;
using System.Collections.Generic;
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

        public override void Execute(object param)
        {
            if (param is List<PathPair> pathPairs)
            {
                foreach (PathPair pathPair in pathPairs)
                {
                    switch (Config.Instance.Policy)
                    {
                        case (int)BackupPolicy.Overwrite:
                            WriteProcess($"Start:{pathPair.SourcePath} ==> {pathPair.TargetPath}\n", Color.Black);
                            Copy(pathPair.SourcePath, pathPair.TargetPath);
                            WriteProcess($"End:{pathPair.SourcePath} ==> {pathPair.TargetPath}\n\n", Color.Black);
                            break;

                        case (int)BackupPolicy.Append:
                            string timeStamp = DateTime.Now.ToString("yyyyMMddhhmmss");
                            string targetPath = $"{pathPair.TargetPath}_{timeStamp}";
                            WriteProcess($"Start:{pathPair.SourcePath} ==> {targetPath}\n", Color.Black);
                            Copy(pathPair.SourcePath, targetPath);
                            WriteProcess($"End:{pathPair.SourcePath} ==> {targetPath}\n\n", Color.Black);
                            break;
                    }
                }
                base.Execute(param);
            }
        }

        private void Copy(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(sourcePath))
            {
                WriteProcess($"DirectoryNotFound:{sourcePath}\n\n", Color.Red);
                return;
            }
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                WriteProcess($"CreateDirectory:{targetPath}\n", Color.Purple);
            }
            string[] dirs = Directory.GetDirectories(sourcePath);
            if (dirs.Length > 0)
            {
                foreach (string dir in dirs)
                {
                    Copy(dir, string.Concat(targetPath, dir.AsSpan(dir.LastIndexOf('\\'))));
                }
            }

            string[] files = Directory.GetFiles(sourcePath);
            if (files.Length > 0)
            {
                foreach (string sourceFile in files)
                {
                    string targetFile = string.Concat(targetPath, sourceFile.AsSpan(sourceFile.LastIndexOf('\\')));
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
