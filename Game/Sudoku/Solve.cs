using Utils.Extend;

namespace Sudoku
{
    /// <summary>
    /// 解数独 TODO
    /// </summary>
    public class Solve
    {
        private readonly int M;
        private readonly int N;
        private int Length => M * N;
        private int[,] playMat;
        private List<int>[,] posibleNum;

        public Solve(Box box)
        {
            M = box.M;
            N = box.N;
            playMat = new int[Length, Length];
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    if (!box.canChange[i, j])
                    {
                        playMat[i, j] = box.playMat[i, j];
                    }
                    else
                    {
                        playMat[i, j] = 0;
                    }
                }
            }

            posibleNum = new List<int>[Length, Length];
            foreach (List<int> ints in posibleNum)
            {
                ints.Clear();
                for (int i = 1; i <= Length; i++)
                {
                    ints.Add(i);
                }
            }
        }

        public void Start()
        {

        }

        private void UpdatePosibleNum()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    int index = i * Length + j;
                    List<int> relatedIndex = GetRelatedIndex(index);
                    foreach (int index2 in relatedIndex)
                    {
                        int x = index2 % Length;
                        int y = index2 / Length;
                        posibleNum[i, j].Remove(playMat[y, x]);
                    }
                }
            }
        }

        private void DetermineNum()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    if (posibleNum[i, j].Count == 0)
                    {
                        throw new Exception("计算错误");
                    }
                    if (posibleNum[i, j].Count == 1)
                    {
                        playMat[i, j] = posibleNum[i, j].First();
                        posibleNum[i, j].Clear();
                    }
                }
            }
        }

        private List<int> GetRelatedIndex(int index)
        {
            int x = index % Length;
            int y = index / Length;
            int startX = x / N * N;
            int startY = y / M * M;
            List<int> result = new();
            for (int i = 0; i < Length; i++)
            {
                result.Add(i * Length + x);
                result.Add(y * Length + i);
            }

            for (int i = startY; i < startY + M; i++)
            {
                for (int j = startX; j < startX + N; j++)
                {
                    result.Add(i * Length + j);
                }
            }
            result.SortAndDeduplicate();
            return result;
        }
    }
}
