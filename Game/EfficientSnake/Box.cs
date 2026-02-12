using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Utils.Mathematical;

namespace EfficientSnake
{
    public class Box(int n)
    {
        public List<Point2D> Snake { get; } = [new Point2D(0, 0)];
        public Point2D Head => Snake[0];
        public int N { get; } = n;
        public Point2D Food { get; set; }

        public void Run()
        {
            Food = GenerateFood();
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
            Display();
        }

        private void Display()
        {
            Console.SetCursorPosition(0, 0);
            for (int y = -1; y < N + 1; y++)
            {
                for (int x = -1; x < N + 1; x++)
                {
                    if (y == -1 || y == N || x == -1 || x == N)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        var point = new Point2D(x, y);
                        if (Head.Equals(point))
                        {
                            Console.Write("%");
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
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        private Point2D GenerateFood()
        {
            if (Snake.Count == N * N)
            {
                return new Point2D(-1, -1);
            }
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
            if (!Check(newHead) && Snake.Last() != newHead)
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

        private Point2D NextMove()
        {
            // 1. 尝试最短路径吃食物
            var pathToFood = FindPath(Head, Food, Snake);

            if (pathToFood != null && pathToFood.Count > 1)
            {
                var next = pathToFood[1];

                // 模拟走这一步
                var simulatedSnake = SimulateMove(next, Snake, eatsFood: next.Equals(Food));

                // 安全性检查：新头是否还能到达尾巴
                var tail = simulatedSnake[^1];
                var pathToTail = FindPath(simulatedSnake[0], tail, simulatedSnake, ignoreTail: true);

                if (pathToTail != null)
                {
                    return next;
                }
            }

            // 2. 兜底：朝蛇尾方向走
            var tailPoint = Snake[^1];
            var pathToTailFallback = FindPath(Head, tailPoint, Snake, ignoreTail: true);

            if (pathToTailFallback != null && pathToTailFallback.Count > 1)
            {
                return pathToTailFallback[1];
            }

            // 3. 最坏情况：随便找一个能走的方向（理论上不会发生）
            foreach (var dir in GetMoveDirections())
            {
                var next = Head.Move(dir);
                if (Check(next))
                    return next;
            }

            return Head; // 不动（保险）
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

        private List<Point2D> FindPath(
            Point2D start,
            Point2D target,
            List<Point2D> snake,
            bool ignoreTail = false)
        {
            var queue = new Queue<Point2D>();
            var prev = new Dictionary<Point2D, Point2D?>();
            var blocked = new HashSet<Point2D>(snake);

            if (ignoreTail && snake.Count > 0)
                blocked.Remove(snake[^1]);

            queue.Enqueue(start);
            prev[start] = null;

            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                if (cur.Equals(target))
                    break;

                foreach (var d in GetMoveDirections(cur))
                {
                    var next = cur.Move(d);
                    if (next.X < 0 || next.X >= N || next.Y < 0 || next.Y >= N)
                        continue;
                    if (blocked.Contains(next))
                        continue;
                    if (prev.ContainsKey(next))
                        continue;

                    prev[next] = cur;
                    queue.Enqueue(next);
                }
            }

            if (!prev.ContainsKey(target))
                return null;

            // 回溯路径
            var path = new List<Point2D>();
            for (Point2D? p = target; p != null; p = prev[p.Value])
                path.Add(p.Value);

            path.Reverse();
            return path;
        }

        private List<Point2D> SimulateMove(
            Point2D newHead,
            List<Point2D> snake,
            bool eatsFood)
        {
            var result = new List<Point2D>(snake);
            result.Insert(0, newHead);

            if (!eatsFood)
                result.RemoveAt(result.Count - 1);

            return result;
        }

        private List<RelativePosition_4> GetMoveDirections()
        {
            return GetMoveDirections(Head);
        }

        private static List<RelativePosition_4> GetMoveDirections(Point2D point)
        {
            //var directions = new List<RelativePosition_4>()
            //{
            //    RelativePosition_4.Up,
            //    RelativePosition_4.Down,
            //    RelativePosition_4.Left,
            //    RelativePosition_4.Right
            //};
            var directions = new List<RelativePosition_4>();
            if (point.Y % 2 == 0)
            {
                directions.Add(RelativePosition_4.Right);
            }
            else
            {
                directions.Add(RelativePosition_4.Left);
            }
            if (point.X % 2 == 0)
            {
                directions.Add(RelativePosition_4.Up);
            }
            else
            {
                directions.Add(RelativePosition_4.Down);
            }
            return directions;
        }
    }
}
