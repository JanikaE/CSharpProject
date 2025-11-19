using FileManager.Configs;
using System;
using System.Drawing;
using System.IO;
using Utils.Extend;

namespace FileManager.Forms.Deletes
{
    public class FormExecDelete : FormExec
    {
        public DeletePattern DeletePattern { get; set; }

        public FormExecDelete() : base()
        {
            InitTag();
            SetRectangle();
        }

        public override void Execute(object param)
        {
            if (DeletePattern != null)
            {
                WriteProcess($"Start:{DeletePattern.SourcePath}\n", Color.Black);
                Delete(DeletePattern.SourcePath);
                WriteProcess($"End:{DeletePattern.SourcePath}\n\n", Color.Black);
            }
            else
            {
                WriteProcess("DeletePattern is null!", Color.Red);
            }
            base.Execute(param);
        }

        private void Delete(string path)
        {
            if (!Directory.Exists(path))
            {
                WriteProcess($"DirectoryNotFound:{DeletePattern.SourcePath}\n\n", Color.Red);
                return;
            }

            // 删除当前目录下的匹配文件
            if (!DeletePattern.MatchFile.IsNullOrWhiteSpace())
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    if (IsMatch(Path.GetFileName(file), DeletePattern.MatchFile))
                    {
                        try
                        {
                            File.Delete(file);
                            WriteProcess($"Delete File:{file}\n", Color.Green);
                        }
                        catch (Exception ex)
                        {
                            WriteProcess($"Can't Delete File:{file}\nError:{ex.Message}\n", Color.Red);
                        }
                    }
                }
            }

            // 删除当前目录
            if (!DeletePattern.MatchFolder.IsNullOrWhiteSpace())
            {
                if (IsMatch(Path.GetFileName(path), DeletePattern.MatchFolder))
                {
                    try
                    {
                        Directory.Delete(path, true);
                        WriteProcess($"Delete Folder:{path}\n", Color.Blue);
                    }
                    catch (Exception ex)
                    {
                        WriteProcess($"Cant Delete Folder:{path}\nError:{ex.Message}\n", Color.Red);
                    }
                }
            }

            if (Directory.Exists(path))
            {
                foreach (var subDir in Directory.GetDirectories(path))
                {
                    Delete(subDir); // 递归子目录
                }
            }
        }

        /// <summary>
        /// 文件名称是否匹配
        /// </summary>
        /// <returns></returns>
        private bool IsMatch(string name, string match)
        {
            if (DeletePattern.IsFullMatch)
            {
                if (DeletePattern.IsIgnoreCase)
                {
                    return name.Equals(match, StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    return name.Equals(match, StringComparison.Ordinal);
                }
            }
            else
            {
                if (DeletePattern.IsIgnoreCase)
                {
                    return name.Contains(match, StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    return name.Contains(match, StringComparison.Ordinal);
                }
            }
        }
    }
}
