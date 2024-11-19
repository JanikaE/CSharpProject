using System.Diagnostics;

namespace Utils.Sample
{
    public class DebugSample
    {
        public static void WriteTrace(string str)
        {
            Trace.TraceInformation(str);
        }

        public static void WriteDebug(string str)
        {
            Debug.Write(str);
        }
    }
}
