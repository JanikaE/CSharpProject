using Maze.Base;
using System;

namespace Maze.Generate
{
    /// <summary>
    /// 十字递归分割算法
    /// </summary>
    public class CrossRecursion : MazeByWall
    {
        public CrossRecursion(int height, int width) : base(height, width)
        {
        }

        public override void Generate()
        {
            SubGenerate(0, 0, width - 1, height - 1);
        }

        private void SubGenerate(int startX, int startY, int endX, int endY)
        {
            if (startX == endX)
            {
                for (int i = startY; i < endY; i++)
                {
                    wall_horizontal[i, startX] = true;
                }
                return;
            }
            if (startY == endY)
            {
                for (int i = startX; i < endX; i++)
                {
                    wall_vertical[startY, i] = true;
                }
                return;
            }

            int midX = Random.Shared.Next(startX, endX);
            int midY = Random.Shared.Next(startY, endY);
            // 随机指定4面墙中哪一面不留门
            int noWall = Random.Shared.Next(4);
            // 上
            if (noWall != 0)
            {
                int i = Random.Shared.Next(startY, midY + 1);
                wall_vertical[i, midX] = true;
            }
            // 下
            if (noWall != 1)
            {
                int i = Random.Shared.Next(midY + 1, endY + 1);
                wall_vertical[i, midX] = true;
            }
            // 左
            if (noWall != 2)
            {
                int i = Random.Shared.Next(startX, midX + 1);
                wall_horizontal[midY, i] = true;
            }
            // 右
            if (noWall != 3)
            {
                int i = Random.Shared.Next(midX + 1, endX + 1);
                wall_horizontal[midY, i] = true;
            }

            SubGenerate(startX, startY, midX, midY);
            SubGenerate(midX + 1, startY, endX, midY);
            SubGenerate(startX, midY + 1, midX, endY);
            SubGenerate(midX + 1, midY + 1, endX, endY);
        }
    }
}
