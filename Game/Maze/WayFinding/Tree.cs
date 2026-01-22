using Utils.Mathematical;
using Utils.Structure;
using Maze.Base;
using System.Collections.Generic;

namespace Maze.WayFinding
{
    /// <summary>
    /// 树
    /// </summary>
    public class Tree : Find
    {
        private readonly Node<Point2D> root;
        private Node<Point2D> endNode;
        private readonly TreeType tree;

        private readonly Queue<Node<Point2D>> queue = new();
        private readonly Stack<Node<Point2D>> stack = new();

        public Tree(MazeByWall maze, TreeType tree) : base(maze)
        {
            this.tree = tree;
            root = new(start, null);
            endNode = null;
        }

        public Tree(MazeByWall maze, Point2D start, Point2D end, TreeType tree) : base(maze, start, end)
        {
            this.tree = tree;
            root = new(start, null);
            endNode = null;
        }

        public Tree(MazeByBlock maze, TreeType tree) : base(maze)
        {
            this.tree = tree;
            root = new(start, null);
            endNode = null;
        }

        public Tree(MazeByBlock maze, Point2D start, Point2D end, TreeType tree) : base(maze, start, end)
        {
            this.tree = tree;
            root = new(start, null);
            endNode = null;
        }

        /// <summary>
        /// 从终点节点开始，循环找父节点直到根节点
        /// </summary>
        public override List<Point2D> FindWay()
        {
            ToTree(root);
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

        /// <summary>
        /// 将迷宫转为树
        /// </summary>
        private void ToTree(Node<Point2D> start)
        {
            switch (tree)
            {
                case TreeType.BFS:
                    BFS(start);
                    break;
                case TreeType.DFS:
                    DFS(start);
                    break;
                case TreeType.DFSR:
                    DFSR(start);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 广度优先（队列）
        /// </summary>
        private void BFS(Node<Point2D> start)
        {
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                Node<Point2D> now = queue.Dequeue();
                List<Point2D> relates = GetRelatedPoint(now.Current);
                if (relates.Count > 0)
                {
                    foreach (Point2D p in relates)
                    {
                        now.AddChildren(p);
                        Mark(p);
                    }
                    foreach (Node<Point2D> node in now.Children)
                    {
                        queue.Enqueue(node);
                        // 记录终点节点
                        Point2D p = node.Current;
                        if (p == end)
                        {
                            endNode = node;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 深度优先（栈）
        /// </summary>
        private void DFS(Node<Point2D> start)
        {
            stack.Push(start);
            while (stack.Count > 0)
            {
                Node<Point2D> now = stack.Pop();
                List<Point2D> relates = GetRelatedPoint(now.Current);
                if (relates.Count > 0)
                {
                    foreach (Point2D p in relates)
                    {
                        now.AddChildren(p);
                        Mark(p);
                    }
                    foreach (Node<Point2D> node in now.Children)
                    {
                        stack.Push(node);
                        // 记录终点节点
                        Point2D p = node.Current;
                        if (p == end)
                        {
                            endNode = node;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 深度优先（递归）
        /// </summary>
        private void DFSR(Node<Point2D> start)
        {
            List<Point2D> relates = GetRelatedPoint(start.Current);
            if (relates.Count == 0)
            {
                return;
            }
            foreach (Point2D p in relates)
            {
                start.AddChildren(p);
                Mark(p);
            }
            foreach (Node<Point2D> node in start.Children)
            {
                ToTree(node);
                // 记录终点节点
                Point2D p = node.Current;
                if (p == end)
                {
                    endNode = node;
                }
            }
        }
    }

    /// <summary>
    /// 建树的方法
    /// </summary>
    public enum TreeType
    {
        /// <summary>广度优先（队列）</summary>
        BFS,

        /// <summary>深度优先（栈）</summary>
        DFS,

        /// <summary>深度优先（递归）</summary>
        DFSR,
    }
}
