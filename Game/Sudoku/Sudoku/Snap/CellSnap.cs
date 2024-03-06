using Sudoku.Game;

namespace Sudoku.Snap
{
    public struct CellSnap
    {
        public int num;
        public bool canChange;
        public int[] possibleNums;

        public CellSnap(Cell cell)
        {
            num = cell.num;
            canChange = cell.canChange;
            possibleNums = cell.possibleNums.ToArray();
        }
    }
}
