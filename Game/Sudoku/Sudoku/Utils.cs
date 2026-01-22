using Sudoku.Game;
using Sudoku.Snap;
using System;

namespace Sudoku
{
    internal static class Utils
    {
        public static string ContentString(Puzzel puzzel)
        {
            string s = string.Empty;
            int max = puzzel.Square;
            for (int row = 0; row < puzzel.Length; row++)
            {
                for (int col = 0; col < puzzel.Length; col++)
                {
                    s += puzzel.PlayMat(row, col).num.AddBlank(max);
                }
                s += "\n";
            }
            return s;
        }

        public static string ContentString(PuzzelSnap puzzelSnap)
        {
            string s = string.Empty;
            int max = puzzelSnap.Square;
            for (int row = 0; row < puzzelSnap.Length; row++)
            {
                for (int col = 0; col < puzzelSnap.Length; col++)
                {
                    s += puzzelSnap.playMat[row, col].num.AddBlank(max);
                }
                s += "\n";
            }
            return s;
        }

        private static string AddBlank(this int num, int max)
        {
            int blank;
            if (num != 0)
            {
                blank = (int)Math.Log10(max) - (int)Math.Log10(num) + 1;
            }
            else
            {
                blank = (int)Math.Log10(max) - (int)Math.Log10(1) + 1;
            }

            string result = num.ToString();
            for (int i = 0; i < blank; i++)
            {
                result += " ";
            }
            return result;
        }
    }
}
