using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
        }

        private void SaveConfig() 
        { 
            Config.Instance.Server = textBoxServer.Text;
            Config.Instance.Port = textBoxPort.Text;
            Config.Instance.User = textBoxUser.Text;
            Config.Instance.Password = textBoxPassword.Text;
            Config.Instance.Save();
        }

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
                List<string> databases = new List<string>();

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
                    string script = File.ReadAllText(openFileDialog.FileName);
                    richTextBoxScript.Text = script;
                }
            }
        }

        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(richTextBoxScript.Text)
                {
                    Connection = conn
                };
                int result = command.ExecuteNonQuery();

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
    }
}
