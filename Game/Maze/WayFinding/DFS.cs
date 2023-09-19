using Maze.Base;
using Utils.Mathematical;

namespace Maze.WayFinding
{
    /// <summary>
    /// 深度优先
    /// </summary>
    public class DFS : Find
    {
        private readonly Stack<Point2D> stack = new();

        public DFS(MazeByWall maze, Point2D start, Point2D end) : base(maze, start, end)
        {
        }

        public DFS(MazeByBlock maze) : base(maze) 
        { 
        }

        public override List<Point2D> FindWay()
        {
            stack.Push(start);
            while (stack.Count > 0)
            {
                Point2D now = stack.Peek();
                if (now == end)
                    break;
                List<Point2D> relates = GetRelatedPoint(now);
                if (relates.Count > 0)
                {
                    Point2D p = relates[0];
                    stack.Push(p);
                    Mark(p);
                }
                else
                {
                    stack.Pop();
                }
            }

            List<Point2D> result = new();
            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }
            return result;
        }
    }
}
