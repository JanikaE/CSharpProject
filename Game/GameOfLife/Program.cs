using System;
using System.Threading;

namespace GameOfLife
{
    public class Program
    {
        private static void Main()
        {
            Box box = new();
            box.RandomInit(5, 5, 20, 20);
            //box.Init(Seed.seeds[0]);
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                box.Print();
                box.Update();
                Thread.Sleep(1000);
            }
        }
    }
}