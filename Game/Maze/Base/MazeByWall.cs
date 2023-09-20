using Maze.WayFinding;
using Utils.Mathematical;

namespace Maze.Base
{
    /// <summary>
    /// 迷宫基类，由格子和墙构成的迷宫
    /// </summary>
    public abstract class MazeByWall
    {
        public int height;
        public int width;

        /// <summary>竖直的墙，true为可通过</summary>
        public bool[,] wall_vertical;

        /// <summary>水平的墙，true为可通过</summary>
        public bool[,] wall_horizontal;

        private List<Point2D> way = new();

        public MazeByWall(int height, int width)
        {
            this.height = height;
            this.width = width;
            wall_vertical = new bool[height, width];
            wall_horizontal = new bool[height, width];
        }

        public abstract void Generate();

        /// <summary>
        /// 终端显示迷宫
        /// </summary>
        /// <param name="showWay">是否显示路径（如果有的话）</param>
        public void Show(bool showWay = false)
        {
            for (int i = 0; i < width * 2 + 1; i++)
            {
                Console.Write('#');
            }
            Console.Write('\n');
            for (int i = 0; i < height; i++)
            {
                Console.Write('#');
                for (int j = 0; j < width; j++)
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
                    if (wall_vertical[i, j])
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write('#');
                    }
                }
                Console.Write('\n');
                Console.Write('#');
                for (int j = 0; j < width; j++)
                {
                    if (wall_horizontal[i, j])
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write('#');
                    }
                    Console.Write('#');
                }
                Console.Write('\n');
            }
        }

        public List<Point2D> FindWay(Point2D start = default, Point2D end = default, FindMode solveType = FindMode.DFS)
        {
            if (!CheckPoint(start))
                throw new ArgumentOutOfRangeException(nameof(start), "Start point is out of the maze.");
            if (!CheckPoint(end))
                throw new ArgumentOutOfRangeException(nameof(end), "End point is out of the maze.");
            
            if (start == default)
                start = new Point2D(0, 0);
            if (end == default)
                end = new Point2D(width - 1, height - 1);
            Find solve = solveType switch
            {
                FindMode.BFSTree => new Tree(this, start, end, TreeType.BFS),
                FindMode.DFSTree => new Tree(this, start, end, TreeType.DFS),
                FindMode.DFSRTree => new Tree(this, start, end, TreeType.DFSR),
                FindMode.DFS => new DFS(this, start, end),
                _ => throw new Exception("Unknown solve mode."),
            };
            way = solve.FindWay();
            return way;
        }

        /// <summary>
        /// 获取某格的四周的格子
        /// </summary>
        protected List<Point2D> GetNeighborBlocks(int x, int y)
        {
            List<Point2D> points = new();
            if (x > 0)
                points.Add(new(x - 1, y));
            if (x < width - 1)
                points.Add(new(x + 1, y));
            if (y > 0)
                points.Add(new(x, y - 1));
            if (y < height - 1)
                points.Add(new(x, y + 1));
            return points;
        }

        /// <summary>
        /// 打通两相邻格之间的墙
        /// </summary>
        protected void BreakWall(Point2D point1, Point2D point2)
        {
            int x = point1.X;
            int y = point1.Y;
            switch (GetRelativePosition(point1, point2))
            {
                case RelativePosition.Left:
                    wall_vertical[y, x - 1] = true;
                    break;
                case RelativePosition.Right:
                    wall_vertical[y, x] = true;
                    break;
                case RelativePosition.Up:
                    wall_horizontal[y - 1, x] = true;
                    break;
                case RelativePosition.Down:
                    wall_horizontal[y, x] = true;
                    break;
            }
        }

        /// <summary>
        /// 计算两个相邻格子的相对位置
        /// </summary>
        protected static RelativePosition GetRelativePosition(Point2D me, Point2D another)
        {
            if (another.X - me.X == 1 && another.Y == me.Y)
                return RelativePosition.Right;
            if (another.X - me.X == -1 && another.Y == me.Y)
                return RelativePosition.Left;
            if (another.X == me.X && another.Y - me.Y == 1)
                return RelativePosition.Down;
            if (another.X == me.X && another.Y - me.Y == -1)
                return RelativePosition.Up;
            return RelativePosition.None;
        }

        /// <summary>
        /// 检查某格是否在迷宫范围内
        /// </summary>
        private bool CheckPoint(Point2D point)
        {
            if (point.X < 0 || point.X >= width || point.Y < 0 || point.Y >= height)
                return false;
            return true;
        }
    }
}
