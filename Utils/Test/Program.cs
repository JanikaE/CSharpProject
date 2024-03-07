using Utils.Extend;
using Utils.Tool;

internal class Program
{
    private static void Main()
    {
        string s = "t41234254.15156465.34514515author)\rLUES ('15420877.65607321600', '0.', ' '0', 'dris_yydj', '1000', '',T I144651654NTO \"EWCP";
        var l = s.GetDecimalList();
        Console.WriteLine(l.ToStringByItem());
    }
}