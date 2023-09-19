using Maze.WayFinding;
using Utils.Mathematical;

namespace Maze.Base
{
    /// <summary>
    /// 只由格子组成的迷宫
    /// </summary>
    public class MazeByBlock
    {
        public bool[,] isWall;

        public int Height => isWall.GetLength(0);

        public int Width => isWall.GetLength(1);

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

        public List<Point2D> FindWay(FindMode solveType = FindMode.DFS)
        {
            Find solve = solveType switch
            {
                FindMode.BFSTree => new Tree(this, TreeType.BFS),
                FindMode.DFSTree => new Tree(this, TreeType.DFS),
                FindMode.DFSRTree => new Tree(this, TreeType.DFSR),
                FindMode.DFS => new DFS(this),
                _ => throw new Exception("Unknown solve mode."),
            };
            way = solve.FindWay();
            return way;
        }
    }
}
