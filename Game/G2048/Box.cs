using System;
using System.Collections.Generic;

namespace G2048
{
    public class Box
    {
        public int rage;
        public int[,] playMat;

        public State state;

        public int score;

        public Box(int rage)
        {
            this.rage = rage;
            playMat = new int[rage, rage];
        }

        public void Init()
        {
            for (int i = 0; i < rage; i++)
            {
                for (int j = 0; j < rage; j++)
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
            Random random = new Random();
            int x, y;
            while (true)
            {
                x = random.Next(rage);
                y = random.Next(rage);
                if (playMat[x, y] == 0)
                {
                    break;
                }
            }
            playMat[x, y] = 2;
        }

        public void Operate(Operation op)
        {
            int[,] playMatNew = Operate(playMat, op, out int score);
            // 如果操作没有使数字发生变化，视为无效操作
            if (!Compare(playMatNew, playMat))
            {
                playMat = (int[,])playMatNew.Clone();
                this.score += score;
                // 在操作有效且游戏继续的情况下生成新的数字
                Generate();
            }
        }

        public bool CheckFail()
        {
            // 判定是否有空位
            for (int i = 0; i < rage; i++)
            {
                for (int j = 0; j < rage; j++)
                {
                    if (playMat[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            // 判定是否有相邻数字相同，有则继续
            for (int i = 0; i < rage; i++)
            {
                for (int j = 0; j < rage - 1; j++)
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

        private int[,] Operate(int[,] playMat, Operation op, out int score)
        {
            int[,] result = (int[,])playMat.Clone();
            int[] line = new int[rage];
            score = 0;
            switch (op)
            {
                case Operation.Up:
                    for (int i = 0; i < rage; i++)
                    {
                        for (int j = 0; j < rage; j++)
                        {
                            line[j] = result[j, i];
                        }
                        score += ChangeLine(line);
                        for (int j = 0; j < rage; j++)
                        {
                            result[j, i] = line[j];
                        }
                    }
                    break;
                case Operation.Down:
                    for (int i = 0; i < rage; i++)
                    {
                        for (int j = 0; j < rage; j++)
                        {
                            line[rage - j - 1] = result[j, i];
                        }
                        score += ChangeLine(line);
                        for (int j = 0; j < rage; j++)
                        {
                            result[j, i] = line[rage - j - 1];
                        }
                    }
                    break;
                case Operation.Left:
                    for (int i = 0; i < rage; i++)
                    {
                        for (int j = 0; j < rage; j++)
                        {
                            line[j] = result[i, j];
                        }
                        score += ChangeLine(line);
                        for (int j = 0; j < rage; j++)
                        {
                            result[i, j] = line[j];
                        }
                    }
                    break;
                case Operation.Right:
                    for (int i = 0; i < rage; i++)
                    {
                        for (int j = 0; j < rage; j++)
                        {
                            line[rage - j - 1] = result[i, j];
                        }
                        score += ChangeLine(line);
                        for (int j = 0; j < rage; j++)
                        {
                            result[i, j] = line[rage - j - 1];
                        }
                    }
                    break;
            }
            return result;
        }

        private int ChangeLine(int[] line)
        {
            Move(line);
            int score = Merge(line);
            Move(line);
            return score;
        }

        private void Move(int[] line)
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

        private int Merge(int[] line)
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

        private bool Compare(int[,] nums1, int[,] nums2)
        {
            for (int i = 0; i < rage; i++)
            {
                for (int j = 0; j < rage; j++)
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

        public Operation Next()
        {
            Dictionary<Operation, int> keyValuePairs = new Dictionary<Operation, int>();
            foreach (Operation op in Enum.GetValues(typeof(Operation)))
            {
                if (op == Operation.None) continue;
                int[,] playMatNew = Operate(playMat, op, out _);
                if (!Compare(playMatNew, playMat))
                {
                    int value = Value(Operate(playMat, op, out _));
                    keyValuePairs.Add(op, value);
                }
            }
            Operation result = Operation.None;
            int maxValue = 0;
            string message = "";
            foreach (Operation op in keyValuePairs.Keys)
            {
                int value = keyValuePairs[op];
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

        private static int Value(int[,] playMat)
        {
            int height = playMat.GetLength(0);
            int width = playMat.GetLength(1);
            int result = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int num = playMat[i, j];
                    if (num > 0)
                    {
                        int x = Math.Min(i, height - i - 1) + 1;
                        int y = Math.Min(j, width - j - 1) + 1;
                        int weight = (int)Math.Pow(2, height + width - x - y);
                        result += weight * num;
                    }
                    else
                    {
                        result += 8192;
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
