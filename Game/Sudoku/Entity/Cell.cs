namespace Sudoku.Entity
{
    public class Cell
    {
        public int num;
        public bool canChange;
        public List<int> posibleNums;

        public char row;
        public char col;

        public Cell(int row, int col)
        {
            num = 0;
            canChange = true;
            posibleNums = new List<int>();
            this.row = (char)('A' + row - 0);
            this.col = (char)('1' + col - 0);
        }
    }
}
