using Utils.Extend;

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

        public void Generate()
        {
            playMat[0, 0].num = 1;
            playMat[8, 0].num = 2;
            playMat[0, 8].num = 3;
            playMat[8, 8].num = 4;
        }

        public void InitPosibleNums()
        {
            foreach (Cell cell in playMat)
            {
                if (cell.num != 0)
                {
                    cell.posibleNums.Clear();
                }                    
                else
                {
                    cell.posibleNums = Nums.Clone();
                }                
            }
            UpdatePosibleNums();
        }

        public void UpdatePosibleNums()
        {
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    Cell cell = PlayMat(row, col);
                    if (cell.num != 0)
                    {
                        cell.posibleNums.Clear();
                    }
                    else
                    {
                        List<int> relatedIndex = GetRelatedIndex(cell);
                        foreach (int index2 in relatedIndex)
                        {
                            int x = index2 % Length;
                            int y = index2 / Length;
                            cell.posibleNums.Remove(PlayMat(y, x).num);
                        }
                    }                    
                }
            }
        }

        #region Check

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

        #endregion

        #region Utils

        private List<int> GetPalace(int index)
        {
            foreach (List<int> palace in Palaces.Values)
            {
                if (palace.Contains(index))
                {
                    return palace;
                }
            }
            throw new SolveException($"Palace Error, Index:{index}");
        }

        /// <summary>
        /// 获取与某小格关联的所有小格（不包含自身）
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private List<int> GetRelatedIndex(int index)
        {
            int thisCol = index % Length;
            int thisRol = index / Length;
            List<int> result = new();
            for (int i = 0; i < Length; i++)
            {
                result.Add(i * Length + thisCol);
                result.Add(thisRol * Length + i);
            }
            foreach (List<int> palace in Palaces.Values)
            {
                if (palace.Contains(index))
                {
                    result.AddRange(palace);
                    break;
                }
            }
            result.SortAndDeduplicate();
            result.Remove(index);
            return result;
        }
        
        /// <summary>
        /// 获取与某小格关联的所有小格（不包含自身）
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private List<int> GetRelatedIndex(Cell cell)
        {
            return GetRelatedIndex(cell.row * Length + cell.col);
        }

        private Dictionary<string, List<int>> GetHouses()
        {
            Dictionary<string, List<int>> results = new();
            for (int i = 0; i < Length; i++)
            {
                List<int> resultRow = new();
                List<int> resultCol = new();
                for (int j = 0; j < Length; j++)
                {
                    resultRow.Add(i * Length + j);
                    resultCol.Add(j * Length + i);
                }
                char row = (char)('A' + i - 0);
                char col = (char)('1' + i - 0);
                results.Add("Row" + row, resultRow);
                results.Add("Col" + col, resultCol);
            }
            results.AddRange(Palaces);
            return results;
        }

        private Dictionary<string, List<int>> GetPalaces()
        {
            Dictionary<string, List<int>> results = new();
            for (int num = 0; num < Length; num++)
            {
                List<int> result = new();
                int startRow = num / H * H;
                int startCol = num % H * W;
                for (int row = startRow; row < startRow + H; row++)
                {
                    for (int col = startCol; col < startCol + W; col++)
                    {
                        result.Add(row * Length + col);
                    }
                }
                results.Add("Palace" + PlayMat(startRow, startCol).Name, result);
            }
            return results;
        }

        #endregion
    }
}
