using Maze.WayFinding;
using System.Collections.Generic;
using Utils.Mathematical;

namespace Maze.Base
{
    public interface IMaze
    {
        public int Height { get; }
        public int Width { get; }

        /// <summary>
        /// 寻路
        /// </summary>
        /// <param name="start">起点（默认为左上角）</param>
        /// <param name="end">终点（默认为右下角）</param>
        /// <param name="solveType">寻路方式</param>
        /// <returns>返回从起点到终点经过的点</returns>
        public List<Point2D> FindWay(Point2D start = default, Point2D end = default, FindMode solveType = FindMode.DFS);

        /// <summary>
        /// 终端显示迷宫
        /// </summary>
        /// <param name="showWay">是否显示路径（如果有的话）</param>
        public void Show(bool showWay = false);
    }
}
