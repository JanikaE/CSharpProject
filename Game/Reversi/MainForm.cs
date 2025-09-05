using System;
using System.Drawing;
using System.Windows.Forms;
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

        /// <summary>
        /// 棋盘边长
        /// </summary>
        private int BoardSize => ChessBoard.Width;

        /// <summary>
        /// 棋盘格子边长
        /// </summary>
        private int CellSize => BoardSize / 8;

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
                Point left = new(0, i * CellSize + CellSize);
                Point right = new(BoardSize, i * CellSize + CellSize);
                Point top = new(i * CellSize + CellSize, BoardSize);
                Point bottom = new(i * CellSize + CellSize, 0);
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
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Chess chess = box.board[j, i];
                    Color color = GetChessColor(chess);
                    if (color == noneColor) continue;

                    DrawChess(graphics, i, j, color);
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
            int positionX = e.X / CellSize * CellSize;
            int positionY = e.Y / CellSize * CellSize;
            int pointX = positionX / CellSize;
            int pointY = positionY / CellSize;

            if (lastPointX == pointX && lastPointY == pointY)
            {
                return;
            }
            else
            {
                Graphics graphics = ChessBoard.CreateGraphics();
                // 清除上一个预览
                DrawChess(graphics, lastPointX, lastPointY, noneColor);
                Point2D point = new(pointX, pointY);
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
            int positionX = e.X / CellSize * CellSize;
            int positionY = e.Y / CellSize * CellSize;
            int pointX = positionX / CellSize;
            int pointY = positionY / CellSize;

            Point2D point = new(pointX, pointY);
            if (box.CanDrop(point, box.turn).Count > 0)
            {
                // 落子
                box.Drop(point, box.turn);
                UpdateChessBoard();
                lastPointX = -1;
                lastPointY = -1;
            }
        }

        private void DrawChess(Graphics graphics, int x, int y, Color color)
        {
            Brush brush = new SolidBrush(color);
            graphics.FillEllipse(brush, x * CellSize + 5, y * CellSize + 5, CellSize * 3 / 4, CellSize * 3 / 4);
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