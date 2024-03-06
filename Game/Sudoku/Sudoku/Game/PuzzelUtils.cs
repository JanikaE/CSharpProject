using Utils.Extend;
using Utils.Tool;

namespace Sudoku.Game
{
    public partial class Puzzel
    {
        /// <summary>
        /// 获取某小格其所在的宫格
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="SolveException"></exception>
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

        /// <summary>
        /// 获取与某两个小格同时关联的所有小格（不包含自身）
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        private List<int> GetRelatedIndex(int index1, int index2)
        {
            List<int> result = ListTool.Intersection(GetRelatedIndex(index1), GetRelatedIndex(index2));
            result.SortAndDeduplicate();
            return result;
        }

        /// <summary>
        /// 获取与某两个小格同时关联的所有小格（不包含自身）
        /// </summary>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        private List<int> GetRelatedIndex(Cell cell1, Cell cell2)
        {
            return GetRelatedIndex(cell1.row * Length + cell1.col, cell2.row * Length + cell2.col);
        }

        /// <summary>
        /// 某两个小格是否关联
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        private bool IsRelated(int index1, int index2)
        {
            List<int> relates = GetRelatedIndex(index1);
            return relates.Contains(index2);
        }

        /// <summary>
        /// 某两个小格是否关联
        /// </summary>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        private bool IsRelated(Cell cell1, Cell cell2)
        {
            return IsRelated(cell1.row * Length + cell1.col, cell2.row * Length + cell2.col);
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
    }
}
