using Maze.WayFinding;

namespace Maze
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Maze maze = new DepthFirstMaze(10, 30);
            maze.Generate();
            maze.Show();
            Console.WriteLine();
            maze.FindWay(new(2, 2), new(5, 5), FindMode.DFS);
            maze.Show(true);

            Console.ReadLine();
        }
    }
}