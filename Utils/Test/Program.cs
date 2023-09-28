using Utils.Mathematical;

internal class Program
{
    private static void Main(string[] args)
    {
        Complex a = new(1, 1);
        int b = 0;
        Complex c = a / b;
        Console.WriteLine(c.ToString());
    }
}