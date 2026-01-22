using Maze.Base;
using System;
using System.Collections.Generic;
using Utils.Mathematical;
using Utils.Structure;

namespace Maze.WayFinding
{
    public class AStar : Find
    {
        /// <summary>起点结点</summary>
        private readonly Node<Point2D> root;

        /// <summary>终点结点</summary>
        private Node<Point2D> endNode = null;

        /// <summary>待计算的点</summary>
        private readonly List<Node<Point2D>> open;

        public AStar(MazeByWall maze) : base(maze)
        {
            root = new(start, null);
            open = new() { root };
        }

        public AStar(MazeByWall maze, Point2D start, Point2D end) : base(maze, start, end)
        {
            root = new(start, null);
            open = new() { root };
        }

        public AStar(MazeByBlock maze) : base(maze)
        {
            root = new(start, null);
            open = new() { root };
        }

        public AStar(MazeByBlock maze, Point2D start, Point2D end) : base(maze, start, end)
        {
            root = new(start, null);
            open = new() { root };
        }

        public override List<Point2D> FindWay()
        {
            FindEnd();
            List<Point2D> way = new();
            if (endNode == null)
            {
                return way;
            }
            else
            {
                while (endNode.Parent != null)
                {
                    way.Add(endNode.Current);
                    endNode = endNode.Parent;
                }
                way.Add(root.Current);
                way.Reverse();
                return way;
            }
        }

        private void FindEnd()
        {
            while (endNode == null)
            {
                Node<Point2D> minNode = GetMinNode();
                open.Remove(minNode);
                List<Point2D> relates = GetRelatedPoint(minNode.Current);
                if (relates.Count > 0)
                {
                    foreach (var point in relates)
                    {
                        minNode.AddChildren(point);
                        Mark(point);
                    }
                    foreach (var node in minNode.Children)
                    {
                        open.Add(node);
                        if (node.Current == end)
                        {
                            endNode = node;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 找到<see cref="Fn(Node{Point2D})"/>最小的点所在的结点
        /// </summary>
        private Node<Point2D> GetMinNode()
        {
            if (open.Count == 0)
            {
                throw new Exception("寻路失败");
            }
            int minFn = Fn(open[0]);
            int minHn = Hn(open[0]);
            Node<Point2D> minNode = open[0];
            foreach (var node in open)
            {
                if (Fn(node) < minFn)
                {
                    minFn = Fn(node);
                    minHn = Hn(node);
                    minNode = node;
                }
                else if (Fn(node) == minFn)
                {
                    if (Hn(node) < minHn)
                    {
                        minFn = Fn(node);
                        minHn = Hn(node);
                        minNode = node;
                    }
                }
            }
            return minNode;
        }

        /// <summary>
        /// 估价函数
        /// </summary>
        private int Fn(Node<Point2D> node)
        {
            return Gn(node) + Hn(node);
        }

        /// <summary>
        /// 从起点到某格的实际代价
        /// </summary>
        private static int Gn(Node<Point2D> node)
        {
            return node.Level;
        }

        /// <summary>
        /// 从某格到终点的估计代价
        /// </summary>
        private int Hn(Node<Point2D> node)
        {
            return Point2D.Manhattan(node.Current, end);
        }
    }
}
