using Maze.WayFinding;
using Utils.Mathematical;

namespace Maze.Base
{
    /// <summary>
    /// 只由格子组成的迷宫
    /// </summary>
    public class MazeByBlock
    {
        /// <summary>格子，true为不可通过</summary>
        public bool[,] isWall;

        public int Height => isWall.GetLength(0);

        public int Width => isWall.GetLength(1);

        public bool IsWall(Point2D p) => isWall[p.Y, p.X];

        private List<Point2D> way = new();

        public MazeByBlock(MazeByWall maze)
        {
            int width = maze.width;
            int height = maze.height;
            isWall = new bool[height * 2 + 1, width * 2 + 1];
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

        public MazeByBlock(bool[,] isWall)
        {
            this.isWall = isWall;
        }

        public MazeByBlock(int[,] isWall)
        {
            int height = isWall.GetLength(0);
            int width = isWall.GetLength(1);
            this.isWall = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    this.isWall[i, j] = !(isWall[i, j] == 0);
                }
            }
        }

        public MazeByBlock(int height, int width, int wallNum)
        {
            isWall = new bool[height, width];
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
                int x = Random.Shared.Next(width);
                int y = Random.Shared.Next(height);
                isWall[y, x] = true;
            }
        }

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

        public List<Point2D> FindWay(Point2D start = default, Point2D end = default, FindMode solveType = FindMode.DFS)
        {
            if (start == default)
                start = new Point2D(1, 1);
            if (end == default)
                end = new Point2D(Width - 2, Height - 2);

            if (IsWall(start))
                throw new ArgumentException("Start point is wall.", nameof(start));
            if (IsWall(end))
                throw new ArgumentException("Start point is wall.", nameof(end));
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
                _ => throw new Exception("Unknown solve mode."),
            };
            way = solve.FindWay();
            return way;
        }

        /// <summary>
        /// 检查某格是否在迷宫范围内
        /// </summary>
        private bool CheckPoint(Point2D point)
        {
            if (point.X < 0 || point.X >= Width || point.Y < 0 || point.Y >= Height)
                return false;
            return true;
        }
    }
}
