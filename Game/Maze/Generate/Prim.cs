using Maze.Base;
using System;
using System.Collections.Generic;
using Utils.Mathematical;

namespace Maze.Generate
{
    /// <summary>
    /// 随机Prim算法
    /// </summary>
    public class Prim : MazeByWall
    {
        public Map2D<bool> canReach;
        public bool CanReach(Point2D point) => canReach[point];
        /// <summary>已连通的格子数</summary>
        private int reachNum;
        /// <summary>所有格子都连通时判定迷宫生成完成</summary>
        private bool IsComplete() => reachNum == height * width;

        public Prim(int height, int width) : base(height, width)
        {
            canReach = new Map2D<bool>(height, width);
            canReach[0, 0] = true;
            reachNum = 1;
        }

        public override void Generate()
        {
            while (!IsComplete())
            {
                // 随机选取一个四周有未连通格子的已连通格子
                Point2D point = GetRandomEdgeBlock();
                List<Point2D> points = GetNeighborUnreachedBlocks(point);
                // 随机选择一个邻格并打通
                Point2D newPoint = points[Random.Shared.Next(points.Count)];
                BreakWall(point, newPoint);
                canReach[newPoint] = true;
                reachNum++;
            }
        }

        /// <summary>
        /// 获取某格的四周的格子中未连通的格子
        /// </summary>
        protected List<Point2D> GetNeighborUnreachedBlocks(Point2D point)
        {
            List<Point2D> points = GetNeighborBlocks(point.X, point.Y);
            List<Point2D> subPoints = new();
            foreach (Point2D p in points)
            {
                if (!CanReach(p))
                    subPoints.Add(p);
            }
            return subPoints;
        }

        /// <summary>
        /// 随机获取一格已联通的格子，其四周至少有一个格子未连通
        /// </summary>
        private Point2D GetRandomEdgeBlock()
        {
            List<Point2D> points = new();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (IsEdge(i, j))
                    {
                        points.Add(new(i, j));
                    }
                }
            }
            return points[Random.Shared.Next(points.Count)];
        }

        /// <summary>
        /// 判断四周是否有未连通格子且自身是否已连通
        /// </summary>
        private bool IsEdge(int x, int y)
        {
            if (!canReach[y, x])
                return false;
            foreach (Point2D p in GetNeighborBlocks(x, y))
            {
                if (!CanReach(p))
                    return true;
            }
            return false;
        }
    }
}
