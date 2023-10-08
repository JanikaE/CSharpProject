namespace GameOfLife
{
    public class Program
    {
        private static void Main()
        {
            Box box = new();
            //box.Init(30, 30);
            box.Init(Seed.seeds[0]);
            while (true)
            {
                Console.Clear();
                box.Print();
                box.Update();
                Thread.Sleep(200);
            }
        }
    }
}