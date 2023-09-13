using Utils.Mathematical;

namespace Maze.WayFinding
{
    /// <summary>
    /// 迷宫寻路基类
    /// </summary>
    public abstract class Find
    {
        public Maze maze;

        protected readonly bool[,] isMark;
        protected bool IsMark(Point2D p) => isMark[p.Y, p.X];
        protected void Mark(Point2D p) => isMark[p.Y, p.X] = true;

        protected Point2D start;
        protected Point2D end;

        public Find(Maze maze, Point2D start, Point2D end) 
        {
            this.maze = maze;
            isMark = new bool[maze.height, maze.width];
            isMark[0, 0] = true;
            this.start = start;
            this.end = end;
        }

        public abstract List<Point2D> FindWay();

        /// <summary>
        /// 找到某格的（未走过的）相邻格
        /// </summary>
        protected List<Point2D> GetRelatedPoint(Point2D p)
        {
            List<Point2D> result = new();
            int x = p.X;
            int y = p.Y;
            if (x > 0)
            {
                Point2D left = new(x - 1, y);
                if (!IsMark(left) && maze.wall_vertical[y, x - 1])
                {
                    result.Add(left);
                }
            }
            if (x < maze.width - 1)
            {
                Point2D right = new(x + 1, y);
                if (!IsMark(right) && maze.wall_vertical[y, x])
                {
                    result.Add(right);
                }
            }
            if (y > 0)
            {
                Point2D up = new(x, y - 1);
                if (!IsMark(up) && maze.wall_horizontal[y - 1, x])
                {
                    result.Add(up);
                }
            }
            if (y < maze.height - 1)
            {
                Point2D down = new(x, y + 1);
                if (!IsMark(down) && maze.wall_horizontal[y, x])
                {
                    result.Add(down);
                }
            }
            return result;
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
