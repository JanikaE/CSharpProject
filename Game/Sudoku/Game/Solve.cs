using Utils.Extend;

namespace Sudoku.Game
{
    public partial class Puzzel
    {
        public void StartSolve(MainForm? mainform = null)
        {
            List<Func<string>> Funcs = new()
            {
                NakedSingle,
                HiddenSingle,
                HiddenTuple
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
                        continue;
                    if (cell.posibleNums.Count == 0)
                    {
                        throw new SolveException($"HiddenSingle  {cell.Name}  Error");
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
                foreach(int index in house)
                {
                    if (PlayMat(index).num == 0)
                    {
                        blankCell.Add(index);
                    }
                }

                foreach (int index in blankCell)
                {
                    List<int> tupleNum = PlayMat(index).posibleNums;
                    List<int> tupleIndex = new(){ index };
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
                                tupleLocation += $"{PlayMat(i).Name}; ";
                            }
                            return $"Hidden Tuple  {pair.Key}  {tupleLocation}  {tupleNum.ToStringByItem()}";
                        }                        
                    }
                }
            }
            return string.Empty;
        }
    }
}
