using G2048;
using System;
using System.Threading;
using System.Windows.Forms;
using Utils.Mathematical;

namespace G2048
{
    public partial class MainForm : Form
    {
        private const int rage = 4;
        private Box box;
        private readonly Label[,] labels = new Label[rage, rage];

        public MainForm()
        {
            InitializeComponent();
            box = new Box(rage);
            box.Init();

            labels[0, 0] = l00;
            labels[0, 1] = l01;
            labels[0, 2] = l02;
            labels[0, 3] = l03;
            labels[1, 0] = l10;
            labels[1, 1] = l11;
            labels[1, 2] = l12;
            labels[1, 3] = l13;
            labels[2, 0] = l20;
            labels[2, 1] = l21;
            labels[2, 2] = l22;
            labels[2, 3] = l23;
            labels[3, 0] = l30;
            labels[3, 1] = l31;
            labels[3, 2] = l32;
            labels[3, 3] = l33;

            UpdateForm();
        }

        private void Up_Click(object sender, EventArgs e)
        {
            Operate(RelativePosition_4.Up);
        }

        private void Down_Click(object sender, EventArgs e)
        {
            Operate(RelativePosition_4.Down);
        }

        private void Left_Click(object sender, EventArgs e)
        {
            Operate(RelativePosition_4.Left);
        }

        private void Right_Click(object sender, EventArgs e)
        {
            Operate(RelativePosition_4.Right);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            box.Init();
            UpdateForm();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    Operate(RelativePosition_4.Left);
                    break;
                case Keys.D:
                    Operate(RelativePosition_4.Right);
                    break;
                case Keys.W:
                    Operate(RelativePosition_4.Up);
                    break;
                case Keys.S:
                    Operate(RelativePosition_4.Down);
                    break;
            }
        }

        private void AI_Click(object sender, EventArgs e)
        {
            int all = 0;
            int min = 0;
            int max = 0;
            for (int i = 0; i < 100; i++)
            {
                box = new Box(rage);
                box.Init();
                while (box.state == State.Playing)
                {
                    RelativePosition_4 op = box.Next();
                    if (op == RelativePosition_4.None)
                    {
                        break;
                    }
                    Operate(op);
                }
                int score = box.score;
                all += score;
                if (i == 0)
                {
                    min = score;
                    max = score;
                }
                if (score > max)
                {
                    max = score;
                }
                if (score < min)
                {
                    min = score;
                }
            }
            MessageBox.Show($"ave:{all / 100} max:{max} min:{min}");
        }

        private void Operate(RelativePosition_4 op)
        {
            box.Operate(op);
            UpdateForm();
        }

        private void UpdateForm()
        {
            for (int i = 0; i < rage; i++)
            {
                for (int j = 0; j < rage; j++)
                {
                    labels[i, j].Text = box.playMat[i, j] == 0 ? "" : box.playMat[i, j].ToString();
                }
            }
            Score.Text = "Score:" + box.score;
            Result.Visible = box.CheckFail();
        }
    }
}
