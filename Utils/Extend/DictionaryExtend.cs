using System.Collections.Generic;

namespace Utils.Extend
{
    public static class DictionaryExtend
    {
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> keyValuePairs) where TKey : notnull
        {
            foreach (var pair in keyValuePairs)
            {
                if (!dictionary.ContainsKey(pair.Key))
                {
                    dictionary.Add(pair.Key, pair.Value);
                }
            }
        }
    }
}
