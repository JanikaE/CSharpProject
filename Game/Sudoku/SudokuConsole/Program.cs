using Sudoku.Game;

namespace SudokuConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                DateTime start = DateTime.Now;
                Puzzel puzzel = new(4, 4);
                puzzel.InitPosibleNums();
                puzzel.SolveBuster(null, true);
                //Console.WriteLine(PuzzelUtils.ContentString(puzzel));
                DateTime end = DateTime.Now;
                Console.WriteLine(" " + (end - start).TotalSeconds);
                //if (puzzel.CountBlank() > 0)
                //{
                //    Console.WriteLine("\n" + PuzzelUtils.ContentString(puzzel));
                //    Console.ReadLine();
                //}
            }
        }
    }
}
