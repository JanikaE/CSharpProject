using System;
using System.Collections.Generic;
using Utils.Mathematical;

namespace GameOfLife
{
    public class Box
    {
        public Map2D<bool> map;
        public Map2D<bool> next;

        private int Height => map.Width;
        private int Width => map.Height;

        public Box()
        {
            map = new Map2D<bool>(1, 1);
            next = new Map2D<bool>(1, 1);
        }

        public void Init(bool[,] seed)
        {
            map = new Map2D<bool>(seed);
            next = new Map2D<bool>(seed);
        }

        public void Init(int[,] seed)
        {
            map = Map2D<bool>.ToMap2D(seed, (n) => n == 1);
            next = map.Clone();
        }

        public void RandomInit(int height, int width)
        {
            map = new Map2D<bool>(height, width);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = Random.Shared.Next(2) != 0;
                }
            }
            next = map.Clone();
        }

        public void RandomInit(int height, int width, int maxHeight, int maxWidth)
        {
            if (height > maxHeight) height = maxHeight;
            if (width > maxWidth) width = maxWidth;
            map = new Map2D<bool>(maxHeight, maxWidth);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[(maxHeight - height) / 2 + i, (maxWidth - width) / 2 + j] = Random.Shared.Next(2) != 0;
                }
            }
            next = map.Clone();
        }

        public void Update()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Point2D point = new(j, i);
                    int count = CountNeighbor(point);
                    if (map[point])
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
            map = next.Clone();
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
                if (map[neighbor])
                    count++;
            }
            return count;
        }
    }
}
