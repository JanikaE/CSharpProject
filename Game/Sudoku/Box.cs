namespace Sudoku
{
    /// <summary>
    /// 自定义大小数独矩阵
    /// </summary>
    public class Box
    {
        /// <summary>子矩阵的高</summary>
        public int M { get; private set; }
        /// <summary>子矩阵的宽</summary>
        public int N { get; private set; }
        public int Length => M * N;

        public int[,] playMat;
        public bool[,] canChange;

        /// <summary>
        /// 生成一个空白的数独矩阵
        /// </summary>
        /// <param name="m">子矩阵的高，以及横向上有多少个子矩阵</param>
        /// <param name="n">子矩阵的宽，以及纵向上有多少个子矩阵</param>
        /// <exception cref="ArgumentException">参数不能为负数或0</exception>
        public Box(int m, int n) 
        {
            if (m <= 0 || n <= 0)
                throw new ArgumentException("参数不能为负数或0");
            M = m;
            N = n;
            playMat = new int[Length, Length];
            canChange = new bool[Length, Length];
        }

        public void Generate()
        {
            //TODO
        }

        public bool[,] CheckCorrect()
        {
            bool[,] result = new bool[Length, Length];
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    result[j, i] = CheckOne(i, j);
                }
            }
            return result;
        }

        public bool CheckOne(int x, int y)
        {
            // 检查在行列中是否有重复
            for (int i = 0; i < Length; i++)
            {
                if (playMat[y, x] == playMat[y, i] && x != i)
                {
                    return false;
                }
                if (playMat[y, x] == playMat[i, x] && y != i)
                {
                    return false;
                }
            }
            // 检查在子矩阵中是否有重复
            int[] subMat = GetSubMat(x, y, out int index);
            for (int i = 0; i < Length; i++)
            {
                if (subMat[index] == subMat[i] && index != i)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取小格其所在的子矩阵对应的数组
        /// </summary>
        /// <param name="index">此小格在子矩阵对应的数组内的下标</param>
        private int[] GetSubMat(int x, int y, out int index)
        {
            int[] subMat = new int[Length];
            int k = 0;
            int startX = x / N * N;
            int startY = y / M * M;
            for (int i = startY; i < startY + M; i++)
            {
                for (int j = startX; j < startX + N; j++)
                {
                    subMat[k++] = playMat[i, j];
                }
            }
            index = (y - startY) * M + x - startX;
            return subMat;
        }
    }
}
