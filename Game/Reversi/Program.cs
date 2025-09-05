#define Form

#if Console
using Utils.Mathematical;
using Utils.Tool;
#endif

using System;
using System.Windows.Forms;

namespace Reversi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
#if Form
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
#elif Console
            Box game = new();
            while (true)
            {                
                Console.SetCursorPosition(0, 0);
                game.PrintToConsole();
                ConsoleTool.ClearCurrentConsoleLine();
                if (game.state == State.End)
                {
                    break;
                }
                char[] s = Console.ReadLine().ToCharArray();
                int[] ints = new int[2];
                int index = 0;
                foreach (char c in s)
                {
                    if (int.TryParse(c.ToString(), out int result))
                    {
                        ints[index++] = result;
                        if (index == 2)
                        {
                            break;
                        }
                    }
                }
                Point2D point = new(ints[0], ints[1]);
                game.Drop(point, game.turn);                
            }
            Console.WriteLine("Over");
            Console.ReadLine();
#endif
        }
    }
}