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

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    int num = chars[i * Length + j] - '0';
                    if (num < 0 || num > Length)
                        throw new ArgumentOutOfRangeException($"数值超出范围0-{Length}");
                    playMat[i, j].num = num;
                    playMat[i, j].canChange = num == 0;
                }
            }
        }

        /// <summary>
        /// 随机生成具有唯一解的数独
        /// </summary>
        public void GenerateRandom()
        {
            string blank = string.Empty;
            for (int i = 0; i < Length * Length; i++)
            {
                blank += "0";
            }
            GenerateByExample(blank);
            InitPosibleNums();
            SolveBuster();

            while (true)
            {
                (int index, int num) = RemoveOne();
                Puzzel clone = Clone();
                clone.InitPosibleNums();
                if (clone.SolveCountBuster() != 1)
                {
                    PlayMat(index).num = num;
                    return;
                }
            }
        }

        private (int, int) RemoveOne()
        {
            int index;
            Cell cell;
            do
            {
                index = new Random().Next(0, Length * Length);
                cell = PlayMat(index);
            } while (cell.num == 0);
            int num = cell.num;
            cell.num = 0;
            return (index, num);
        }
    }
}
