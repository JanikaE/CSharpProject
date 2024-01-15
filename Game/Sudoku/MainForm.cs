﻿using Sudoku.Entity;

namespace Sudoku
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 防止因窗口最小化等原因丢失PictureBox的内容
        /// </summary>
        private readonly Bitmap bitmapM;

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
        private Graphics Graphics => Graphics.FromImage(bitmapM);
        private Font SmallFont => new(DefaultFont.FontFamily, Gap / 5);
        private Font MidFont => new(DefaultFont.FontFamily, Gap / 3);
        private Font LargeFont => new(DefaultFont.FontFamily, Gap / 2);

        #endregion

        private void DrawBoard(Puzzel puzzel)
        {
            DrawBoard(new PuzzelSnap(puzzel));
        }

        private void DrawBoard(PuzzelSnap puzzel)
        {
            Graphics.Clear(Color.White);

            // 初始格为灰色
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    CellSnap cell = puzzel.playMat[row, col];
                    if (!cell.canChange)
                    {
                        Point point = new(col * Gap, row * Gap);
                        Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(point, new Size(Gap, Gap)));
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
                Graphics.DrawLine(new(Color.Black, i % W == 0 ? 3 : 1), top, bottom);
                Graphics.DrawLine(new(Color.Black, i % H == 0 ? 3 : 1), left, right);
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
                        Graphics.DrawString(num.ToString(), LargeFont, new SolidBrush(Color.Black), point);
                    }
                    else
                    {
                        int[] posibleNums = cell.posibleNums;
                        foreach (int posibleNum in posibleNums)
                        {
                            Point point = new Point(col * Gap, row * Gap) + new Size((posibleNum - 1) % SubLength * SubGap, (posibleNum - 1) / SubLength * SubGap);
                            Graphics.DrawString(posibleNum.ToString(), SmallFont, new SolidBrush(Color.Black), point);
                        }
                    }
                }
            }
            SudokuBoard.Image = bitmapM;
        }

        private void DrawAxis(int length)
        {
            Bitmap bitmapR = new(PictureBoxRow.Width, PictureBoxRow.Height);            
            Bitmap bitmapC = new(PictureBoxCol.Width, PictureBoxCol.Height);
            Graphics graphicsR = Graphics.FromImage(bitmapR);
            Graphics graphicsC = Graphics.FromImage(bitmapC);
            graphicsR.Clear(Color.White);
            graphicsC.Clear(Color.White);
            for (int i = 0; i < length; i++)
            {
                graphicsR.DrawString(((char)('A' + i)).ToString(), MidFont, new SolidBrush(Color.Black), new Point(0, i * Gap + Gap / 4));
                graphicsC.DrawString(((char)('1' + i)).ToString(), MidFont, new SolidBrush(Color.Black), new Point(i * Gap + Gap / 4, 0));
            }
            PictureBoxRow.Image = bitmapR;
            PictureBoxCol.Image = bitmapC;
        }

        public void AddSolveStep(string msg, Puzzel puzzel)
        {
            PuzzelSnap puzzelSnap = new(puzzel);
            steps.Add(msg, puzzelSnap);
            ListBoxStep.Items.Add(msg);
            ListBoxStep.SelectedIndex = ListBoxStep.Items.Count - 1;
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            puzzel = new();
            //puzzel.Generate();
            puzzel.GenerateByExample(Example.examples[1]);
            puzzel.InitPosibleNums();
            DrawBoard(puzzel);
            DrawAxis(puzzel.Length);
            AddSolveStep("Start.", puzzel);
        }

        private void ButtonSolve_Click(object sender, EventArgs e)
        {
            if (puzzel == null)
                return;
            puzzel.StartSolve(this);
        }

        private void ListBoxStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            steps.TryGetValue((string)ListBoxStep.SelectedItem, out PuzzelSnap puzzelSnap);
            DrawBoard(puzzelSnap);
        }
    }
}
