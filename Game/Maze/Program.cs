namespace Maze
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Maze maze = new DepthFirstMaze(10, 10);
            maze.Generate();
            maze.Show();

            Console.ReadLine();
        }
    }
}