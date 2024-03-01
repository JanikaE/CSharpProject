using Sudoku.Snap;
using Utils.Extend;
using Utils.Mathematical;

namespace Sudoku.Game
{
    public partial class Puzzel
    {
        #region Buster

        /// <summary>
        /// 暴力求解
        /// </summary>
        /// <param name="mainform"></param>
        internal void SolveBuster(MainForm mainform)
        {
            List<Func<string>> Funcs = new()
            {
                NakedSingle,
                HiddenSingle,
            };
            // 记录猜测
            Stack<Guess> guessStack = new();
            while (true)
            {
                string result = string.Empty;
                bool flag = true;
                try
                {
                    foreach (Func<string> func in Funcs)
                    {
                        result = func();
                        if (result != string.Empty)
                        {
                            UpdatePosibleNums();
                            mainform?.AddSolveStep(result, this);
                            break;
                        }
                    }
                }
                catch (SolveException)
                {
                    flag = false;
                }

                if (flag)
                {
                    // 无法继续时选择一个格子猜测
                    if (result == string.Empty)
                    {
                        Cell? toGuess = GetGuessTarget();
                        // 没有格子可猜时，可以认为已经完成了
                        if (toGuess == null)
                        {
                            mainform?.AddSolveStep("Over.", this);
                            break;
                        }
                        else
                        {
                            PuzzelSnap puzzelSnap = new(this);
                            int guessNum = GetGuessNum(toGuess);
                            toGuess.num = guessNum;
                            UpdatePosibleNums();
                            guessStack.Push(new Guess(toGuess.Position, puzzelSnap, guessNum));
                            mainform?.AddSolveStep($"Guess  {toGuess.Name}  value:{toGuess.num}", this);
                        }
                    }
                }
                else
                {
                    // 出错时回溯至上一次猜测前
                    if (guessStack.Any())
                    {
                        (Cell cell, int num) =  TraceBack(guessStack);
                        mainform?.AddSolveStep($"GuessBack  {cell.Name}  wrong value:{num}", this);
                    }
                    // 在没有猜测的情况下出错，直接停止
                    else
                    {
                        mainform?.AddSolveStep("Stop.", this);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 暴力求解
        /// </summary>
        /// <returns>成功或失败</returns>
        public bool SolveBuster()
        {
            List<Func<string>> Funcs = new()
            {
                NakedSingle,
                HiddenSingle,
            };
            // 记录猜测
            Stack<Guess> guessStack = new();
            while (true)
            {
                string result = string.Empty;
                bool flag = true;
                try
                {
                    foreach (Func<string> func in Funcs)
                    {
                        result = func();
                        if (result != string.Empty)
                        {
                            UpdatePosibleNums();
                            break;
                        }
                    }
                }
                catch (SolveException)
                {
                    flag = false;
                }

                if (flag)
                {
                    // 无法继续时选择一个格子猜测
                    if (result == string.Empty)
                    {
                        Cell? toGuess = GetGuessTarget();
                        // 没有格子可猜时，可以认为已经完成了
                        if (toGuess == null)
                        {
                            return true;
                        }
                        else
                        {
                            PuzzelSnap puzzelSnap = new(this);
                            int guessNum = GetGuessNum(toGuess);
                            toGuess.num = guessNum;
                            UpdatePosibleNums();
                            guessStack.Push(new Guess(toGuess.Position, puzzelSnap, guessNum));
                        }
                    }
                }
                else
                {
                    // 出错时回溯至上一次猜测前
                    if (guessStack.Any())
                    {
                        TraceBack(guessStack);
                    }
                    // 在没有猜测的情况下出错，直接停止
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 暴力求非唯一解
        /// </summary>
        /// <returns>所有可能的解</returns>
        public List<PuzzelSnap> SolveMultipleBuster()
        {
            List<PuzzelSnap> results = new();
            List<Func<string>> Funcs = new()
            {
                NakedSingle,
                HiddenSingle,
            };
            // 记录猜测
            Stack<Guess> guessStack = new();
            while (true)
            {
                string result = string.Empty;
                bool flag = true;
                try
                {
                    foreach (Func<string> func in Funcs)
                    {
                        result = func();
                        if (result != string.Empty)
                        {
                            UpdatePosibleNums();
                            break;
                        }
                    }
                }
                catch (SolveException)
                {
                    flag = false;
                }

                if (flag)
                {
                    // 无法继续时选择一个格子猜测
                    if (result == string.Empty)
                    {
                        Cell? toGuess = GetGuessTarget();
                        // 没有格子可猜时，可以认为已经完成了
                        if (toGuess == null)
                        {
                            results.Add(new(this));
                            // 回溯，寻找其他解
                            if (guessStack.Any())
                            {
                                TraceBack(guessStack);
                            }
                            else
                            {
                                return results;
                            }
                        }
                        else
                        {
                            PuzzelSnap puzzelSnap = new(this);
                            int guessNum = GetGuessNum(toGuess);
                            toGuess.num = guessNum;
                            UpdatePosibleNums();
                            guessStack.Push(new Guess(toGuess.Position, puzzelSnap, guessNum));
                        }
                    }
                }
                else
                {
                    // 出错时回溯至上一次猜测前
                    if (guessStack.Any())
                    {
                        TraceBack(guessStack);
                    }
                    // 在没有猜测的情况下出错，直接停止
                    else
                    {
                        return results;
                    }
                }
            }
        }

        /// <summary>
        /// 暴力求是否有多解
        /// </summary>
        /// <returns>0 无解，1 唯一解，2 多解</returns>
        public int SolveCountBuster()
        {
            int count = 0;
            List<Func<string>> Funcs = new()
            {
                NakedSingle,
                HiddenSingle,
            };
            // 记录猜测
            Stack<Guess> guessStack = new();
            while (true)
            {
                string result = string.Empty;
                bool flag = true;
                try
                {
                    foreach (Func<string> func in Funcs)
                    {
                        result = func();
                        if (result != string.Empty)
                        {
                            UpdatePosibleNums();
                            break;
                        }
                    }
                }
                catch (SolveException)
                {
                    flag = false;
                }

                if (flag)
                {
                    // 无法继续时选择一个格子猜测
                    if (result == string.Empty)
                    {
                        Cell? toGuess = GetGuessTarget();
                        // 没有格子可猜时，可以认为已经完成了
                        if (toGuess == null)
                        {
                            count++;
                            if (count == 2)
                            {
                                return count;
                            }
                            // 回溯，寻找其他解
                            if (guessStack.Any())
                            {
                                TraceBack(guessStack);
                            }
                            else
                            {
                                return count;
                            }
                        }
                        else
                        {
                            PuzzelSnap puzzelSnap = new(this);
                            int guessNum = GetGuessNum(toGuess);
                            toGuess.num = guessNum;
                            UpdatePosibleNums();
                            guessStack.Push(new Guess(toGuess.Position, puzzelSnap, guessNum));
                        }
                    }
                }
                else
                {
                    // 出错时回溯至上一次猜测前
                    if (guessStack.Any())
                    {
                        TraceBack(guessStack);
                    }
                    // 在没有猜测的情况下出错，直接停止
                    else
                    {
                        return count;
                    }
                }
            }
        }

        private Cell? GetGuessTarget()
        {
            for (int i = 2; i <= Length; i++)
            {
                foreach (Cell cell in playMat)
                {
                    if (cell.num == 0 && cell.posibleNums.Count == i)
                    {
                        return cell;
                    }
                }
            }
            return null;
        }

        private static int GetGuessNum(Cell cell)
        {
            Random random = new();
            int index = random.Next(0, cell.posibleNums.Count);
            return cell.posibleNums[index];
        }

        /// <summary>
        /// 回溯
        /// </summary>
        /// <param name="guessStack"></param>
        /// <returns></returns>
        private (Cell, int) TraceBack(Stack<Guess> guessStack)
        {
            Guess trace = guessStack.Pop();

            PuzzelSnap puzzelSnap = trace.puzzelSnap;
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    Cell cell = PlayMat(row, col);
                    CellSnap cellSnap = puzzelSnap.playMat[row, col];
                    cell.num = cellSnap.num;
                    cell.posibleNums = cellSnap.posibleNums.ToList();
                }
            }

            Cell thisCell = PlayMat(trace.position);
            int num = trace.num;
            // 将上一次猜的数字排除
            thisCell.posibleNums.Remove(num);
            UpdatePosibleNums();
            return (thisCell, num);
        }

        private struct Guess
        {
            public Point2D position; 
            public PuzzelSnap puzzelSnap;
            public int num;

            public Guess(Point2D position, PuzzelSnap puzzelSnap, int num)
            {
                this.position = position;
                this.puzzelSnap = puzzelSnap;
                this.num = num;
            }
        }

        #endregion

        #region Arts

        /// <summary>
        /// 技巧求解
        /// </summary>
        /// <param name="mainform"></param>
        public void SolveArts(MainForm? mainform = null)
        {
            List<Func<string>> Funcs = new()
            {
                NakedSingle,
                HiddenSingle,
                HiddenTuple,
                XYWing
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
                            mainform?.AddSolveStep(result, this);
                            break;
                        }
                    }
                    if (result == string.Empty)
                    {
                        if (CheckFull())
                        {
                            mainform?.AddSolveStep("Over.", this);
                        }
                        else
                        {
                            mainform?.AddSolveStep("Stop.", this);
                        }
                        break;
                    }
                }
            }
            catch (SolveException se)
            {
                mainform?.AddSolveStep(se.Message, this);
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
                            return $"NakedSingle  {cell.Name}  value:{value}";
                        }
                        if (cell.posibleNums.Count == 0)
                        {
                            throw new SolveException($"NakedSingle  {cell.Name}  Error");
                        }
                    }
                }
            }
            return string.Empty;
        }

