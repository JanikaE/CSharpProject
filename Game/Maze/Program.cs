using Maze.Base;
using Maze.WayFinding;
using Utils.Extend;
using Utils.Mathematical;

namespace Maze
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int testType = 3;
            MazeByWall maze;
            MazeByBlock maze2;
            switch (testType) 
            { 
                case 1:
                    maze = new DepthFirst(10, 30);
                    maze.Generate();
                    maze.Show();
                    Console.WriteLine();

                    maze.FindWay(new(0, 0), new(29, 9), FindMode.AStar);
                    maze.Show(true);
                    Console.WriteLine();

                    maze2 = new(maze);
                    maze2.Show();
                    Console.WriteLine();

                    maze2.FindWay(solveType: FindMode.AStar);
                    maze2.Show(true);
                    break;
                case 2:
                    Point2D start = Point2D.RandomPoint(1, 29, 1, 9);
                    Point2D end = Point2D.RandomPoint(1, 29, 1, 9);
                    Console.WriteLine("start:" + start + " end:" + end);

                    maze2 = new(10, 30, 60, start, end);
                    maze2.Show();
                    Console.WriteLine();

                    if (maze2.Check(start, end))
                    {
                        maze2.FindWay(start, end, FindMode.AStar);
                        maze2.Show(true);
                        Console.WriteLine();
                    }
                    else
                    {
                        maze2.way = new() { start, end };
                        maze2.Show(true);
                        Console.WriteLine();
                    }
                    break;
                case 3:
                    List<Point2D> list = new();
                    for (int i = 0; i < 20; i++)
                    {
                        list.Add(Point2D.RandomPoint(3, 3));
                    }
                    Console.WriteLine(list.ToStringByItem());
                    list.SortAndDeduplicate((a, b) =>
                    {
                        return (a.X * 10 + a.Y).CompareTo(b.X * 10 + b.Y);
                    });
                    Console.WriteLine(list.ToStringByItem());
                    break;
            }

            Console.ReadLine();
        }
    }
}