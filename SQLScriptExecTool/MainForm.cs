using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

                return $"server={server};port={port};user={user};password={password};";
            }
        }

        public MainForm()
        {
            InitializeComponent();
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
                return;
            }
        }
    }
}
