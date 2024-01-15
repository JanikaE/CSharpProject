namespace Sudoku.Entity
{
    public struct CellSnap
    {
        public int num;
        public bool canChange;
        public int[] posibleNums;

        public CellSnap(Cell cell)
        {
            num = cell.num;
            canChange = cell.canChange;
            posibleNums = cell.posibleNums.ToArray();
        }
    }
}
