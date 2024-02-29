using Sudoku.Game;
using Utils.Extend;

namespace SudokuConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string? s = Console.ReadLine();
                if (s != null)
                {
                    if (s == "q")
                    {
                        break;
                    }
                    try
                    {
                        Puzzel puzzel = new(3, 3);
                        puzzel.GenerateByExample(s);
                        puzzel.InitPosibleNums();
                        //Console.WriteLine(puzzel.SolveBuster());
                        //Console.WriteLine(PuzzelUtils.ContentString(puzzel));
                        Console.WriteLine(puzzel.SolveBusterMultiple().ToStringByItem(PuzzelUtils.ContentString, "\n"));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }
}
