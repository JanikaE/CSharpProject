﻿using Sudoku.Game;

namespace SudokuConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                DateTime start = DateTime.Now;
                Puzzel puzzel = new(7, 7);
                puzzel.InitPosibleNums();
                puzzel.SolveBuster(null, true);
                DateTime end = DateTime.Now;
                Console.WriteLine(" " + (end - start).TotalSeconds);
            }
        }
    }
}