        private string HiddenSingle()
        {
            foreach (var pair in Houses)
            {
                List<int> house = pair.Value;
                Dictionary<int, Cell?> valueInCell = new();
                foreach (int index in house)
                {
                    Cell cell = PlayMat(index);
                    if (cell.num != 0)
                    {
                        continue;
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
                        return $"HiddenSingle  {cell.Name}  value:{value}";
                    }
                }
            }
            return string.Empty;
        }

        private string HiddenTuple()
        {
            foreach (var pair in Houses)
            {
                List<int> house = pair.Value;
                List<int> blankCell = new();
                foreach (int index in house)
                {
                    if (PlayMat(index).num == 0)
                    {
                        blankCell.Add(index);
                    }
                }

                foreach (int index in blankCell)
                {
                    List<int> tupleNum = PlayMat(index).posibleNums;
                    List<int> tupleIndex = new() { index };
                    bool success = false;
                    foreach (int another in blankCell)
                    {
                        if (another == index)
                        {
                            continue;
                        }
                        if (tupleNum.Contains(PlayMat(another).posibleNums))
                        {
                            tupleIndex.Add(another);
                        }
                        if (tupleNum.Count == tupleIndex.Count)
                        {
                            success = true;
                            break;
                        }
                    }
                    if (success)
                    {
                        int removeNum = 0;
                        foreach (int remove in blankCell)
                        {
                            if (tupleIndex.Contains(remove))
                            {
                                continue;
                            }
                            removeNum += PlayMat(remove).posibleNums.RemoveAll(n => tupleNum.Contains(n));
                        }
                        if (removeNum > 0)
                        {
                            string tupleLocation = string.Empty;
                            foreach (int i in tupleIndex)
                            {
                                tupleLocation += $"{PlayMat(i).Name},";
                            }
                            return $"Hidden Tuple  {pair.Key}  {tupleLocation}  {tupleNum.ToStringByItem(",")}";
                        }
                    }
                }
            }
            return string.Empty;
        }

