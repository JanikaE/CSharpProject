using System;

namespace EfficientSnake
{
    internal class Program
    {
        static void Main()
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out int n))
            {
                while (true)
                {
                    Box box = new(n);
                    box.Run();
                    Console.ReadLine();
                }
            }
        }
    }
}
