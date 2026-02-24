using System;
using System.Collections.Generic;
using Utils.Mathematical;

namespace G2048
{
    public class Box
    {
        public int Rage => playMat.Width;
        public Map2D<int> playMat;

        public State state;

        public int score;

        public Box(int rage)
        {
            playMat = new Map2D<int>(rage, rage);
        }

        public void Init()
        {
            for (int i = 0; i < Rage; i++)
            {
                for (int j = 0; j < Rage; j++)
                {
                    playMat[i, j] = 0;
                }
            }
            Generate();
            score = 0;
            state = State.Playing;
        }

        private void Generate()
        {
            Random random = new();
            int x, y;
            while (true)
            {
                x = random.Next(Rage);
                y = random.Next(Rage);
                if (playMat[x, y] == 0)
                {
                    break;
                }
            }
            playMat[x, y] = 2;
        }

        public void Operate(RelativePosition_4 op)
        {
            Map2D<int> playMatNew = Operate(playMat, op, out int score);
            // 如果操作没有使数字发生变化，视为无效操作
            if (!Compare(playMatNew, playMat))
            {
                playMat = playMatNew.Clone();
                this.score += score;
                // 在操作有效且游戏继续的情况下生成新的数字
                Generate();
            }
        }

        public bool CheckFail()
        {
            // 判定是否有空位
            for (int i = 0; i < Rage; i++)
            {
                for (int j = 0; j < Rage; j++)
                {
                    if (playMat[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            // 判定是否有相邻数字相同，有则继续
            for (int i = 0; i < Rage; i++)
            {
                for (int j = 0; j < Rage - 1; j++)
                {
                    if (playMat[i, j] == playMat[i, j + 1])
                    {
                        return false;
                    }
                    if (playMat[j, i] == playMat[j + 1, i])
                    {
                        return false;
                    }
                }
            }

            // 没有空位且没有相邻数字相同，判定失败
            state = State.Over;
            return true;
        }

        #region Operate

        private Map2D<int> Operate(Map2D<int> playMat, RelativePosition_4 op, out int score)
        {
            var result = playMat.Clone();
            int[] line = new int[Rage];
            score = 0;
            switch (op)
            {
                case RelativePosition_4.Up:
                    for (int i = 0; i < Rage; i++)
                    {
                        for (int j = 0; j < Rage; j++)
                        {
                            line[j] = result[j, i];
                        }
                        score += ChangeLine(line);
                        for (int j = 0; j < Rage; j++)
                        {
                            result[j, i] = line[j];
                        }
                    }
                    break;
                case RelativePosition_4.Down:
                    for (int i = 0; i < Rage; i++)
                    {
                        for (int j = 0; j < Rage; j++)
                        {
                            line[Rage - j - 1] = result[j, i];
                        }
                        score += ChangeLine(line);
                        for (int j = 0; j < Rage; j++)
                        {
                            result[j, i] = line[Rage - j - 1];
                        }
                    }
                    break;
                case RelativePosition_4.Left:
                    for (int i = 0; i < Rage; i++)
                    {
                        for (int j = 0; j < Rage; j++)
                        {
                            line[j] = result[i, j];
                        }
                        score += ChangeLine(line);
                        for (int j = 0; j < Rage; j++)
                        {
                            result[i, j] = line[j];
                        }
                    }
                    break;
                case RelativePosition_4.Right:
                    for (int i = 0; i < Rage; i++)
                    {
                        for (int j = 0; j < Rage; j++)
                        {
                            line[Rage - j - 1] = result[i, j];
                        }
                        score += ChangeLine(line);
                        for (int j = 0; j < Rage; j++)
                        {
                            result[i, j] = line[Rage - j - 1];
                        }
                    }
                    break;
            }
            return result;
        }

        private static int ChangeLine(int[] line)
        {
            Move(line);
            int score = Merge(line);
            Move(line);
            return score;
        }

        private static void Move(int[] line)
        {
            for (int i = 1; i < line.Length; i++)
            {
                if (line[i] != 0 && line[i - 1] == 0)
                {
                    line[i - 1] = line[i];
                    line[i] = 0;
                    i = 1;
                }
            }
        }

        private static int Merge(int[] line)
        {
            int score = 0;
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (line[i] == line[i + 1] && line[i] != 0)
                {
                    line[i] *= 2;
                    line[i + 1] = 0;
                    score += line[i];
                }
            }
            return score;
        }

        private bool Compare(Map2D<int> nums1, Map2D<int> nums2)
        {
            for (int i = 0; i < Rage; i++)
            {
                for (int j = 0; j < Rage; j++)
                {
                    if (nums1[i, j] != nums2[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region AI

        public List<Point2D> Lines { get; private set; }

        public void InitAI()
        {
            Lines = new List<Point2D>();
            for (int y = 0; y < Rage; y++)
            {
                if (y % 2 == 0)
                {
                    for (int x = 0; x < Rage; x++)
                        Lines.Add(new Point2D(x, y));
                }
                else
                {
                    for (int x = Rage - 1; x >= 0; x--)
                        Lines.Add(new Point2D(x, y));
                }
            }
        }

        public RelativePosition_4 Next()
        {
            Dictionary<RelativePosition_4, long> keyValuePairs = new();
            foreach (RelativePosition_4 op in Enum.GetValues(typeof(RelativePosition_4)))
            {
                if (op == RelativePosition_4.None) continue;
                Map2D<int> playMatNew = Operate(playMat, op, out _);
                if (!Compare(playMatNew, playMat))
                {
                    long value = Value(Operate(playMat, op, out _));
                    keyValuePairs.Add(op, value);
                }
            }
            RelativePosition_4 result = RelativePosition_4.None;
            long maxValue = 0;
            string message = "";
            foreach (RelativePosition_4 op in keyValuePairs.Keys)
            {
                long value = keyValuePairs[op];
                if (value > maxValue)
                {
                    maxValue = value;
                    result = op;
                }

                message += op.ToString() + ":" + value + "\n";
            }
            //MessageBox.Show(message);
            return result;
        }

        private long Value(Map2D<int> playMat)
        {
            int result = 0;
            foreach (var point in Lines)
            {
                result *= 2;
                result += playMat[point];
            }
            return result;
        }

        #endregion
    }
}
