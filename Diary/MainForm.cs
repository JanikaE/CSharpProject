using System.Configuration;

namespace Diary
{
    public partial class MainForm : Form
    {
        private readonly string[] files;

        public MainForm()
        {
            InitializeComponent();

            string path = ConfigurationManager.AppSettings["DiaryPath"].ToString();
            files = Directory.GetFiles(path, "*.txt");
            foreach (string file in files)
            {
                ListViewItem item = new(Path.GetFileName(file));
                FileList.Items.Add(item);
            }
        }
    }
}