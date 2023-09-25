using Maze.Base;
using Maze.WayFinding;
using Utils.Mathematical;

namespace Maze
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool test = false;
            if (test)
            {
                MazeByWall maze = new DepthFirst(10, 30);
                maze.Generate();
                maze.Show();
                Console.WriteLine();

                maze.FindWay(new(0, 0), new(29, 9), FindMode.AStar);
                maze.Show(true);
                Console.WriteLine();

                MazeByBlock maze2 = new(maze);
                maze2.Show();
                Console.WriteLine();

                maze2.FindWay(solveType: FindMode.AStar);
                maze2.Show(true);
            }
            else
            {
                Point2D start = Point2D.RandomPoint(1, 29, 1, 9);
                Point2D end = Point2D.RandomPoint(1, 29, 1, 9);
                Console.WriteLine("start:" + start + " end:" + end);

                MazeByBlock maze = new(10, 30, 60, start, end);
                maze.Show();
                Console.WriteLine();

                if (maze.Check(start, end))
                {
                    maze.FindWay(start, end, FindMode.AStar);
                    maze.Show(true);
                    Console.WriteLine();
                }
                else
                {
                    maze.way = new() { start, end };
                    maze.Show(true);
                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }
    }
}