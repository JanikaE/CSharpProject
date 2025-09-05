using Maze.Base;
using Maze.WayFinding;
using System;
using Utils.Mathematical;
using Utils.Tool;

namespace Maze
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                int testType = 1;
                MazeByWall maze;
                MazeByBlock maze2;
                switch (testType)
                {
                    case 1:
                        maze = new DepthFirst(10, 30);
                        maze.Generate();
                        maze.Show();
                        Console.WriteLine();

                        maze.FindWay(new(0, 0), new(29, 10), FindMode.AStar);
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
                }
            }
            catch (Exception e)
            {
                LogTool.Error(e);
            }

            Console.ReadLine();
        }
    }
}