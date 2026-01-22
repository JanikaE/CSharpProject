using System;
using System.Collections.Generic;
using Utils.Mathematical;

namespace Reversi
{
    public class Box
    {
        public readonly Map2D<Chess> board;

        public Chess turn;
        public State state;

        public int BlackNum => CountChess(Chess.Black);
        public int WhiteNum => CountChess(Chess.White);

        public Box()
        {
            board = new Map2D<Chess>(8, 8);
            board[3, 3] = Chess.Black;
            board[3, 4] = Chess.White;
            board[4, 3] = Chess.White;
            board[4, 4] = Chess.Black;
            turn = Chess.Black;
            state = State.Continue;
        }

        /// <summary>
        /// 落子
        /// </summary>
        public void Drop(Point2D point, Chess chess)
        {
            List<Point2D> list = CanDrop(point, chess);
            if (list.Count == 0)
                return;
            board[point.Y, point.X] = chess;
            foreach (Point2D another in list)
            {
                Point2D offset = another - point;
                offset.Normalize();
                Point2D newPoint = point;
                while (true)
                {
                    newPoint += offset;
                    if (newPoint == another) break;
                    Reverse(newPoint);
                }
            }
            if (CheckFull())
            {
                state = State.End;
                return;
            }
            if (CanDrop(turn.Opposite()))
            {
                turn = turn.Opposite();
            }
            else
            {
                if (!CanDrop(turn))
                {
                    state = State.End;
                }
            }
        }

        /// <summary>
        /// 翻转
        /// </summary>
        private void Reverse(Point2D point)
        {
            int x = point.X;
            int y = point.Y;
            board[y, x] = board[y, x].Opposite();
        }

        /// <summary>
        /// 是否可在某一点落子
        /// </summary>
        /// <returns>返回在落子后能翻转的方向的另一端的子的集合，若集合为空则不能落子</returns>
        public List<Point2D> CanDrop(Point2D point, Chess chess)
        {
            List<Point2D> result = new();
            if (chess == Chess.None || !IsInBoard(point))
                return result;
            Chess opposite = chess.Opposite();
            for (int i = 1; i <= 8; i++)
            {
                RelativePosition_8 position = (RelativePosition_8)i;
                Point2D offset = RelativePosition.ToPoint2D(position);
                Point2D newPoint = point;
                int flag = 0;
                while (true)
                {
                    newPoint += offset;
                    if (!IsInBoard(newPoint))
                    {
                        break;
                    }
                    Chess newChess = board[newPoint];
                    if (newChess == Chess.None)
                    {
                        break;
                    }
                    else if (newChess == opposite)
                    {
                        flag = 1;
                        continue;
                    }
                    else if (newChess == chess)
                    {
                        if (flag == 0)
                        {
                            break;
                        }
                        else if (flag == 1)
                        {
                            result.Add(newPoint);
                            break;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 棋盘上是否存在可落子的点
        /// </summary>
        public bool CanDrop(Chess chess)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == Chess.None)
                    {
                        Point2D point = new(i, j);
                        if (CanDrop(point, chess).Count > 0)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 棋盘是否已满
        /// </summary>
        public bool CheckFull()
        {
            foreach (Chess c in board)
            {
                if (c == Chess.None)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 棋盘内棋子数量
        /// </summary>
        public int CountChess(Chess chess)
        {
            int count = 0;
            foreach (Chess c in board)
            {
                if (c == chess)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 将棋盘显示在终端
        /// </summary>
        public void PrintToConsole()
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7");
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{i} ");
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(board[j, i].ToChar());
                }
                Console.Write("\n");
            }
            Console.Write($"\n{Chess.Black.ToChar()}Num:{BlackNum}  {Chess.White.ToChar()}Num:{WhiteNum}  NowTurn:{turn.ToChar()}\n");
        }

        /// <summary>
        /// 某点是否在棋盘内
        /// </summary>
        private static bool IsInBoard(Point2D point)
        {
            return (point.X >= 0 && point.X <= 7 && point.Y >= 0 && point.Y <= 7);
        }
    }
}