        private string XYWing()
        {
            foreach (Cell cell in playMat)
            {
                // cell: xy
                if (cell.num != 0 || cell.posibleNums.Count != 2)
                {
                    continue;
                }
                int a = cell.posibleNums[0];
                int b = cell.posibleNums[1];
                foreach (int index1 in GetRelatedIndex(cell))
                {
                    // cell1: xz
                    Cell cell1 = PlayMat(index1);
                    if (cell1.num != 0 || cell1.posibleNums.Count != 2)
                    {
                        continue;
                    }
                    int c = cell1.posibleNums[0];
                    int d = cell1.posibleNums[1];
                    int x, y, z;
                    if (a == c && b != d)
                    {
                        x = a;
                        y = b;
                        z = d;
                    }
                    else if (a == d && b != c)
                    {
                        x = a;
                        y = b;
                        z = c;
                    }
                    else if (b == c && a != d)
                    {
                        x = b;
                        y = a;
                        z = d;
                    }
                    else if (b == d && a != c)
                    {
                        x = b;
                        y = a;
                        z = c;
                    }
                    else
                    {
                        continue;
                    }

                    foreach (int index2 in GetRelatedIndex(cell))
                    {
                        // cell2: yz
                        Cell cell2 = PlayMat(index2);
                        if (cell2.num != 0 || cell2.posibleNums.Count != 2)
                        {
                            continue;
                        }
                        if (cell2.posibleNums.Contains(y) && cell2.posibleNums.Contains(z))
                        {
                            List<int> related = GetRelatedIndex(cell1, cell2);
                            List<Cell> update = new();
                            foreach (int index in related)
                            {
                                // cell3: not z
                                Cell cell3 = PlayMat(index);
                                if (cell3.posibleNums.Contains(z))
                                {
                                    cell3.posibleNums.Remove(z);
                                    update.Add(cell3);
                                }
                            }
                            if (update.Count > 0)
                            {
                                return $"XY-Wing  {cell.Name},{cell1.Name},{cell2.Name}  x={x},y={y},z={z}  {update.ToStringByItem(",")}";
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        #endregion
    }
}
