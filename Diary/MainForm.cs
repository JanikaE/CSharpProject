namespace Diary
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            x = Width;
            y = Height;
            SetTag(this);

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
                        // �Ƿ��ļ����ŵ���һ���б�
                        DiaryInvalid.Add(fileName);
                    }
                }
                ButtonInvalid.Text = $"�Ƿ��ļ�����{DiaryInvalid.Count}��";
                // ����������������
                DiarysAll.Sort((a, b) =>
                {
                    return a.date.CompareTo(b.date);
                });

                // ��ʼ��������ѡ��
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

        #region �ؼ���С�洰���С�ȱ�������

        /// <summary>
        /// ���嵱ǰ����Ŀ��
        /// </summary>
        private readonly float x;
        /// <summary>
        /// ���嵱ǰ����ĸ߶�
        /// </summary>
        private readonly float y;

        private static void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    SetTag(con);
                }
            }
        }

        private static void SetControls(float newx, float newy, Control cons)
        {
            // ���������еĿؼ����������ÿؼ���ֵ
            foreach (Control con in cons.Controls)
            {
                // ��ȡ�ؼ���Tag����ֵ�����ָ��洢�ַ�������
                if (con.Tag != null)
                {
                    string? tag = con.Tag.ToString();
                    if (string.IsNullOrEmpty(tag))
                        continue;
                    string[] mytag = tag.Split(new char[] { ';' });
                    // ���ݴ������ŵı���ȷ���ؼ���ֵ
                    con.Width = Convert.ToInt32(Convert.ToSingle(mytag[0]) * newx);
                    con.Height = Convert.ToInt32(Convert.ToSingle(mytag[1]) * newy);
                    con.Left = Convert.ToInt32(Convert.ToSingle(mytag[2]) * newx);
                    con.Top = Convert.ToInt32(Convert.ToSingle(mytag[3]) * newy);
                    // �����С
                    float currentSize = Convert.ToSingle(mytag[4]) * newy;
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        SetControls(newx, newy, con);
                    }
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            float newx = Width / x;
            float newy = Height / y;
            SetControls(newx, newy, this);
        }

        #endregion

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
            string? path = ListBoxFile.SelectedItem?.ToString();
            if (path == null || prePath == null)
                return;

            // ��ȡ�ı�
            string text = File.ReadAllText(prePath + path);
            RichTextBox.Text = text;
        }

        private void ButtonInvalid_Click(object sender, EventArgs e)
        {
            InvalidForm form = new();
            form.ShowDialog();
        }
    }
}