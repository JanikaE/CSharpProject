using System.Collections;
using Utils.Extend;

namespace Sudoku
{
    /// <summary>
    /// 自定义大小数独矩阵
    /// </summary>
    public class Puzzel : ICloneable
    {
        /// <summary>宫格的高，即横向上宫格的数量</summary>
        public int H { get; }
        /// <summary>宫格的宽，即纵向上宫格的数量</summary>
        public int W { get; }
        public int Length => H * W;

        public Cell[,] playMat;

        public Cell PlayMat(int i, int j) => playMat[i, j];
        public Cell PlayMat(int i) => playMat[i / Length, i % Length];

        private List<List<int>> Houses { get; }
        private List<List<int>> Palaces { get; }

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
                    playMat[row, col] = new Cell
                    {
                        row = row,
                        col = col
                    };
                }
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
                cell.posibleNums.Clear();
                for (int i = 1; i <= Length; i++)
                {
                    cell.posibleNums.Add(i);
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
                    int index = row * Length + col;
                    List<int> relatedIndex = GetRelatedIndex(index);
                    foreach (int index2 in relatedIndex)
                    {
                        int x = index2 % Length;
                        int y = index2 / Length;
                        PlayMat(row, col).posibleNums.Remove(PlayMat(y, x).num);
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
            int[] subMat = GetPalace(x, y, out int index);
            for (int i = 0; i < Length; i++)
            {
                if (subMat[index] == subMat[i] && index != i)
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

        #region utils

        /// <summary>
        /// 获取小格其所在的宫格对应的数组
        /// </summary>
        /// <param name="index">此小格在宫格对应的数组内的下标</param>
        private int[] GetPalace(int x, int y, out int index)
        {
            int[] subMat = new int[Length];
            int k = 0;
            int startX = x / W * W;
            int startY = y / H * H;
            for (int i = startY; i < startY + H; i++)
            {
                for (int j = startX; j < startX + W; j++)
                {
                    subMat[k++] = playMat[i, j].num;
                }
            }
            index = (y - startY) * H + x - startX;
            return subMat;
        }

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
            foreach (List<int> palace in Palaces)
            {
                if (palace.Contains(index))
                {
                    result.AddRange(palace);
                    break;
                }
            }
            result.SortAndDeduplicate();
            return result;
        }

        private List<List<int>> GetHouses()
        {
            List<List<int>> results = new();
            for (int i = 0; i < Length; i++)
            {
                List<int> resultRow = new();
                List<int> resultCol = new();
                for (int j = 0; j < Length; j++)
                {
                    resultRow.Add(i * Length + j);
                    resultCol.Add(j * Length + i);
                }
                results.Add(resultRow);
                results.Add(resultCol);
            }
            results.AddRange(Palaces);
            return results;
        }

        private List<List<int>> GetPalaces()
        {
            List<List<int>> results = new();
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
                results.Add(result);
            }
            return results;
        }

        #endregion

        #region solve

        public void StartSolve(MainForm? mainform = null)
        {
            List<Func<string>> Funcs = new()
            {
                NakedSingle,
                HiddenSingle
            };
            try
            {
                while (true)
                {
                    string result = string.Empty;
                    foreach (Func<string> func in Funcs)
                    {
                        result = func();
                        if (result != string.Empty)
                        {
                            UpdatePosibleNums();
                            mainform?.AddSolveStep(result, (Puzzel)Clone());
                            continue;
                        }
                    }
                    if (result == string.Empty)
                    {
                        if (CheckFull())
                        {
                            mainform?.AddSolveStep("Over.", null);
                        }
                        else
                        {
                            mainform?.AddSolveStep("Stop.", null);
                        }
                        break;
                    }
                }
            }
            catch (SolveException se)
            {
                mainform?.AddSolveStep(se.Message, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
            }
        }

        private string NakedSingle()
        {
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    Cell cell = PlayMat(row, col);
                    if (cell.num == 0)
                    {
                        if (cell.posibleNums.Count == 1)
                        {
                            int value = cell.posibleNums.First();
                            PlayMat(row, col).num = value;
                            cell.posibleNums.Clear();
                            return $"NakedSingle ({row + 1}, {col + 1}) value:{value}";
                        }
                        if (cell.posibleNums.Count == 0)
                        {
                            throw new SolveException($"NakedSingle ({row + 1}, {col + 1}) Error");
                        }
                    }
                }
            }
            return string.Empty;
        }

        private string HiddenSingle()
        {
            foreach (List<int> house in Houses)
            {
                Dictionary<int, Cell?> valueInCell = new();
                foreach (int index in house)
                {
                    Cell cell = PlayMat(index);
                    if (cell.num != 0)
                        continue;
                    if (cell.posibleNums.Count == 0)
                    {
                        throw new SolveException($"HiddenSingle ({index / Length + 1}, {index % Length + 1}) Error");
                    }
                    else
                    {
                        foreach (int value in cell.posibleNums)
                        {
                            if (!valueInCell.ContainsKey(value))
                            {
                                valueInCell.Add(value, cell);
                            }
                            // 一个值可填入多个格子时
                            else
                            {
                                valueInCell[value] = null;
                            }
                        }
                    }
                }
                foreach (int value in valueInCell.Keys)
                {
                    Cell? cell = valueInCell[value];
                    if (cell != null)
                    {
                        cell.num = value;
                        cell.posibleNums.Clear();
                        return $"HiddenSingle ({cell.row + 1}, {cell.col + 1}) value:{value}";
                    }
                }
            }
            return string.Empty;
        }

        #endregion

        public object Clone()
        {
            Puzzel clone = new(H, W);
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    clone.playMat[row, col] = new Cell
                    {
                        row = row,
                        col = col,
                        num = playMat[row, col].num
                    };
                }
            }
            clone.InitPosibleNums();
            return clone;
        }
    }
}
