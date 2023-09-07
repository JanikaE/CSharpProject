using Utils.Mathematical;

namespace Maze
{
    public abstract class Maze
    {
        public int height;
        public int width;

        public bool[,] wall_vertical;
        public bool[,] wall_horizontal;

        public Maze(int height, int width)
        {
            this.height = height;
            this.width = width;
            wall_vertical = new bool[height, width];
            wall_horizontal = new bool[height, width];
        }

        public abstract void Generate();

        public void Show()
        {
            for (int i = 0; i < width * 2 + 1; i++)
            {
                Console.Write('#');
            }
            Console.Write('\n');
            for (int i = 0; i < height; i++)
            {
                Console.Write('#');
                for (int j = 0; j < width; j++)
                {
                    Console.Write(' ');
                    if (wall_vertical[i, j])
                        Console.Write(' ');
                    else
                        Console.Write('#');
                }
                Console.Write('\n');
                Console.Write('#');
                for (int j = 0; j < width; j++)
                {
                    if (wall_horizontal[i, j])
                        Console.Write(' ');
                    else
                        Console.Write('#');
                    Console.Write('#');
                }
                Console.Write('\n');
            }
        }

        /// <summary>
        /// 获取某格的四周的格子
        /// </summary>
        public List<Point2D> GetNeighborBlocks(int x, int y)
        {
            List<Point2D> points = new();
            if (x > 0)
                points.Add(new(x - 1, y));
            if (x < width - 1)
                points.Add(new(x + 1, y));
            if (y > 0)
                points.Add(new(x, y - 1));
            if (y < height - 1)
                points.Add(new(x, y + 1));
            return points;
        }
        
        /// <summary>
        /// 打通两相邻格之间的墙
        /// </summary>
        public void BreakWall(Point2D point1, Point2D point2)
        {
            int x = point1.X;
            int y = point1.Y;
            switch (GetRelativePosition(point1, point2))
            {
                case RelativePosition.Left:
                    wall_vertical[y, x - 1] = true;
                    break;
                case RelativePosition.Right:
                    wall_vertical[y, x] = true;
                    break;
                case RelativePosition.Up:
                    wall_horizontal[y - 1, x] = true;
                    break;
                case RelativePosition.Down:
                    wall_horizontal[y, x] = true;
                    break;
            }
        }

        /// <summary>
        /// 计算两个相邻格子的相对位置
        /// </summary>
        public static RelativePosition GetRelativePosition(Point2D me, Point2D another)
        {
            if (another.X - me.X == 1 && another.Y == me.Y)
                return RelativePosition.Right;
            if (another.X - me.X == -1 && another.Y == me.Y)
                return RelativePosition.Left;
            if (another.X == me.X && another.Y - me.Y == 1)
                return RelativePosition.Down;
            if (another.X == me.X && another.Y - me.Y == -1)
                return RelativePosition.Up;
            return RelativePosition.None;
        }
    }
}
