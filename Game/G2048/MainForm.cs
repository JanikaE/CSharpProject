using G2048;
using System;
using System.Windows.Forms;

namespace G2048Form
{
    public partial class MainForm : Form
    {
        private const int rage = 4;
        private readonly Box box;
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
            Operate(Operation.Up);
        }

        private void Down_Click(object sender, EventArgs e)
        {
            Operate(Operation.Down);
        }

        private void Left_Click(object sender, EventArgs e)
        {
            Operate(Operation.Left);
        }

        private void Right_Click(object sender, EventArgs e)
        {
            Operate(Operation.Right);
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
                    Operate(Operation.Left);
                    break;
                case Keys.D:
                    Operate(Operation.Right);
                    break;
                case Keys.W:
                    Operate(Operation.Up);
                    break;
                case Keys.S:
                    Operate(Operation.Down);
                    break;
            }
        }

        private void Operate(Operation op)
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
