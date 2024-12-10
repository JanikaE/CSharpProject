using System.Collections.Generic;

namespace Utils.Extend
{
    public static class DictionaryExtend
    {
        public static void Append<TKey, TValue>(this Dictionary<TKey, TValue> dicSource, Dictionary<TKey, TValue> dicAppend, bool overWrite) where TKey : notnull
        {
            dicSource ??= new Dictionary<TKey, TValue>();
            if (dicAppend == null)
            {
                return;
            }

            foreach (var item in dicAppend)
            {
                if (!dicSource.ContainsKey(item.Key) || overWrite)
                {
                    dicSource.Add(item.Key, item.Value);
                }
            }
        }

        public static TValue? GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dicSource, TKey key) where TKey : notnull
        {
            if (dicSource != null && dicSource.ContainsKey(key))
            {
                return dicSource[key];
            }
            else
            {
                return default;
            }
        }
    }
}
