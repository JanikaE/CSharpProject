using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SQLScriptExecTool
{
    public partial class MainForm : Form
    {
        private string ConnStr
        {
            get
            {
                string server = textBoxServer.Text;
                string port = textBoxPort.Text;
                string user = textBoxUser.Text;
                string password = textBoxPassword.Text;
                string database = comboBoxDatabase.Text;

                string connStr = $"server={server};port={port};user={user};password={password};";
                if (database.Length > 0)
                    connStr += $"database={database}";
                return connStr;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            LoadConfig();
        }

        private void LoadConfig()
        {
            textBoxServer.Text = Config.Instance.Server;
            textBoxPort.Text = Config.Instance.Port;
            textBoxUser.Text = Config.Instance.User;
            textBoxPassword.Text = Config.Instance.Password;

            textBoxCurrentVersion.Text = Config.Instance.CurrentVersion;
            textBoxTargetVersion.Text = Config.Instance.TargetVertion;
            textBoxDirectory.Text = Config.Instance.Directory;
        }

        private void SaveConfig()
        {
            Config.Instance.Server = textBoxServer.Text;
            Config.Instance.Port = textBoxPort.Text;
            Config.Instance.User = textBoxUser.Text;
            Config.Instance.Password = textBoxPassword.Text;
            Config.Instance.CurrentVersion = textBoxCurrentVersion.Text;
            Config.Instance.TargetVertion = textBoxTargetVersion.Text;
            Config.Instance.Directory = textBoxDirectory.Text;
            Config.Instance.Save();
        }

        /// <summary>
        /// 测试连接，并显示数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTestConnect_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);

            try
            {
                conn.Open();
                MessageBox.Show("连接成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败！\n" + ex.Message);
                return;
            }

            try
            {
                SaveConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                List<string> databases = new List<string>() { "" };

                string sql = "show databases";
                MySqlCommand command = new MySqlCommand(sql)
                {
                    Connection = conn
                };
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[0].ToString());
                    }
                }
                comboBoxDatabase.DataSource = databases;

                buttonOpenFile.Enabled = true;
                buttonExecuteOne.Enabled = true;
                buttonLoadFolder.Enabled = true;
                buttonExecuteMultiple.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取数据库错误！\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        #region 根据文件

        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Title = "选择脚本文件",
                Filter = "脚本文件(*.sql)|*.sql"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName != "")
                {
                    FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    string script = sr.ReadToEnd();
                    richTextBoxScript.Text = script;
                }
            }
        }

        private void ButtonExecuteOne_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            try
            {
                conn.Open();
                MySqlScript command = new MySqlScript(richTextBoxScript.Text)
                {
                    Connection = conn
                };
                int result = command.Execute();

                MessageBox.Show($"执行成功！\n影响的行：{result}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("执行发生错误！\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region 根据版本

        private Version CurrentVersion
        {
            get
            {
                if (Version.TryParse(textBoxCurrentVersion.Text, out Version version))
                {
                    return version;
                }
                else
                {
                    MessageBox.Show("请输入正确的版本！");
                    textBoxCurrentVersion.Focus();
                    return null;
                }
            }
        }

        private Version TargetVersion
        {
            get
            {
                if (Version.TryParse(textBoxTargetVersion.Text, out Version version))
                {
                    return version;
                }
                else
                {
                    MessageBox.Show("请输入正确的版本！");
                    textBoxTargetVersion.Focus();
                    return null;
                }
            }
        }

        private string DirectoryPath { get; set; }

        private void TextBoxDirectory_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
                {
                    SelectedPath = textBoxDirectory.Text,
                    ShowNewFolderButton = false,
                    Description = "选择一个文件夹。将会加载其中包括子文件夹的所有脚本文件。"
                };
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    if (folderBrowserDialog.SelectedPath != null)
                    {
                        textBoxDirectory.Text = folderBrowserDialog.SelectedPath;
                    }
                }
            }
            //else if (e.Button == MouseButtons.Right)
            //{
            //    if (textBoxDirectory.Text.Any())
            //    {
            //        Process.Start(textBoxDirectory.Text);
            //    }
            //}
            else
            {
            }
        }

        private void ButtonLoadFolder_Click(object sender, EventArgs e)
        {
            Version currentVersion = CurrentVersion;
            Version targetVersion = TargetVersion;
            if (currentVersion == null || targetVersion == null) 
            {
                return;
            }

            DirectoryPath = textBoxDirectory.Text;
            List<string> allScripts = LoadFolder(DirectoryPath);            
            SaveConfig();

            checkedListBoxScripts.Items.Clear();
            foreach (string script in allScripts)
            {
                string fileName = Path.GetFileName(script);
                Version version = GetFileVersion(fileName);
                if (version == null)
                {
                    continue;
                }

                if (version >= currentVersion && version <= targetVersion)
                {
                    string path = script.Remove(0, DirectoryPath.Length);
                    checkedListBoxScripts.Items.Add(path);                    
                }
            }

            for (int i = 0; i < checkedListBoxScripts.Items.Count; i++)
            {
                checkedListBoxScripts.SetItemChecked(i, true);
            }
        }

        private void CheckedListBoxScripts_DoubleClick(object sender, EventArgs e)
        {
            string fileName = DirectoryPath + checkedListBoxScripts.SelectedItem;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string script = sr.ReadToEnd();
            richTextBoxScript.Text = script;
            tabControl.SelectedTab = tabPageByFile;
        }

        private void ButtonExecuteMultiple_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            conn.Open();
            for (int i = 0; i < checkedListBoxScripts.Items.Count; i++)
            {
                if (checkedListBoxScripts.GetItemChecked(i))
                {
                    checkedListBoxScripts.SelectedIndex = i;
                }
                else
                {
                    continue;
                }

                try
                {
                    string fileName = DirectoryPath + checkedListBoxScripts.SelectedItem;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    string script = sr.ReadToEnd();

                    MySqlScript command = new MySqlScript(script)
                    {
                        Connection = conn
                    };
                    int result = command.Execute();

                    MessageBox.Show($"执行成功！\n影响的行：{result}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("执行发生错误！\n" + ex.Message);
                }
            }
            conn.Close();            
        }

        #region LinkLabel

        private void LinkLabelChooseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxScripts.Items.Count; i++)
            {
                checkedListBoxScripts.SetItemChecked(i, true);
            }
        }

        private void LinkLabelChooseOthers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxScripts.Items.Count; i++)
            {                
                checkedListBoxScripts.SetItemChecked(i, !checkedListBoxScripts.GetItemChecked(i));
            }
        }

        private void LinkLabelChooseNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < checkedListBoxScripts.Items.Count; i++)
            {
                checkedListBoxScripts.SetItemChecked(i, false);
            }
        }

        #endregion

        /// <summary>
        /// 递归获取文件夹下（包含子文件夹）所有sql文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static List<string> LoadFolder(string path)
        {
            List<string> result = new List<string>();

            // 当前文件夹
            List<string> files = Directory.GetFiles(path).ToList();
            foreach (string file in files) 
            {
                if (Path.GetExtension(file) == ".sql")
                {
                    result.Add(file);
                }
            }
            // 子文件夹
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs) 
            {
                result.AddRange(LoadFolder(dir));
            }
            return result;
        }

        /// <summary>
        /// 获取文件名中可能的版本信息
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static Version GetFileVersion(string file)
        {
            string pattern = @"[vV](([0-9]+.)+[0-9]+)";
            var match = Regex.Match(file, pattern);
            if (match.Success) 
            {
                if (Version.TryParse(match.Groups[1].Value, out Version result))
                {
                    return result;
                }
            }
            return null;
        }

        #endregion

    }
}
