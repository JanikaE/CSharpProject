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
                //string? s = Console.ReadLine();
                //if (s != null)
                //{
                //    if (s == "q")
                //    {
                //        break;
                //    }
                    try
                    {
                        DateTime start = DateTime.Now;
                        Puzzel puzzel = new(4, 4);
                        puzzel.InitPosibleNums();
                        puzzel.SolveBuster();
                        //Console.WriteLine(PuzzelUtils.ContentString(puzzel));
                        DateTime end = DateTime.Now;
                        Console.WriteLine("\n" + (end - start).TotalSeconds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                //}
            }
        }
    }
}
