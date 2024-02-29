using Sudoku.Game;
using Sudoku.Snap;

namespace SudokuConsole
{
    internal static class PuzzelUtils
    {
        public static string ContentString(Puzzel puzzel)
        {
            string s = string.Empty;
            for (int row = 0; row < puzzel.Length; row++)
            {
                for (int col = 0; col < puzzel.Length; col++)
                {
                    s += puzzel.PlayMat(row, col).num.ToString();
                }
                s += "\n";
            }
            return s;
        }

        public static string ContentString(PuzzelSnap puzzelSnap)
        {
            string s = string.Empty;
            for (int row = 0; row < puzzelSnap.Length; row++)
            {
                for (int col = 0; col < puzzelSnap.Length; col++)
                {
                    s += puzzelSnap.playMat[row, col].num.ToString();
                }
                s += "\n";
            }
            return s;
        }
    }
}
