using Maze.Base;
using Maze.Generate;
using Maze.WayFinding;

namespace Maze
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool test = true;
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
                MazeByBlock maze = new(10, 30, 20);
                maze.Show();
                Console.WriteLine();

                maze.FindWay(solveType: FindMode.AStar);
                maze.Show(true);
                Console.WriteLine();

                maze.FindWay(solveType: FindMode.DFSRTree);
                maze.Show(true);
                Console.WriteLine();
            }           

            Console.ReadLine();
        }
    }
}