using System;

namespace EfficientSnake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Box box = new(10);
            box.Init();
            box.Run();
        }
    }
}
