using System.Collections.Generic;
using System.Text;

namespace Utils.Tool
{
    public static class FormatTool
    {
        public static List<int> ConvertStringToList(string s)
        {
            s ??= "";
            string[] arr = s.Split(',');
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

        public static string ConvertListToString(List<int> list)
        {
            StringBuilder s = new();
            for (int i = 0; i < list.Count; i++)
            {
                s.Append(list[i].ToString() + ",");
            }
            return s.ToString();
        }
    }
}
