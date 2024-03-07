using Sudoku.Snap;
using Utils.Extend;
using Utils.Mathematical;
using Utils.Tool;

namespace Sudoku.Game
{
    public partial class Puzzel
    {
        #region Buster

        /// <summary>
        /// 暴力求解
        /// </summary>
        /// <param name="action">记录所有的步骤</param>
        public void SolveBuster(Action<string, Puzzel>? action = null, bool consoleLog = false)
        {
            List<Func<string>> Funcs = new()
            {
                NakedSingle,
                HiddenSingle,
            };
            // 记录猜测
            Stack<Guess> guessStack = new();
            int loopCnt = 0;
            KeyValuePair<int, int> maxProgress = new(0, 0);
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
                            action?.Invoke(result, this);
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
                            action?.Invoke("Over.", this);
                            break;
                        }
                        else
                        {
                            PuzzelSnap puzzelSnap = new(this);
                            int guessNum = toGuess.possibleNums.GetRandomOne();
                            toGuess.num = guessNum;
                            UpdatePosibleNums();
                            guessStack.Push(new Guess(toGuess.Position, puzzelSnap, guessNum));
                            action?.Invoke($"Guess  {toGuess.Name}  value:{toGuess.num}", this);
                        }
                    }
                }
                else
                {
                    // 出错时回溯至上一次猜测前
                    if (guessStack.Any())
                    {
                        (Cell cell, int num) = TraceBack(guessStack);
                        action?.Invoke($"GuessBack  {cell.Name}  wrong value:{num}", this);
                    }
                    // 在没有猜测的情况下出错，直接停止
                    else
                    {
                        action?.Invoke("Stop.", this);
                        break;
                    }
                }
                loopCnt++;

                // 因未知原因迟迟没有进展时，直接往前多回溯几步
                if (Square - CountBlank() > maxProgress.Key)
                {
                    maxProgress = new(Square - CountBlank(), loopCnt);
                }
                if (loopCnt - maxProgress.Value > Length * 10)
                {
                    TraceBack(guessStack, Length);
                    maxProgress = new(Square - CountBlank(), loopCnt);
                }

                if (consoleLog)
                {
                    ConsoleTool.ClearCurrentConsoleLine();
                    Console.Write(CountBlank() + " " + guessStack.Count + " " + loopCnt);
                    //Console.SetCursorPosition(0, 0);
                    //Console.Write(Utils.ContentString(this));
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
                            int guessNum = toGuess.possibleNums.GetRandomOne();
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
                            int guessNum = toGuess.possibleNums.GetRandomOne();
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
            int patern = 2;
            Cell? result = null;
            int min = Length + 1;
            switch (patern)
            {
                case 1:
                    foreach (Cell cell in playMat)
                    {
                        if (cell.num == 0 && cell.possibleNums.Count < min && cell.possibleNums.Count > 0)
                        {
                            result = cell;
                            min = cell.possibleNums.Count;
                        }
                    }
                    return result;
                case 2:
                    List<Cell> results = new();
                    foreach (Cell cell in playMat)
                    {
                        if (cell.num == 0 && cell.possibleNums.Count > 0)
                        {
                            if (cell.possibleNums.Count < min)
                            {
                                results.Clear();
                                results.Add(cell);
                                min = cell.possibleNums.Count;
                            }
                            else if (cell.possibleNums.Count == min)
                            {
                                results.Add(cell);
                            }
                        }
                    }
                    if (results.Any())
                    {
                        result = results.GetRandomOne();
                    }
                    return result;
                case 3:
                    foreach (Cell cell in playMat)
                    {
                        if (cell.num == 0)
                        {
                            result = cell;
                            break;
                        }
                    }
                    return result;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 回溯
        /// </summary>
        /// <param name="guessStack"></param>
        /// <returns></returns>
        private (Cell, int) TraceBack(Stack<Guess> guessStack, int loop = 1)
        {
            Guess trace = guessStack.Pop();
            for (int i = 1; i < loop; i++)
            {
                if (guessStack.Any())
                {
                    trace = guessStack.Pop();
                }
                else
                {
                    break;
                }
            }

            PuzzelSnap puzzelSnap = trace.puzzelSnap;
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    Cell cell = PlayMat(row, col);
                    CellSnap cellSnap = puzzelSnap.playMat[row, col];
                    cell.num = cellSnap.num;
                    cell.possibleNums = cellSnap.possibleNums.ToList();
                }
            }

            Cell thisCell = PlayMat(trace.position);
            int num = trace.num;
            // 将上一次猜的数字排除
            thisCell.possibleNums.Remove(num);
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
        /// <param name="action">记录所有的步骤</param>
        public void SolveArts(Action<string, Puzzel> action)
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
                            action(result, this);
                            break;
                        }
                    }
                    if (result == string.Empty)
                    {
                        if (CheckFull())
                        {
                            action("Over.", this);
                        }
                        else
                        {
                            action("Stop.", this);
                        }
                        break;
                    }
                }
            }
            catch (SolveException se)
            {
                action(se.Message, this);
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
                        if (cell.possibleNums.Count == 1)
                        {
                            int value = cell.possibleNums.First();
                            PlayMat(row, col).num = value;
                            cell.possibleNums.Clear();
                            return $"NakedSingle  {cell.Name}  value:{value}";
                        }
                        if (cell.possibleNums.Count == 0)
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
                        foreach (int value in cell.possibleNums)
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
                        cell.possibleNums.Clear();
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
                    List<int> tupleNum = PlayMat(index).possibleNums;
                    List<int> tupleIndex = new() { index };
                    bool success = false;
                    foreach (int another in blankCell)
                    {
                        if (another == index)
                        {
                            continue;
                        }
                        if (tupleNum.Contains(PlayMat(another).possibleNums))
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
                            removeNum += PlayMat(remove).possibleNums.RemoveAll(n => tupleNum.Contains(n));
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
                if (cell.num != 0 || cell.possibleNums.Count != 2)
                {
                    continue;
                }
                int a = cell.possibleNums[0];
                int b = cell.possibleNums[1];
                foreach (int index1 in GetRelatedIndex(cell))
                {
                    // cell1: xz
                    Cell cell1 = PlayMat(index1);
                    if (cell1.num != 0 || cell1.possibleNums.Count != 2)
                    {
                        continue;
                    }
                    int c = cell1.possibleNums[0];
                    int d = cell1.possibleNums[1];
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
                        if (cell2.num != 0 || cell2.possibleNums.Count != 2)
                        {
                            continue;
                        }
                        if (cell2.possibleNums.Contains(y) && cell2.possibleNums.Contains(z))
                        {
                            List<int> related = GetRelatedIndex(cell1, cell2);
                            List<Cell> update = new();
                            foreach (int index in related)
                            {
                                // cell3: not z
                                Cell cell3 = PlayMat(index);
                                if (cell3.possibleNums.Contains(z))
                                {
                                    cell3.possibleNums.Remove(z);
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
