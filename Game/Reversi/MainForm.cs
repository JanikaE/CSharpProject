using Utils.Mathematical;

namespace Reversi
{
    public partial class MainForm : Form
    {
        private Box? box = null;

        /// <summary>
        /// 棋盘背景色
        /// </summary>
        private static readonly Color noneColor = Color.DarkGreen;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绘制棋盘
        /// </summary>
        private void InitializeChessBoard()
        {
            Graphics graphics = ChessBoard.CreateGraphics();
            graphics.Clear(noneColor);
            Pen pen = new(Color.Black, 2);
            for (int i = 0; i < 7; i++)
            {
                Point left = new(0, i * 40 + 40);
                Point right = new(320, i * 40 + 40);
                Point top = new(i * 40 + 40, 320);
                Point bottom = new(i * 40 + 40, 0);
                graphics.DrawLine(pen, left, right);
                graphics.DrawLine(pen, top, bottom);
            }
        }

        /// <summary>
        /// 更新棋子绘制和信息框
        /// </summary>
        private void UpdateChessBoard()
        {
            if (box == null)
            {
                return;
            }
            Graphics graphics = ChessBoard.CreateGraphics();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Chess chess = box.board[j, i];
                    Color color = GetChessColor(chess);
                    if (color == noneColor) continue;

                    DrawChess(graphics, i, 7 - j, color);
                }
            }
            Turn.Text = box.turn.ToString();
            BlackNum.Text = box.BlackNum.ToString();
            WhiteNum.Text = box.WhiteNum.ToString();
            State.Text = box.state.ToString();
        }

        /// <summary>
        /// 开始/重新开始
        /// </summary>
        private void StartButton_Click(object sender, EventArgs e)
        {
            InitializeChessBoard();
            box = new Box();
            UpdateChessBoard();
        }

        int lastPointX = -1;
        int lastPointY = -1;

        private void ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (box == null || box.state != Reversi.State.Continue) return;
            int positionX = e.X / 40 * 40;
            int positionY = e.Y / 40 * 40;
            int pointX = positionX / 40;
            int pointY = positionY / 40;

            if (lastPointX == pointX && lastPointY == pointY)
            {
                return;
            }
            else
            {
                Graphics graphics = ChessBoard.CreateGraphics();
                // 清除上一个预览
                DrawChess(graphics, lastPointX, lastPointY, noneColor);
                Point2D point = new(pointX, 7 - pointY);
                if (box.Board(point) == Chess.None)
                {
                    lastPointX = pointX;
                    lastPointY = pointY;
                    // 鼠标当前所在格可落子时绘制预览
                    if (box.CanDrop(point, box.turn).Count > 0)
                    {
                        DrawChess(graphics, pointX, pointY, GetChessColor(box.turn));
                    }
                }
            }
        }

        private void ChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (box == null || box.state != Reversi.State.Continue) return;
            int positionX = e.X / 40 * 40;
            int positionY = e.Y / 40 * 40;
            int pointX = positionX / 40;
            int pointY = positionY / 40;

            Point2D point = new(pointX, 7 - pointY);
            if (box.CanDrop(point, box.turn).Count > 0)
            {
                // 落子
                box.Drop(point, box.turn);
                UpdateChessBoard();
                lastPointX = -1;
                lastPointY = -1;
            }
        }

        private static void DrawChess(Graphics graphics, int x, int y, Color color)
        {
            Brush brush = new SolidBrush(color);
            graphics.FillEllipse(brush, x * 40 + 5, y * 40 + 5, 30, 30);
        }

        private static Color GetChessColor(Chess chess)
        {
            return chess switch
            {
                Chess.Black => Color.Black,
                Chess.White => Color.White,
                _ => noneColor,
            };
        }
    }
}