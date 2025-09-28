using Maze.Base;
using System;
using System.Collections.Generic;
using Utils.Mathematical;

namespace Maze.WayFinding
{
    /// <summary>
    /// 迷宫寻路基类
    /// </summary>
    public abstract class Find
    {
        public IMaze maze;
        private readonly int type;

        protected readonly bool[,] isMark;
        protected bool IsMark(Point2D p) => isMark[p.Y, p.X];
        protected void Mark(Point2D p) => isMark[p.Y, p.X] = true;

        protected Point2D start;
        protected Point2D end;

        public Find(IMaze maze)
        {
            this.maze = maze;
            if (maze is MazeByWall)
                type = 1;
            else if (maze is MazeByBlock)
                type = 2;
            else
                throw new NotImplementedException("UnKnown maze type.");

            isMark = new bool[maze.Height, maze.Width];
            isMark[1, 1] = true;
            start = new(1, 1);
            end = new(maze.Width - 2, maze.Height - 2);
        }

        public Find(IMaze maze, Point2D start, Point2D end)
        {
            this.maze = maze;
            if (maze is MazeByWall)
                type = 1;
            else if (maze is MazeByBlock)
                type = 2;
            else
                throw new NotImplementedException("UnKnown maze type.");

            isMark = new bool[maze.Height, maze.Width];
            Mark(start);
            this.start = start;
            this.end = end;
        }

        public abstract List<Point2D> FindWay();

        /// <summary>
        /// 找到某格的（未走过的）相邻格
        /// </summary>
        protected List<Point2D> GetRelatedPoint(Point2D p)
        {
            if (type == 1)
            {
                MazeByWall mazeByWall = (MazeByWall)maze;
                List<Point2D> result = new();
                int x = p.X;
                int y = p.Y;
                if (x > 0)
                {
                    Point2D left = new(x - 1, y);
                    if (!IsMark(left) && mazeByWall.wall_vertical[y, x - 1])
                    {
                        result.Add(left);
                    }
                }
                if (x < mazeByWall.width - 1)
                {
                    Point2D right = new(x + 1, y);
                    if (!IsMark(right) && mazeByWall.wall_vertical[y, x])
                    {
                        result.Add(right);
                    }
                }
                if (y > 0)
                {
                    Point2D up = new(x, y - 1);
                    if (!IsMark(up) && mazeByWall.wall_horizontal[y - 1, x])
                    {
                        result.Add(up);
                    }
                }
                if (y < mazeByWall.height - 1)
                {
                    Point2D down = new(x, y + 1);
                    if (!IsMark(down) && mazeByWall.wall_horizontal[y, x])
                    {
                        result.Add(down);
                    }
                }
                return result;
            }
            else if (type == 2)
            {
                MazeByBlock mazeByBlock = (MazeByBlock)maze;
                List<Point2D> result = new();
                int x = p.X;
                int y = p.Y;
                if (x > 0)
                {
                    Point2D left = new(x - 1, y);
                    if (!IsMark(left) && !mazeByBlock.IsWall(left))
                    {
                        result.Add(left);
                    }
                }
                if (x < mazeByBlock.Width - 1)
                {
                    Point2D right = new(x + 1, y);
                    if (!IsMark(right) && !mazeByBlock.IsWall(right))
                    {
                        result.Add(right);
                    }
                }
                if (y > 0)
                {
                    Point2D up = new(x, y - 1);
                    if (!IsMark(up) && !mazeByBlock.IsWall(up))
                    {
                        result.Add(up);
                    }
                }
                if (y < mazeByBlock.Height - 1)
                {
                    Point2D down = new(x, y + 1);
                    if (!IsMark(down) && !mazeByBlock.IsWall(down))
                    {
                        result.Add(down);
                    }
                }
                return result;
            }
            else
            {
                throw new NotImplementedException("UnKnown type.");
            }
        }
    }

    /// <summary>
    /// 寻路类型
    /// </summary>
    public enum FindMode
    {
        /// <summary>以广度优先（队列）建树</summary>
        BFSTree,

        /// <summary>以深度优先（栈）建树</summary>
        DFSTree,

        /// <summary>以深度优先（递归）建树</summary>
        DFSRTree,

        /// <summary>深度优先</summary>
        DFS,

        /// <summary>A*</summary>
        AStar,
    }
}
