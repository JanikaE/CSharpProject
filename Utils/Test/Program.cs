using Utils.Extend;
using Utils.Mathematical;

internal class Program
{
    private static void Main()
    {
        RelativePosition_8? ne = RelativePosition_8.Left.Previous();
        Console.WriteLine(ne == null ? "null" : ne);
        Console.ReadLine();
    }
}