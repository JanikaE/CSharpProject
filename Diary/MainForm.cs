using System.Configuration;
using System.Globalization;
using System.Windows.Forms;

namespace Diary
{
    public partial class MainForm : Form
    {
        private readonly List<string> files;
        private readonly Dictionary<string, DiaryDto> Diarys;

        private readonly string? prePath;

        public MainForm()
        {
            InitializeComponent();
            files = new();
            Diarys = new();
            prePath = ConfigurationManager.AppSettings["DiaryPath"]?.ToString();
            if (prePath == null)
            {
                FileListBox.Items.Add("path is null");
            }
            else
            {
                files = Directory.GetFiles(prePath, "*.txt").ToList();
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string[] s = fileName.Split('_');
                    DateTime date = DateTime.ParseExact(s[0], "yyyy.mm.dd", CultureInfo.CurrentCulture);
                    string topic = s[1];
                    DiaryDto dto = new(date, topic, file);
                    Diarys.Add(fileName, dto);

                    FileListBox.Items.Add(fileName);
                }
            }
        }

        private void FileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? path = FileListBox.SelectedItem.ToString();
            if (path == null || prePath == null)
                return;

            string text = File.ReadAllText(prePath + path);
            BodyTextBox.Text = text;
        }
    }
}