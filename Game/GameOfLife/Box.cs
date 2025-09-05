using System;
using System.Collections.Generic;
using Utils.Mathematical;

namespace GameOfLife
{
    public class Box
    {
        public bool[,] map;
        public bool[,] next;
        private bool Map(Point2D point) => map[point.Y, point.X];

        private int Height => map.GetLength(0);
        private int Width => map.GetLength(1);

        public Box()
        {
            map = new bool[1,1];
            next = new bool[1, 1];
        }

        public void Init(bool[,] seed)
        {
            map = (bool[,])seed.Clone();
            next = (bool[,])seed.Clone();
        }

        public void Init(int[,] seed)
        {
            int height = seed.GetLength(0);
            int width = seed.GetLength(1);
            map = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = seed[i, j] != 0;
                }
            }
            next = (bool[,])map.Clone();
        }

        public void RandomInit(int height, int width)
        {
            map = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = Random.Shared.Next(2) != 0;
                }
            }
            next = (bool[,])map.Clone();
        }

        public void RandomInit(int height, int width, int maxHeight, int maxWidth)
        {
            if (height > maxHeight) height = maxHeight;
            if (width > maxWidth) width = maxWidth;
            map = new bool[maxHeight, maxWidth];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[(maxHeight - height) / 2 + i, (maxWidth - width) / 2 + j] = Random.Shared.Next(2) != 0;
                }
            }
            next = (bool[,])map.Clone();
        }

        public void Update()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Point2D point = new(j, i);
                    int count = CountNeighbor(point);
                    if (Map(point))
                    {
                        if (count < 2 || count > 3)
                            next[i, j] = false;
                        else
                            next[i, j] = true;
                    }
                    else
                    {
                        if (count == 3)
                            next[i, j] = true;
                        else
                            next[i, j] = false;
                    }
                }
            }
            map = (bool[,])next.Clone();
        }

        public void Print()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (map[i, j])
                        Console.Write("■");
                    else
                        Console.Write("□");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        private int CountNeighbor(Point2D point)
        {
            if (map == null)
                return 0;

            List<Point2D> neighbors = new();
            if (point.X > 0)
            {
                neighbors.Add(point + new Point2D(-1, 0));
                if (point.Y > 0)
                    neighbors.Add(point + new Point2D(-1, -1));
                if (point.Y < Height - 1)
                    neighbors.Add(point + new Point2D(-1, 1));
            }
            if (point.X < Width - 1)
            {
                neighbors.Add(point + new Point2D(1, 0));
                if (point.Y > 0)
                    neighbors.Add(point + new Point2D(1, -1));
                if (point.Y < Height - 1)
                    neighbors.Add(point + new Point2D(1, 1));
            }
            if (point.Y > 0)
                neighbors.Add(point + new Point2D(0, -1));
            if (point.Y < Height - 1)
                neighbors.Add(point + new Point2D(0, 1));

            int count = 0;
            foreach (var neighbor in neighbors)
            {
                if (Map(neighbor))
                    count++;
            }
            return count;
        }
    }
}
