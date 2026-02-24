using Utils.Extend;
using Utils.Mathematical;

internal class Program
{
    private static void Main()
    {
        Map2D<int> map = new Map2D<int>(6, 8, (i, j) => Random.Shared.Next(10, 100));
        Console.WriteLine("Original Map:");
        for (int i = 0; i < map.Height; i++)
        {
            for (int j = 0; j < map.Width; j++)
            {
                Console.Write($"{map[i, j]} ");
            }
            Console.WriteLine();
        }

        map.Sort((a, b) => a.CompareTo(b), false);
        Console.WriteLine("Sorted Map:");
        for (int i = 0; i < map.Height; i++)
        {
            for (int j = 0; j < map.Width; j++)
            {
                Console.Write($"{map[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}