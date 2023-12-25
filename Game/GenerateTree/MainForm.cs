namespace GenerateTree
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ReadConfig();
        }

        public Config config = new();

        public void ReadConfig()
        {
            TextBoxRootRad.Text = config.RootRad.ToString();
            TextBoxRootSize.Text = config.RootSize.ToString();
            TextBoxRootLength.Text = config.RootLength.ToString();
            TextBoxMaxDepth.Text = config.MaxDepth.ToString();
            TextBoxMinSize.Text = config.MinSize.ToString();
            TextBoxMinLength.Text = config.MinLength.ToString();
            TextBoxMaxRad.Text = config.MaxRad.ToString();
            TextBoxChildrenNumMu.Text = config.ChildrenNumMu.ToString();
            TextBoxChildrenNumSigma.Text = config.ChildrenNumSigma.ToString();
            TextBoxSizeChangeMu.Text = config.SizeChangeMu.ToString();
            TextBoxSizeChangeSigma.Text = config.SizeChangeSigma.ToString();
            TextBoxLengthChangeMu.Text = config.LengthChangeMu.ToString();
            TextBoxLengthChangeSigma.Text = config.LengthChangeSigma.ToString();
        }

        private void UpdateConfig()
        {
            config.RootRad = float.Parse(TextBoxRootRad.Text);
            config.RootSize = float.Parse(TextBoxRootSize.Text);
            config.RootLength = float.Parse(TextBoxRootLength.Text);
            config.MaxDepth = int.Parse(TextBoxMaxDepth.Text);
            config.MinSize = float.Parse(TextBoxMinSize.Text);
            config.MinLength = float.Parse(TextBoxMinLength.Text);
            config.MaxRad = float.Parse(TextBoxMaxRad.Text);
            config.ChildrenNumMu = int.Parse(TextBoxChildrenNumMu.Text);
            config.ChildrenNumSigma = float.Parse(TextBoxChildrenNumSigma.Text);
            config.SizeChangeMu = float.Parse(TextBoxSizeChangeMu.Text);
            config.SizeChangeSigma = float.Parse(TextBoxSizeChangeSigma.Text);
            config.LengthChangeMu = float.Parse(TextBoxLengthChangeMu.Text);
            config.LengthChangeSigma = float.Parse(TextBoxLengthChangeSigma.Text);
        }

        private void Clear()
        {
            PictureBoxTree.CreateGraphics().Clear(Color.White);
        }

        private void ButtonGo_Click(object sender, EventArgs e)
        {
            UpdateConfig();
            Clear();
            Draw();
        }

        /// <summary>
        /// �������
        /// </summary>
        private Point startPoint = default;

        private void Draw()
        {
            int seed = int.Parse(TextBoxSeed.Text);
            Tree tree = new(seed, config);
            tree.Generate();
            tree.Draw(PictureBoxTree.CreateGraphics(), startPoint);
        }

        /// <summary>
        /// �Ƿ��ڻ���״̬
        /// </summary>
        private bool line = false;
        /// <summary>
        /// �����
        /// </summary>
        private Point start;
        /// <summary>
        /// ���յ�
        /// </summary>
        private Point end;
        private new Color BackColor => PictureBoxTree.BackColor;

        private void PictureBoxTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (line)
            {
                // ���߹����У��Ҽ�ȡ������
                if (e.Button == MouseButtons.Right)
                {
                    line = false;
                    PictureBoxTree.CreateGraphics().DrawLine(new(BackColor), start, end);
                }
            }
            if (!line)
            {
                // ������£���ʼ����
                if (e.Button == MouseButtons.Left)
                {
                    line = true;
                    if (start != e.Location)
                    {
                        PictureBoxTree.CreateGraphics().DrawLine(new(BackColor), start, end);
                        start = e.Location;
                    }
                }
            }
        }

        private void PictureBoxTree_MouseUp(object sender, MouseEventArgs e)
        {
            if (line)
            {
                // �������ߣ��������ͽǶ�
                if (e.Button == MouseButtons.Left)
                {
                    line = false;
                    float rad = (float)(Math.Atan2(end.Y - start.Y, end.X - start.X) * 180 / Math.PI);

                    config.RootRad = rad;
                    TextBoxRootRad.Text = rad.ToString();
                    startPoint = start;
                }
            }
        }

        private void PictureBoxTree_MouseMove(object sender, MouseEventArgs e)
        {
            if (line)
            {
                if (end != e.Location)
                {
                    PictureBoxTree.CreateGraphics().DrawLine(new(BackColor), start, end);
                    end = e.Location;
                    PictureBoxTree.CreateGraphics().DrawLine(new(Color.Red), start, end);
                }
            }
        }

        /// <summary>
        /// ����ǰ���ñ������ļ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            config.Save();
            MessageBox.Show($"Save Success");
        }

        /// <summary>
        /// ���ļ��ж�ȡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            SelectForm form = new();
            form.ShowDialog(this);
            form.Activate();
            form.BringToFront();
        }
    }
}
