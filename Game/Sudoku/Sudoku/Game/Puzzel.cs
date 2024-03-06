using Utils.Extend;
using Utils.Mathematical;

namespace Sudoku.Game
{
    /// <summary>
    /// 自定义大小数独矩阵
    /// </summary>
    public partial class Puzzel
    {
        /// <summary>宫格的高，即横向上宫格的数量</summary>
        public int H { get; }
        /// <summary>宫格的宽，即纵向上宫格的数量</summary>
        public int W { get; }
        public int Length => H * W;

        private readonly Cell[,] playMat;

        public Cell PlayMat(int row, int col) => playMat[row, col];
        public Cell PlayMat(int index) => playMat[index / Length, index % Length];
        public Cell PlayMat(Point2D position) => playMat[position.Y, position.X];

        public List<int> Nums { get; }
        private Dictionary<string, List<int>> Houses { get; set; }
        private Dictionary<string, List<int>> Palaces { get; set; }

        /// <summary>
        /// 生成一个空白数独矩阵
        /// </summary>
        /// <param name="H">子矩阵的高，以及横向上有多少个子矩阵</param>
        /// <param name="W">子矩阵的宽，以及纵向上有多少个子矩阵</param>
        /// <exception cref="ArgumentException">参数不能为负数或0</exception>
        public Puzzel(int H = 3, int W = 3)
        {
            if (H <= 0 || W <= 0)
                throw new ArgumentException("参数不能为负数或0");
            this.H = H;
            this.W = W;
            playMat = new Cell[Length, Length];
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    playMat[row, col] = new Cell(row, col);
                }
            }

            Nums = new List<int>();
            for (int i = 1; i <= Length; i++)
            {
                Nums.Add(i);
            }
            Palaces = GetPalaces();
            Houses = GetHouses();
        }

        /// <summary>
        /// 初始化所有格子的可能数字并更新
        /// </summary>
        public void InitPosibleNums()
        {
            foreach (Cell cell in playMat)
            {
                if (cell.num != 0)
                {
                    cell.possibleNums.Clear();
                }
                else
                {
                    cell.possibleNums = Nums.Clone();
                }
            }
            UpdatePosibleNums();
        }

        /// <summary>
        /// 更新所有格子的可能数字
        /// </summary>
        public void UpdatePosibleNums()
        {
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    Cell cell = PlayMat(row, col);
                    if (cell.num != 0)
                    {
                        cell.possibleNums.Clear();
                    }
                    else
                    {
                        List<int> relatedIndex = GetRelatedIndex(cell);
                        foreach (int index2 in relatedIndex)
                        {
                            int x = index2 % Length;
                            int y = index2 / Length;
                            cell.possibleNums.Remove(PlayMat(y, x).num);
                        }
                    }
                }
            }
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

        public bool CheckOne(int col, int row)
        {
            // 检查在行列中是否有重复
            for (int i = 0; i < Length; i++)
            {
                if (PlayMat(row, col).num == PlayMat(row, i).num && col != i)
                {
                    return false;
                }
                if (PlayMat(row, col).num == PlayMat(i, col).num && row != i)
                {
                    return false;
                }
            }
            // 检查在宫格中是否有重复
            int index = row * Length + col;
            List<int> palace = GetPalace(index);
            foreach (int another in palace)
            {
                if (PlayMat(index) == PlayMat(another) && index != another)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckFull()
        {
            foreach (Cell cell in playMat)
            {
                if (cell.num == 0)
                    return false;
            }
            return true;
        }

        public int CountBlank()
        {
            int count = 0;
            foreach (Cell cell in playMat)
            {
                if (cell.num == 0)
                    count++;
            }
            return count;
        }

        public int CountPossibleNum()
        {
            int count = 0;
            foreach (Cell cell in playMat)
            {
                count += cell.possibleNums.Count;
            }
            return count;
        }

        public Puzzel Clone()
        {
            Puzzel puzzel = new(H, W);
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    puzzel.PlayMat(row, col).num = PlayMat(row, col).num;
                }
            }
            return puzzel;
        }
    }
}
