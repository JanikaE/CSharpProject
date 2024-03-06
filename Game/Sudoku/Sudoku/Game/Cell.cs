using Utils.Mathematical;

namespace Sudoku.Game
{
    public class Cell
    {
        public int num;
        public bool canChange;
        public List<int> possibleNums;

        /// <summary>
        /// 行号
        /// </summary>
        public int row;
        /// <summary>
        /// 列号
        /// </summary>
        public int col;

        public Point2D Position => new(col, row);

        public char Row => (char)('A' + row - 0);
        public char Col => (char)('1' + col - 0);
        public string Name => Row.ToString() + Col.ToString();

        public Cell(int row, int col)
        {
            num = 0;
            canChange = true;
            possibleNums = new List<int>();
            this.row = row;
            this.col = col;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
