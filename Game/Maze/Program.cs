using Maze.Base;
using Maze.Generate;
using Maze.WayFinding;

namespace Maze
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MazeByWall maze = new DepthFirstMaze(10, 30);
            maze.Generate();
            maze.Show();
            Console.WriteLine();

            maze.FindWay(new(0, 0), new(29, 9), FindMode.DFS);
            maze.Show(true);
            Console.WriteLine();

            MazeByBlock maze2 = new(maze);
            maze2.Show();
            Console.WriteLine();

            maze2.FindWay(solveType: FindMode.DFSRTree);
            maze2.Show(true);

            Console.ReadLine();
        }
    }
}