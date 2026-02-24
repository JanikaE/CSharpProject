using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Utils.Mathematical
{
    /// <summary>
    /// 0→x <br/>
    /// ↓<br/>
    /// y
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Map2D<T> : IEnumerable<T>
    {
        private readonly T[,] data;
        public int Height => data.GetLength(0);
        public int Width => data.GetLength(1);

        public T this[int y, int x]
        {
            get
            {
                if (y < 0 || y > Height)
                {
                    throw new IndexOutOfRangeException($"Y index {y} is out of range (0 to {Height - 1})");
                }
                if (x < 0 || x > Width)
                {
                    throw new IndexOutOfRangeException($"X index {x} is out of range (0 to {Width - 1})");
                }
                return data[y, x];
            }
            set
            {
                if (y < 0 || y > Height)
                {
                    throw new IndexOutOfRangeException($"Y index {y} is out of range (0 to {Height - 1})");
                }
                if (x < 0 || x > Width)
                {
                    throw new IndexOutOfRangeException($"X index {x} is out of range (0 to {Width - 1})");
                }
                data[y, x] = value;
            }
        }

        public T this[Point2D point]
        {
            get => this[point.Y, point.X];
            set => this[point.Y, point.X] = value;
        }

        public Map2D(T[,] data)
        {
            this.data = data;
        }

        public Map2D(int height, int width, T defaultValue = default)
        {
            data = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[i, j] = defaultValue;
                }
            }
        }

        public Map2D(int height, int width, Func<int, int, T> func)
        {
            data = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[i, j] = func(i, j);
                }
            }
        }

        public static Map2D<TTarget> ToMap2D<TSource, TTarget>(TSource[,] data, Func<TSource, TTarget> func)
        {
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            var target = new TTarget[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    target[i, j] = func(data[i, j]);
                }
            }
            return new Map2D<TTarget>(target);
        }

        public Map2D<T> Clone()
        {
            T[,] newData = new T[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    newData[i, j] = data[i, j];
                }
            }
            return new Map2D<T>(newData);
        }

        public bool IsValidPoint(Point2D point)
        {
            return point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
        }

        #region IEnumerable Implementation

        public IEnumerator<T> GetEnumerator()
        {
            var list = new List<T>();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    list.Add(data[i, j]);
                }
            }
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// 蛇形排序
        /// </summary>
        /// <param name="comparison"></param>
        /// <param name="isAsc"></param>
        public void Sort(Comparison<T> comparison, bool isAsc)
        {
            // 对每行进行排序
            for (int i = 0; i < Height; i++)
            {
                // 偶数行：从左到右排序
                if (i % 2 == 0)
                {
                    for (int j = 0; j < Width - 1; j++)
                    {
                        for (int k = 0; k < Width - j - 1; k++)
                        {
                            if (isAsc ? comparison(data[i, k], data[i, k + 1]) > 0 : comparison(data[i, k], data[i, k + 1]) < 0)
                            {
                                (data[i, k], data[i, k + 1]) = (data[i, k + 1], data[i, k]);
                            }
                        }
                    }
                }
                // 奇数行：从右到左排序
                else
                {
                    for (int j = 0; j < Width - 1; j++)
                    {
                        for (int k = 0; k < Width - j - 1; k++)
                        {
                            if (isAsc ? comparison(data[i, Width - k - 2], data[i, Width - k - 1]) > 0 : comparison(data[i, Width - k - 2], data[i, Width - k - 1]) < 0)
                            {
                                (data[i, Width - k - 2], data[i, Width - k - 1]) = (data[i, Width - k - 1], data[i, Width - k - 2]);
                            }
                        }
                    }
                }
            }
            // 对每列进行排序
            for (int j = 0; j < Width; j++)
            {
                for (int i = 0; i < Height - 1; i++)
                {
                    for (int k = 0; k < Height - i - 1; k++)
                    {
                        if (isAsc ? comparison(data[k, j], data[k + 1, j]) > 0 : comparison(data[k, j], data[k + 1, j]) < 0)
                        {
                            (data[k, j], data[k + 1, j]) = (data[k + 1, j], data[k, j]);
                        }
                    }
                }
            }
        }
    }
}
