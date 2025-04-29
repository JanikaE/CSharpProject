using System.Collections.Generic;
using System.Text;

namespace Utils.Tool
{
    public static class FormatTool
    {
        public static List<int> ConvertStringToList(string s, string separator = ",")
        {
            s ??= "";
            string[] arr = s.Split(separator);
            List<int> result = new();
            for (int i = 0; i < arr.Length; i++)
            {
                if (!string.IsNullOrEmpty(arr[i]))
                {
                    result.Add(int.Parse(arr[i]));
                }
            }
            return result;
        }

        public static string ConvertListToString(List<int> list, string separator = ",")
        {
            StringBuilder s = new();
            for (int i = 0; i < list.Count; i++)
            {
                s.Append(list[i].ToString() + separator);
            }
            return s.ToString();
        }
    }
}
