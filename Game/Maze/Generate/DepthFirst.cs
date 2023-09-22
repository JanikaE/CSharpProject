using Utils.Mathematical;

namespace Maze
{
    /// <summary>
    /// 深度优先算法
    /// </summary>
    internal class DepthFirst : Prim
    {
        private readonly Stack<Point2D> blockStack;
        private Point2D currentBlock;

        public DepthFirst(int height, int width) : base(height, width)
        {
            blockStack = new();
            currentBlock = new(0, 0);
        }

        public override void Generate()
        {
            while (true)
            {
                List<Point2D> points = GetNeighborUnreachedBlocks(currentBlock);
                // 当前格有未连通的邻格
                if (points.Count > 0)
                {
                    // 随机选择一个邻格并打通
                    Point2D newPoint = points[Random.Shared.Next(points.Count)];
                    BreakWall(currentBlock, newPoint);
                    canReach[newPoint.Y, newPoint.X] = true;

                    // 将当前格入栈并将邻格作为新的当前格
                    blockStack.Push(currentBlock);
                    currentBlock = newPoint;
                }
                // 当前格没有未连通的邻格
                else if (blockStack.Count > 0)
                {
                    // 栈顶格子出栈作为当前格
                    currentBlock = blockStack.Pop();
                }
                // 栈空时迷宫生成完成
                else
                {
                    break;
                }
            }
        }
    }
}
