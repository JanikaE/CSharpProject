using System;
using System.Collections.Generic;
using System.Linq;
using Utils.Mathematical;

namespace Minesweeper
{
    public class Box
    {
        private readonly int height;
        private readonly int width;
        private readonly int boomNum;

        private Map2D<bool> isBoom;
        private Map2D<bool> isMark;
        private Map2D<bool> isOpen;

        private Map2D<int> surroundNum;

        public State State { get; private set; }

        public static readonly List<Point2D> surroundBlock = new ()
        {  
            new(-1, -1),
            new(-1, 0), 
            new(-1, 1),
            new(0, -1),
            new(0, 1),
            new(1, -1),  
            new(1, 0),
            new(1, 1),
        };

        public Box(int height, int width, int boomNum)
        {
            if (height <= 0 || width <= 0 || boomNum <= 0)
                throw new ArgumentException("宽高和雷数不能为负数或0");
            if (boomNum > height * width)
                throw new ArgumentException("雷数过多");
            this.height = height;
            this.width = width;
            this.boomNum = boomNum;
            isBoom = new Map2D<bool>(height, width);
            isMark = new Map2D<bool>(height, width);
            isOpen = new Map2D<bool>(height, width);
            surroundNum = new Map2D<int>(height, width);
            State = State.None;
        }

        private void Init()
        {
            isBoom = new Map2D<bool>(height, width);
            isMark = new Map2D<bool>(height, width);
            isOpen = new Map2D<bool>(height, width);
            surroundNum = new Map2D<int>(height, width);
            State = State.None;
        }

        private void Generate()
        {
            Random random = Random.Shared;
            for (int i = 0; i < boomNum; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(width);
                    y = random.Next(height);
                } while (isBoom[y, x]);
                isBoom[y, x] = true;
            }
        }

        /// <summary>
        /// 开始新游戏
        /// </summary>
        public void StartNew()
        {
            Init();
            Generate();
            State = State.Continue;
        }

        /// <summary>
        /// 在不改变雷位置的情况下重新开始
        /// </summary>
        public void Restart()
        {
            isMark = new Map2D<bool>(height, width);
            isOpen = new Map2D<bool>(height, width);
            surroundNum = new Map2D<int>(height, width);
        }

        /// <summary>
        /// 打开一格
        /// </summary>
        /// <returns>true胜利，false失败，null继续</returns>
        public void Open(int x, int y)
        {
            Point2D point = new(x, y);
            if (!CheckRange(point))
                throw new ArgumentException("输入超出范围");
            if (State != State.Continue)
                return;
            if (isBoom[y, x])
            {
                State = State.Fail;
                return;
            }               
            if (isMark[y, x] || isOpen[y, x])
                return;

            // 计算周围8格雷数
            int boomNum = 0;
            foreach (Point2D p in surroundBlock)
            {
                Point2D sur = point + p;
                if (!CheckRange(sur))
                    continue;
                if (isBoom[sur])
                    boomNum++;
            }
            surroundNum[y, x] = boomNum;
            isOpen[y, x] = true;

            // 如果雷数为0则打开周围8格
            if (boomNum == 0)
            {
                foreach (Point2D p in surroundBlock)
                {
                    Point2D sur = point + p;
                    if (!CheckRange(sur))
                        continue;
                    Open(sur);
                }
            }

            if (CheckVictory() && State == State.Continue)
                State = State.Victory;
        }

        private void Open(Point2D point)
        {
            Open(point.X, point.Y);
        }

        /// <summary>
        /// 标记一格
        /// </summary>
        public void Mark(int x, int y)
        {
            if (!CheckRange(x, y))
                throw new ArgumentException("输入超出范围");
            if (State != State.Continue)
                return;
            if (isOpen[y, x])
                return;
            isMark[y, x] = !isMark[y, x];
        }

        /// <summary>
        /// 双键
        /// </summary>
        public void Double(int x, int y)
        {
            Point2D point = new(x, y);
            if (!CheckRange(point))
                throw new ArgumentException("输入超出范围");
            if (State != State.Continue)
                return;
            if (!isOpen[y, x])
                return;

            // 计算周围8格标记的个数
            int markNum = 0;
            foreach(Point2D p in surroundBlock)
            {
                Point2D sur = point + p;
                if (!CheckRange(sur))
                    continue;
                if (!isOpen[sur] && isMark[sur])
                    markNum++;
            }

            // 标记的个数和雷数一样则打开未标记的格子
            if (surroundNum[y, x] == markNum)
            {
                foreach (Point2D p in surroundBlock)
                {
                    Point2D sur = point + p;
                    if (!CheckRange(sur))
                        continue;
                    if (!isOpen[sur] && !isMark[sur])
                        Open(sur);
                }
            }
        }

        private bool CheckVictory()
        {
            int notOpen = 0;
            foreach (bool b in isOpen)
            {
                if (b)
                    notOpen++;
            }
            return notOpen == boomNum;
        }

        private bool CheckRange(Point2D point)
        {
            if (point.X < 0 || point.Y < 0 || point.X >= width || point.Y >= height) 
                return false;
            else
                return true;
        }

        private bool CheckRange(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
                return false;
            else
                return true;
        }
        
        /// <summary>
        /// 终端显示游戏画面
        /// </summary>
        public void Show()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(ShowBlock(x, y));
                }
                Console.Write("\n");
            }
        }

        private char ShowBlock(int x, int y)
        {
            if (isOpen[y, x])
            {
                if (surroundNum[y, x] == 0)
                    return Config.zero;
                else
                    return surroundNum[y, x].ToString().First();
            }
            else if (isMark[y, x])
            {
                return Config.mark;
            }
            else if (isBoom[y, x])
            {
                if (State == State.Continue)
                {
                    return Config.unknown;
                }
                else 
                { 
                    return Config.boom; 
                }
            }
            else
            {
                return Config.unknown;
            }
        }
    }
}
