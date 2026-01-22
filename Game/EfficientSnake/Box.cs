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

        private Point2D NextMove()
        {
            // 策略1: 首先尝试找到食物的最短路径
            var foodPath = DfsFindPath();
            if (foodPath.Count > 0)
            {
                // 找到路径，检查第一步是否安全
                var firstStep = foodPath[0];
                if (Check(firstStep))
                {
                    // 额外检查：确保这一步不会导致死胡同
                    if (!WillCreateDeadEnd(firstStep))
                    {
                        return firstStep;
                    }
                }
            }

            // 策略2: 使用改进的哈密顿循环策略
            var hamiltonianDirection = HamiltonianCycleMove();
            var hamiltonianMove = Snake[0].Move(hamiltonianDirection);

            if (Check(hamiltonianMove) && !WillCreateDeadEnd(hamiltonianMove))
            {
                return hamiltonianMove;
            }

            // 策略3: 尝试所有可能的方向，选择不会立即导致死亡的方向
            foreach (var direction in GetMoveDirections())
            {
                var next = Head.Move(direction);
                if (Check(next) && !WillCreateDeadEnd(next))
                {
                    return next;
                }
            }

            // 策略4: 实在没有方向，返回向上（这可能会导致死亡）
            return Head.Move(RelativePosition_4.Up);
        }

        private List<Point2D> DfsFindPath()
        {
            List<Point2D> visited = [];
            Queue<(Point2D position, List<Point2D> path)> queue = new();
            queue.Enqueue((Head, new List<Point2D>()));
            while (queue.Count > 0)
            {
                var (currentPosition, path) = queue.Dequeue();
                if (currentPosition.Equals(Food))
                {
                    return path;
                }

                foreach (var direction in GetMoveDirections(currentPosition))
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
            int x = Head.X;
            int y = Head.Y;

            // 计算当前方向下还有多少空格可以访问
            // 优先选择能访问更多空格的方向

            // 尝试所有可能的方向，计算它们的"分数"
            var directionScores = new Dictionary<RelativePosition_4, int>();

            foreach (var direction in GetMoveDirections())
            {
                var next = Head.Move(direction);
                if (Check(next))
                {
                    // 计算从next位置可以访问的空格数量
                    int accessibleCells = CountAccessibleCells(next);
                    directionScores[direction] = accessibleCells;
                }
                else
                {
                    directionScores[direction] = -1;
                }
            }

            // 选择可访问空格最多的方向
            var bestDirection = directionScores
                .Where(kv => kv.Value >= 0)
                .OrderByDescending(kv => kv.Value)
                .FirstOrDefault();

            if (bestDirection.Value > 0)
                return bestDirection.Key;

            // 如果所有方向都无效，使用原来的哈密顿逻辑
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

        /// <summary>
        /// 计算从给定位置可以访问的空格数量（使用洪水填充）
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        private int CountAccessibleCells(Point2D start)
        {
            if (!Check(start))
                return 0;

            HashSet<Point2D> visited = [];
            Queue<Point2D> queue = new();

            queue.Enqueue(start);
            visited.Add(start);

            // 创建虚拟网格（假设蛇移动到了start位置）
            var virtualGrid = GetVirtualGrid();
            virtualGrid[start.X, start.Y] = true; // 标记新蛇头位置

            int count = 0;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                count++;

                foreach (var direction in GetMoveDirections(current))
                {
                    var next = current.Move(direction);

                    // 检查是否有效且未访问
                    if (Check(next, virtualGrid) && !visited.Contains(next))
                    {
                        visited.Add(next);
                        queue.Enqueue(next);
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// 判断移动是否会创建死胡同
        /// </summary>
        /// <param name="newHead"></param>
        /// <returns></returns>
        private bool WillCreateDeadEnd(Point2D newHead)
        {
            // 创建一个虚拟的蛇和网格来模拟移动后的状态
            var virtualSnake = new List<Point2D>(Snake);
            virtualSnake.Insert(0, newHead);

            // 如果吃到食物，蛇不会缩短
            if (!newHead.Equals(Food) && virtualSnake.Count > 1)
            {
                virtualSnake.RemoveAt(virtualSnake.Count - 1);
            }

            // 创建虚拟网格
            bool[,] virtualGrid = new bool[N, N];
            foreach (var point in virtualSnake)
            {
                virtualGrid[point.X, point.Y] = true;
            }

            // 计算从新蛇头位置可以访问的空格数量
            int accessibleCells = 0;
            HashSet<Point2D> visited = [];
            Queue<Point2D> queue = new();

            queue.Enqueue(newHead);
            visited.Add(newHead);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                accessibleCells++;

                foreach (var direction in GetMoveDirections(current))
                {
                    var next = current.Move(direction);

                    // 检查边界
                    if (next.X < 0 || next.X >= N || next.Y < 0 || next.Y >= N)
                        continue;

                    // 检查是否为空且未访问
                    if (!virtualGrid[next.X, next.Y] && !visited.Contains(next))
                    {
                        visited.Add(next);
                        queue.Enqueue(next);
                    }
                }
            }

            // 如果可访问的空格数量小于蛇需要增长的空间，则认为是死胡同
            int neededSpace = N * N - virtualSnake.Count;
            return accessibleCells < neededSpace;
        }

        /// <summary>
        /// 创建虚拟网格，标记蛇身位置（除了蛇尾，因为蛇尾会移动）
        /// </summary>
        /// <returns></returns>
        private bool[,] GetVirtualGrid()
        {
            bool[,] grid = new bool[N, N];

            // 标记蛇身位置（除了蛇尾）
            for (int i = 0; i < Snake.Count - 1; i++)
            {
                var point = Snake[i];
                grid[point.X, point.Y] = true;
            }

            return grid;
        }

        /// <summary>
        /// 检查移动是否有效
        /// </summary>
        /// <param name="point"></param>
        /// <param name="virtualGrid"></param>
        /// <returns></returns>
        private bool Check(Point2D point, bool[,] virtualGrid)
        {
            if (point.X < 0 || point.X >= N || point.Y < 0 || point.Y >= N)
                return false;
            return !virtualGrid[point.X, point.Y];
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
