using Utils.Mathematical;
using Utils.Structure;

namespace Maze
{
    /// <summary>
    /// 迷宫寻路（广度优先）
    /// </summary>
    public class Solve
    {
        private readonly Maze maze;
        private readonly Node<Point2D> root;
        private Node<Point2D>? end;

        private readonly bool[,] isMark;
        private bool IsMark(Point2D p) => isMark[p.Y, p.X];
        private void Mark(Point2D p) => isMark[p.Y, p.X] = true;

        public Solve(Maze maze)
        {
            this.maze = maze;
            isMark = new bool[maze.height, maze.width];
            root = new(new(0, 0), null);
            isMark[0, 0] = true;
            end = null;
        }

        public List<Point2D> FindWay()
        {
            FindEnd(root);
            List<Point2D> way = new();
            if (end == null)
            {
                return way;
            }
            else
            {
                while (end.Parent != null)
                {
                    way.Add(end.Current);
                    end = end.Parent;
                }
                way.Add(root.Current);
                way.Reverse();
                return way;
            }
        }

        private void FindEnd(Node<Point2D> start)
        {
            List<Point2D> relates = GetRelatedPoint(start.Current);
            if (relates.Count == 0)
            {
                return;
            }
            foreach (Point2D p in relates)
            {
                start.AddChildren(p);
            }
            foreach (Node<Point2D> node in start.Children)
            {
                FindEnd(node);
                Point2D p = node.Current;
                if (p.X == maze.width - 1 && p.Y == maze.height - 1)
                {
                    end = node;
                }
            }
        }

        private List<Point2D> GetRelatedPoint(Point2D p)
        {
            List<Point2D> result = new();
            int x = p.X;
            int y = p.Y;
            if (x > 0)
            {
                Point2D left = new(x - 1, y);
                if (!IsMark(left) && maze.wall_vertical[y, x - 1]) 
                {
                    result.Add(left);
                    Mark(left);
                }
            }
            if (x < maze.width - 1)
            {
                Point2D right = new(x + 1, y);
                if (!IsMark(right) && maze.wall_vertical[y, x])
                {
                    result.Add(right);
                    Mark(right);
                } 
            }
            if (y > 0)
            {
                Point2D up = new(x, y - 1);
                if (!IsMark(up) && maze.wall_horizontal[y - 1, x])
                {
                    result.Add(up);
                    Mark(up);
                }
            }
            if (y < maze.height - 1)
            {
                Point2D down = new(x, y + 1);
                if (!IsMark(down) && maze.wall_horizontal[y, x])
                {
                    result.Add(down);
                    Mark(down);
                }
            }
            return result;
        }
    }
}
