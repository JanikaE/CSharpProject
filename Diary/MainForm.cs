using System.Configuration;

namespace Diary
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            if (prePath != null)
            {
                string[] files = Directory.GetFiles(prePath, "*.txt");
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    try
                    {
                        string[] s = fileName.Split('_');
                        string dateString = s[0];
                        string[] s2 = dateString.Split(".");
                        DateTime date = new(int.Parse(s2[0]), int.Parse(s2[1]), int.Parse(s2[2]));
                        string topic = s[1];
                        DiaryDto dto = new(date, fileName, topic, file);
                        DiarysAll.Add(dto);
                    }
                    catch
                    {
                        DiaryInvalid.Add(fileName);
                    }
                }
                DiarysAll.Sort((a, b) =>
                {
                    return a.date.CompareTo(b.date);
                });

                ComboBoxYear.Items.Clear();
                ComboBoxYear.Items.Add("All");
                foreach (DiaryDto diary in DiarysAll)
                {
                    int year = diary.date.Year;
                    if (!ComboBoxYear.Items.Contains(year))
                    {
                        ComboBoxYear.Items.Add(year);
                    }
                }
                ComboBoxYear.SelectedIndex = 0;
                ComboBoxMonth.Items.Clear();
                ComboBoxMonth.Items.Add("All");
                ComboBoxMonth.SelectedIndex = 0;
                UpdateListBoxFile();
            }
        }

        private void ComboBoxYear_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBoxMonth.Items.Clear();
            ComboBoxMonth.Items.Add("All");
            if (ComboBoxYear.SelectedIndex != 0)
            {
                int year = (int)ComboBoxYear.SelectedItem;
                foreach (DiaryDto diary in DiarysAll)
                {
                    if (diary.date.Year == year)
                    {
                        int month = diary.date.Month;
                        if (!ComboBoxMonth.Items.Contains(month))
                        {
                            ComboBoxMonth.Items.Add(month);
                        }
                    }
                }
            }
            ComboBoxMonth.SelectedIndex = 0;
            UpdateListBoxFile();
        }

        private void ComboBoxMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateListBoxFile();
        }

        private void UpdateListBoxFile()
        {
            ListBoxFile.Items.Clear();
            if (prePath == null)
            {
                ListBoxFile.Items.Add("Path is null.");
            }
            else if (DiarysAll.Count == 0)
            {
                ListBoxFile.Items.Add("No diary exists.");
            }
            else
            {
                foreach (DiaryDto diary in DiarysAll)
                {
                    if ((ComboBoxYear.SelectedIndex == 0 || diary.date.Year == (int)ComboBoxYear.SelectedItem) &&
                        (ComboBoxMonth.SelectedIndex == 0 || diary.date.Month == (int)ComboBoxMonth.SelectedItem))
                    {
                        ListBoxFile.Items.Add(diary.name);
                    }
                }
            }
        }

        private void ListBoxFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? path = ListBoxFile.SelectedItem.ToString();
            if (path == null || prePath == null)
                return;

            string text = File.ReadAllText(prePath + path);
            TextBox.Text = text;
        }

        private void ButtonInvalid_Click(object sender, EventArgs e)
        {
            InvalidForm form = new();
            form.ShowDialog();
        }
    }
}