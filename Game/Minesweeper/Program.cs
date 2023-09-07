namespace Minesweeper 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Box box = new(10, 20, 10);
            box.StartNew();
            box.Open(5, 5);
            box.Show();
            Console.ReadLine();
        }
    }
}
