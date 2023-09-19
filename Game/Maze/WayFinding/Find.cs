using Maze.Base;
using Utils.Mathematical;

namespace Maze.WayFinding
{
    /// <summary>
    /// 迷宫寻路基类
    /// </summary>
    public abstract class Find
    {
        public MazeByWall? maze1;
        public MazeByBlock? maze2;
        private readonly int type;

        protected readonly bool[,] isMark;
        protected bool IsMark(Point2D p) => isMark[p.Y, p.X];
        protected void Mark(Point2D p) => isMark[p.Y, p.X] = true;

        protected Point2D start;
        protected Point2D end;

        public Find(MazeByWall maze, Point2D start, Point2D end) 
        {
            maze1 = maze;
            maze2 = null;
            type = 1;
            isMark = new bool[maze.height, maze.width];
            Mark(start);
            this.start = start;
            this.end = end;
        }

        public Find(MazeByBlock maze)
        {
            maze1 = null;
            maze2 = maze;
            type = 2;
            isMark = new bool[maze.Height, maze.Width];
            isMark[1, 1] = true;
            start = new(1, 1);
            end = new(maze.Width - 2, maze.Height - 2);
        }

        public abstract List<Point2D> FindWay();

        /// <summary>
        /// 找到某格的（未走过的）相邻格
        /// </summary>
        protected List<Point2D> GetRelatedPoint(Point2D p)
        {
            if (type == 1)
            {
                if (maze1 == null)
                    throw new NullReferenceException("type==1 but maze1 is null");
                List<Point2D> result = new();
                int x = p.X;
                int y = p.Y;
                if (x > 0)
                {
                    Point2D left = new(x - 1, y);
                    if (!IsMark(left) && maze1.wall_vertical[y, x - 1])
                    {
                        result.Add(left);
                    }
                }
                if (x < maze1.width - 1)
                {
                    Point2D right = new(x + 1, y);
                    if (!IsMark(right) && maze1.wall_vertical[y, x])
                    {
                        result.Add(right);
                    }
                }
                if (y > 0)
                {
                    Point2D up = new(x, y - 1);
                    if (!IsMark(up) && maze1.wall_horizontal[y - 1, x])
                    {
                        result.Add(up);
                    }
                }
                if (y < maze1.height - 1)
                {
                    Point2D down = new(x, y + 1);
                    if (!IsMark(down) && maze1.wall_horizontal[y, x])
                    {
                        result.Add(down);
                    }
                }
                return result;
            }
            else if (type == 2)
            {
                if (maze2 == null)
                    throw new NullReferenceException("type==2 but maze2 is null");
                List<Point2D> result = new();
                int x = p.X;
                int y = p.Y;
                if (x > 0)
                {
                    Point2D left = new(x - 1, y);
                    if (!IsMark(left) && !maze2.isWall[y, x - 1])
                    {
                        result.Add(left);
                    }
                }
                if (x < maze2.Width - 1)
                {
                    Point2D right = new(x + 1, y);
                    if (!IsMark(right) && !maze2.isWall[y, x + 1])
                    {
                        result.Add(right);
                    }
                }
                if (y > 0)
                {
                    Point2D up = new(x, y - 1);
                    if (!IsMark(up) && !maze2.isWall[y - 1, x])
                    {
                        result.Add(up);
                    }
                }
                if (y < maze2.Height - 1)
                {
                    Point2D down = new(x, y + 1);
                    if (!IsMark(down) && !maze2.isWall[y + 1, x])
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
    }
}
