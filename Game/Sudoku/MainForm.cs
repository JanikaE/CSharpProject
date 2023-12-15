namespace Sudoku
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
        private Graphics Graphics => SudokuBoard.CreateGraphics();
        private Font SmallFont => new(DefaultFont.FontFamily, Gap / 5);
        private Font LargeFont => new(DefaultFont.FontFamily, Gap / 2);

        #endregion

        private void DrawBoard()
        {
            if (puzzel == null)
            {
                return;
            }
            Graphics.Clear(Color.White);

            for (int i = 0; i <= Length; i++)
            {
                Point left = new(0, i * Gap);
                Point right = new(BoardSize, i * Gap);
                Point top = new(i * Gap, BoardSize);
                Point bottom = new(i * Gap, 0);
                Graphics.DrawLine(new(Color.Black, i % W == 0 ? 3 : 1), top, bottom);
                Graphics.DrawLine(new(Color.Black, i % H == 0 ? 3 : 1), left, right);
            }
        }

        private void DrawNum()
        {
            if (puzzel == null)
            {
                return;
            }

            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    Cell cell = puzzel.PlayMat(row, col);
                    int num = cell.num;
                    if (num != 0)
                    {
                        Point point = new(col * Gap, row * Gap);
                        Graphics.DrawString(num.ToString(), LargeFont, new SolidBrush(Color.Black), point);
                    }
                    else
                    {
                        List<int> posibleNums = cell.posibleNums;
                        foreach (int posibleNum in posibleNums)
                        {
                            Point point = new Point(col * Gap, row * Gap) + new Size((posibleNum - 1) % SubLength * SubGap, (posibleNum - 1) / SubLength * SubGap);
                            Graphics.DrawString(posibleNum.ToString(), SmallFont, new SolidBrush(Color.Black), point);
                        }
                    }
                }
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            puzzel = new();
            puzzel.Generate();
            puzzel.InitPosibleNums();
            DrawBoard();
            DrawNum();
        }
    }
}
