using System;

namespace Sudoku.Game
{
    public partial class Puzzel
    {
        /// <summary>
        /// 用现有字符串生成数独
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void GenerateByExample(string s)
        {
            char[] chars = s.ToCharArray();
            if (chars.Length != Length * Length)
                throw new ArgumentException("长宽不匹配");

            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    int num = chars[row * Length + col] - '0';
                    if (num < 0 || num > Length)
                        throw new ArgumentOutOfRangeException($"数值超出范围0-{Length}");
                    PlayMat(row, col).num = num;
                    PlayMat(row, col).canChange = num == 0;
                }
            }
        }

        /// <summary>
        /// 随机生成具有唯一解的数独
        /// </summary>
        public void GenerateRandom(MainForm? form = null)
        {
            // 对一个空白的数独暴力求解，可随机产生一个满的数独
            string blank = string.Empty;
            for (int i = 0; i < Length * Length; i++)
            {
                blank += "0";
            }
            GenerateByExample(blank);
            InitPosibleNums();
            if (form != null)
            {
                SolveBuster(form);
            }
            else
            {
                SolveBuster();
            }

            // 循环随机去除已知的格子，直至不管去除哪个都不再有唯一解
            List<int> ignore = new();
            while (true)
            {
                (int index, int num, bool success) = RemoveOne(ignore);
                if (!success)
                    break;
                //if (CountBlank() > 10)
                //    break;

                Puzzel clone = Clone();
                clone.InitPosibleNums();
                if (clone.SolveCountBuster() != 1)
                {
                    PlayMat(index).num = num;
                    ignore.Add(index);
                }
                else
                {
                    ignore.Clear();
                }
            }

            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {                    
                    PlayMat(row, col).canChange = PlayMat(row, col).num == 0;
                }
            }
        }

        private (int, int, bool) RemoveOne(List<int> ignore)
        {
            List<int> indexList = new();
            for (int i = 0; i < Length * Length; i++)
            {
                if (!ignore.Contains(i) && PlayMat(i).num != 0)
                {
                    indexList.Add(i);
                }
            }
            if (indexList.Count == 0)
            {
                return (0, 0, false);
            }

            int index = indexList[new Random().Next(0, indexList.Count)];
            Cell cell = PlayMat(index);
            int num = cell.num;
            cell.num = 0;
            return (index, num, true);
        }
    }
}
