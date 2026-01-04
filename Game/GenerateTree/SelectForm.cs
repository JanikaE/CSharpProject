using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Utils.Tool;

namespace GenerateTree
{
    public partial class SelectForm : Form
    {
        public SelectForm()
        {
            InitializeComponent();
            LoadList();
        }

        private void LoadList()
        {
            ListBoxXml.Items.Clear();
            string[] files = Directory.GetFiles(Config.XMLPath, "*.xml");
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                ListBoxXml.Items.Add(fileName);
            }
        }

        private void ListBoxXml_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? path = ListBoxXml.SelectedItem?.ToString();
            if (path == null)
                return;

            XmlDocument xmlDoc = new();
            xmlDoc.Load(Config.XMLPath + path);
            XmlNode? root = xmlDoc.SelectSingleNode("Config");
            if (root == null)
                return;

            string text = string.Empty;
            foreach (XmlNode node in root.ChildNodes)
            {
                text += node.Name + ":  " + node.InnerText + "\n";
            }
            RichTextBoxContent.Text = text;
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            string? path = ListBoxXml.SelectedItem?.ToString();
            if (path == null)
                return;

            MainForm form = (MainForm)Owner;
            Config? config = XmlTool.FromXmlFile<Config>(Config.XMLPath + path);
            if (config != null)
            {
                form.config = config;
                form.ReadConfig();
                MessageBox.Show("Load Success");
                Close();
            }
            else
            {
                MessageBox.Show("Load Failed");
            }
        }
    }
}
