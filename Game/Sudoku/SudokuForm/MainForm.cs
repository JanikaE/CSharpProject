﻿using Sudoku.Game;
using Sudoku.Snap;

namespace SudokuForm
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 防止因窗口最小化等原因丢失PictureBox的内容
        /// </summary>
        private readonly Bitmap bitmapM;

        /// <summary>
        /// 记录求解的步骤
        /// </summary>
        private readonly Dictionary<string, PuzzelSnap> steps = new();

        public MainForm()
        {
            InitializeComponent();
            bitmapM = new Bitmap(SudokuBoard.Width, SudokuBoard.Height);
        }

        #region 属性

        private Puzzel? puzzel;
        private int H => puzzel == null ? 0 : puzzel.H;
        private int W => puzzel == null ? 0 : puzzel.W;
        private int BoardSize => Math.Max(SudokuBoard.Height, SudokuBoard.Width);
        private int Length => H * W;
        /// <summary>
        /// 每个小格的边长
        /// </summary>
        private int Gap => BoardSize / Length;
        private int SubLength => (int)Math.Ceiling(Math.Sqrt(Length));
        private int SubGap => Gap / SubLength;

        #endregion

        private void DrawBoard(Puzzel puzzel)
        {
            DrawBoard(new PuzzelSnap(puzzel));
        }

        private void DrawBoard(PuzzelSnap puzzel)
        {
            using Graphics graphics = Graphics.FromImage(bitmapM);
            using Font smallFont = new(DefaultFont.FontFamily, Gap / 5);
            using Font largeFont = new(DefaultFont.FontFamily, Gap / 2);
            graphics.Clear(Color.White);

            // 初始格为灰色
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    CellSnap cell = puzzel.playMat[row, col];
                    if (!cell.canChange)
                    {
                        Point point = new(col * Gap, row * Gap);
                        graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(point, new Size(Gap, Gap)));
                    }
                }
            }
            // 划线
            for (int i = 0; i <= Length; i++)
            {
                Point left = new(0, i * Gap);
                Point right = new(BoardSize, i * Gap);
                Point top = new(i * Gap, BoardSize);
                Point bottom = new(i * Gap, 0);
                graphics.DrawLine(new(Color.Black, i % W == 0 ? 3 : 1), top, bottom);
                graphics.DrawLine(new(Color.Black, i % H == 0 ? 3 : 1), left, right);
            }
            // 数字
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    CellSnap cell = puzzel.playMat[row, col];
                    int num = cell.num;
                    if (num != 0)
                    {
                        Point point = new(col * Gap, row * Gap);
                        graphics.DrawString(num.ToString(), largeFont, new SolidBrush(Color.Black), point);
                    }
                    else
                    {
                        int[] possibleNums = cell.possibleNums;
                        foreach (int possibleNum in possibleNums)
                        {
                            Point point = new Point(col * Gap, row * Gap) + new Size((possibleNum - 1) % SubLength * SubGap, (possibleNum - 1) / SubLength * SubGap);
                            graphics.DrawString(possibleNum.ToString(), smallFont, new SolidBrush(Color.Black), point);
                        }
                    }
                }
            }
            SudokuBoard.Image = bitmapM;
        }

        /// <summary>
        /// 绘制坐标轴
        /// </summary>
        /// <param name="length"></param>
        private void DrawAxis(int length)
        {
            Bitmap bitmapR = new(PictureBoxRow.Width, PictureBoxRow.Height);
            Bitmap bitmapC = new(PictureBoxCol.Width, PictureBoxCol.Height);
            using Graphics graphicsR = Graphics.FromImage(bitmapR);
            using Graphics graphicsC = Graphics.FromImage(bitmapC);
            using Font midFont = new(DefaultFont.FontFamily, Gap / 3);
            graphicsR.Clear(Color.White);
            graphicsC.Clear(Color.White);
            for (int i = 0; i < length; i++)
            {
                graphicsR.DrawString(((char)('A' + i)).ToString(), midFont, new SolidBrush(Color.Black), new Point(0, i * Gap + Gap / 4));
                graphicsC.DrawString(((char)('1' + i)).ToString(), midFont, new SolidBrush(Color.Black), new Point(i * Gap + Gap / 4, 0));
            }
            PictureBoxRow.Image = bitmapR;
            PictureBoxCol.Image = bitmapC;
        }

        /// <summary>
        /// 添加一条求解步骤
        /// </summary>
        /// <param name="msg">步骤信息</param>
        /// <param name="puzzel">该步骤执行后的数独状态</param>
        public void AddSolveStep(string msg, Puzzel puzzel)
        {
            PuzzelSnap puzzelSnap = new(puzzel);

            // 暴力求解的情况下可能会有重复的步骤信息
            string origin = msg;
            int i = 2;
            while (steps.ContainsKey(msg))
            {
                msg = origin + $" ({i})";
                i++;
            }
            steps.Add(msg, puzzelSnap);

            ListBoxStep.Items.Add(msg);
            ListBoxStep.SelectedIndex = ListBoxStep.Items.Count - 1;
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            puzzel = new(3, 3);
            DrawBoard(puzzel);
            DrawAxis(puzzel.Length);

            //puzzel.Generate();
            puzzel.GenerateRandom(AddSolveStep);
            puzzel.InitPosibleNums();
            AddSolveStep("Start.", puzzel);
        }

        private void ButtonSolveArts_Click(object sender, EventArgs e)
        {
            if (puzzel == null)
                return;
            puzzel.SolveArts(AddSolveStep);
        }

        private void ButtonSolveBuster_Click(object sender, EventArgs e)
        {
            if (puzzel == null)
                return;
            puzzel.SolveBuster(AddSolveStep);
        }

        private void ListBoxStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            steps.TryGetValue((string)ListBoxStep.SelectedItem, out PuzzelSnap puzzelSnap);
            DrawBoard(puzzelSnap);
        }
    }
}