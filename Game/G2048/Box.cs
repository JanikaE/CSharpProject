using System;

namespace G2048
{
    public class Box
    {
        public int rage;
        public int[,] playMat;
        public int[,] playMatOld;

        public State state;

        public int score;

        public Box(int rage)
        {
            this.rage = rage;
            playMat = new int[rage, rage];
            playMatOld = new int[rage, rage];
        }

        public void Init()
        {
            for (int i = 0; i < rage; i++)
            {
                for (int j = 0; j < rage; j++)
                {
                    playMat[i, j] = 0;
                    playMatOld[i, j] = 0;
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
            Copy(playMatOld, playMat);
            int[] line = new int[rage];
            switch (op)
            {
                case Operation.Up:
                    for (int i = 0; i < rage; i++)
                    {
                        for (int j = 0; j < rage; j++)
                        {
                            line[j] = playMat[j, i];
                        }
                        ChangeLine(line);
                        for (int j = 0; j < rage; j++)
                        {
                            playMat[j, i] = line[j];
                        }
                    }
                    break;
                case Operation.Down:
                    for (int i = 0; i < rage; i++)
                    {
                        for (int j = 0; j < rage; j++)
                        {
                            line[rage - j - 1] = playMat[j, i];
                        }
                        ChangeLine(line);
                        for (int j = 0; j < rage; j++)
                        {
                            playMat[j, i] = line[rage - j - 1];
                        }
                    }
                    break;
                case Operation.Left:
                    for (int i = 0; i < rage; i++)
                    {
                        for (int j = 0; j < rage; j++)
                        {
                            line[j] = playMat[i, j];
                        }
                        ChangeLine(line);
                        for (int j = 0; j < rage; j++)
                        {
                            playMat[i, j] = line[j];
                        }
                    }
                    break;
                case Operation.Right:
                    for (int i = 0; i < rage; i++)
                    {
                        for (int j = 0; j < rage; j++)
                        {
                            line[rage - j - 1] = playMat[i, j];
                        }
                        ChangeLine(line);
                        for (int j = 0; j < rage; j++)
                        {
                            playMat[i, j] = line[rage - j - 1];
                        }
                    }
                    break;
            }
            // 如果操作没有使数字发生变化，视为无效操作
            if (!Compare(playMatOld, playMat))
            {
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

        private void ChangeLine(int[] line)
        {
            Move(line);
            Merge(line);
            Move(line);
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

        private void Merge(int[] line)
        {
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (line[i] == line[i + 1] && line[i] != 0)
                {
                    line[i] *= 2;
                    line[i + 1] = 0;
                    score += line[i];
                }
            }
        }

        private void Copy(int[,] target, int[,] source)
        {
            for (int i = 0; i < rage; i++)
            {
                for (int j = 0; j < rage; j++)
                {
                    target[i, j] = source[i, j];
                }
            }
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
    }
}
