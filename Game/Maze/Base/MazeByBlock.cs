using Maze.WayFinding;
using System;
using System.Collections.Generic;
using Utils.Extend;
using Utils.Mathematical;

namespace Maze.Base
{
    /// <summary>
    /// 只由格子组成的迷宫
    /// </summary>
    public class MazeByBlock : IMaze
    {
        /// <summary>格子，true为不可通过</summary>
        public Map2D<bool> isWall;

        public int Height => isWall.Height;

        public int Width => isWall.Width;

        public bool IsWall(Point2D p) => isWall[p];

        public List<Point2D> way = new();

        public MazeByBlock(MazeByWall maze)
        {
            int width = maze.width;
            int height = maze.height;
            isWall = new Map2D<bool>(height * 2 + 1, width * 2 + 1);
            for (int i = 0; i < width * 2 + 1; i++)
            {
                isWall[0, i] = true;
                isWall[height * 2, i] = true;
            }
            for (int i = 0; i < height * 2 + 1; i++)
            {
                isWall[i, 0] = true;
                isWall[i, width * 2] = true;
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (!maze.wall_vertical[i, j])
                        isWall[i * 2 + 1, j * 2 + 2] = true;
                    if (!maze.wall_horizontal[i, j])
                        isWall[i * 2 + 2, j * 2 + 1] = true;
                    isWall[i * 2 + 2, j * 2 + 2] = true;
                }
            }
        }

        /// <summary>
        /// 用bool数组构造迷宫
        /// </summary>
        /// <param name="isWall">false表示可通过</param>
        public MazeByBlock(bool[,] isWall)
        {
            this.isWall = new Map2D<bool>(isWall);
        }

        /// <summary>
        /// 用int数组构造迷宫
        /// </summary>
        /// <param name="isWall">0表示可通过</param>
        public MazeByBlock(int[,] isWall)
        {
            this.isWall = Map2D<bool>.ToMap2D(isWall, (n) => n != 0);
        }

        /// <summary>
        /// 随机构造一个由障碍物构成的迷宫，起点和终点处不会生成障碍物
        /// </summary>
        public MazeByBlock(int height, int width, int wallNum, Point2D start = default, Point2D end = default)
        {
            isWall = new Map2D<bool>(height, width);
            if (start == default)
                start = new Point2D(1, 1);
            if (end == default)
                end = new Point2D(width - 2, height - 2);

            for (int i = 0; i < width; i++)
            {
                isWall[0, i] = true;
                isWall[height - 1, i] = true;
            }
            for (int i = 0; i < height; i++)
            {
                isWall[i, 0] = true;
                isWall[i, width - 1] = true;
            }
            for (int i = 0; i < wallNum; i++)
            {
                int x = Random.Shared.Next(1, width - 1);
                int y = Random.Shared.Next(1, height - 1);
                Point2D point = new(x, y);
                if (point == start || point == end)
                {
                    i--;
                    continue;
                }
                isWall[y, x] = true;
            }
        }

        /// <summary>
        /// 终端显示迷宫
        /// </summary>
        /// <param name="showWay">是否显示路径（如果有的话）</param>
        public void Show(bool showWay = false)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (isWall[i, j])
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        if (showWay)
                        {
                            if (way.FindAll(p => p.X == j && p.Y == i).Count > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write('*');
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            else
                            {
                                Console.Write(' ');
                            }
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                }
                Console.Write('\n');
            }
        }

        /// <summary>
        /// 寻路
        /// </summary>
        /// <param name="start">起点（默认为左上角）</param>
        /// <param name="end">终点（默认为右下角）</param>
        /// <param name="solveType">寻路方式</param>
        /// <returns>返回从起点到终点经过的点</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public List<Point2D> FindWay(Point2D start = default, Point2D end = default, FindMode solveType = FindMode.DFS)
        {
            if (start == default)
                start = new Point2D(1, 1);
            if (end == default)
                end = new Point2D(Width - 2, Height - 2);

            if (IsWall(start))
                throw new ArgumentException("Start point is wall.", nameof(start));
            if (IsWall(end))
                throw new ArgumentException("End point is wall.", nameof(end));
            if (!CheckPoint(start))
                throw new ArgumentOutOfRangeException(nameof(start), "Start point is out of the maze.");
            if (!CheckPoint(end))
                throw new ArgumentOutOfRangeException(nameof(end), "End point is out of the maze.");

            Find solve = solveType switch
            {
                FindMode.BFSTree => new Tree(this, start, end, TreeType.BFS),
                FindMode.DFSTree => new Tree(this, start, end, TreeType.DFS),
                FindMode.DFSRTree => new Tree(this, start, end, TreeType.DFSR),
                FindMode.DFS => new DFS(this, start, end),
                FindMode.AStar => new AStar(this, start, end),
                _ => throw new ArgumentException("Unknown solve mode.", nameof(solveType)),
            };
            way = solve.FindWay();
            return way;
        }

        /// <summary>
        /// 检查迷宫能否从起点走到终点
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool Check(Point2D start = default, Point2D end = default)
        {
            if (start == default)
                start = new Point2D(1, 1);
            if (end == default)
                end = new Point2D(Width - 2, Height - 2);

            if (!CheckPoint(start))
                throw new ArgumentOutOfRangeException(nameof(start), "Start point is out of the maze.");
            if (!CheckPoint(end))
                throw new ArgumentOutOfRangeException(nameof(end), "End point is out of the maze.");

            List<Point2D> open = new() { start };
            List<Point2D> close = new();
            while (open.Count > 0)
            {
                Point2D now = open[0];
                if (Point2D.Manhattan(now, end) == 1)
                {
                    return true;
                }
                open.RemoveAt(0);
                close.Add(now);

                int x = now.X;
                int y = now.Y;
                if (x > 0)
                {
                    Point2D left = new(x - 1, y);
                    if (close.FindAll(p => p == left).Count == 0 && open.FindAll(p => p == left).Count == 0 && !IsWall(left))
                    {
                        open.Add(left);
                    }
                }
                if (x < Width - 1)
                {
                    Point2D right = new(x + 1, y);
                    if (close.FindAll(p => p == right).Count == 0 && open.FindAll(p => p == right).Count == 0 && !IsWall(right))
                    {
                        open.Add(right);
                    }
                }
                if (y > 0)
                {
                    Point2D up = new(x, y - 1);
                    if (close.FindAll(p => p == up).Count == 0 && open.FindAll(p => p == up).Count == 0 && !IsWall(up))
                    {
                        open.Add(up);
                    }
                }
                if (y < Height - 1)
                {
                    Point2D down = new(x, y + 1);
                    if (close.FindAll(p => p == down).Count == 0 && open.FindAll(p => p == down).Count == 0 && !IsWall(down))
                    {
                        open.Add(down);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检查某格是否在迷宫范围内
        /// </summary>
        private bool CheckPoint(Point2D point)
        {
            return isWall.IsValidPoint(point);
        }
    }
}
