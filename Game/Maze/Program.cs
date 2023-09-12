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
            maze.Show(true);

            Console.ReadLine();
        }
    }
}