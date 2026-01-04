using System;
using System.Collections.Generic;
using System.Threading;
using Utils.Mathematical;

namespace EfficientSnake
{
    public class Box(int n)
    {
        public List<Point2D> Snake { get; } = [new Point2D(0, 0)];
        public int N { get; } = n;
        public Point2D Food { get; set; }

        public void Init()
        {
            Food = GenerateFood();
        }

        public void Run()
        {
            while (Snake.Count < N * N)
            {
                Display();
                var nextMove = NextMove();
                if (!Move(nextMove))
                {
                    break;
                }
                Thread.Sleep(100);
            }
        }

        private void Display()
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    var point = new Point2D(x, y);
                    if (Snake[0].Equals(point))
                    {
                        Console.Write("#");
                    }
                    else if (Snake.Contains(point))
                    {
                        Console.Write("*");
                    }
                    else if (Food.Equals(point))
                    {
                        Console.Write("$");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        private Point2D GenerateFood()
        {
            var rand = new Random();
            Point2D food;
            do
            {
                int x = rand.Next(0, N);
                int y = rand.Next(0, N);
                food = new Point2D(x, y);
            } while (Snake.Contains(food));
            return food;
        }

        private bool Move(Point2D newHead)
        {
            if (!Check(newHead))
            {
                return false;
            }
            else
            {
                Snake.Insert(0, newHead);
                if (newHead.Equals(Food))
                {
                    Food = GenerateFood();
                }
                else
                {
                    Snake.RemoveAt(Snake.Count - 1);
                }
                return true;
            }
        }

        private List<Point2D> FindPath()
        {
            List<Point2D> visited = [];
            Queue<(Point2D position, List<Point2D> path)> queue = new();
            queue.Enqueue((Snake[0], new List<Point2D>()));
            while (queue.Count > 0)
            {
                var (currentPosition, path) = queue.Dequeue();
                if (currentPosition.Equals(Food))
                {
                    return path;
                }
                foreach (var direction in new List<RelativePosition_4>
                {
                    RelativePosition_4.Up,
                    RelativePosition_4.Down,
                    RelativePosition_4.Left,
                    RelativePosition_4.Right
                })
                {
                    var newPosition = currentPosition.Move(direction);
                    if (!visited.Contains(newPosition) && Check(newPosition))
                    {
                        visited.Add(newPosition);
                        var newPath = new List<Point2D>(path) { newPosition };
                        queue.Enqueue((newPosition, newPath));
                    }
                }
            }
            return [];
        }

        private RelativePosition_4 HamiltonianCycleMove()
        {
            int x = Snake[0].X;
            int y = Snake[0].Y;
            // 如果当前在最上方一行且不是最右列，向右移动
            if (y == 0 && x < N - 1)
                return RelativePosition_4.Right;
            // 如果当前在最下方一行且不是最左列，向左移动
            else if (y == N - 1 && x > 0)
                return RelativePosition_4.Left;
            // 如果在偶数行且在最右列，向下移动
            else if (y % 2 == 0 && x == N - 1)
                return RelativePosition_4.Down;
            // 如果在奇数行且在最左列，向上移动
            else if (y % 2 == 1 && x == 0)
                return RelativePosition_4.Up;
            // 如果在偶数行，向右移动
            else if (y % 2 == 0)
                return RelativePosition_4.Right;
            // 如果在奇数行，向左移动
            else
                return RelativePosition_4.Left;
        }

        public Point2D NextMove()
        {
            var findPath = FindPath();
            if (findPath?.Count > 0)
            {
                return findPath[0];
            }

            var directionH = HamiltonianCycleMove();
            var nextPoint = Snake[0].Move(directionH);
            if (Check(nextPoint))
            {
                return nextPoint;
            }

            foreach (var direction in new List<RelativePosition_4>
                {
                    RelativePosition_4.Up,
                    RelativePosition_4.Down,
                    RelativePosition_4.Left,
                    RelativePosition_4.Right
                })
            {
                nextPoint = Snake[0].Move(direction);
                if (Check(nextPoint))
                {
                    return nextPoint;
                }
            }

            return Snake[0].Move(RelativePosition_4.Up);
        }

        private bool Check(Point2D point)
        {
            if (Snake.Contains(point)
                || point.X < 0 || point.X >= N
                || point.Y < 0 || point.Y >= N)
            {
                return false;
            }
            return true;
        }
    }
}
