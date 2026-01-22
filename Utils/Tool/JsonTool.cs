using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Utils.Tool
{
    public static class JsonTool
    {
        /// <summary>
        /// 使用 JSONPath 语法更新或创建节点
        /// </summary>
        public static string UpdateJsonByPath(string jsonString, string jsonPath, object newValue)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                return jsonString;

            JToken token;

            // 尝试解析为 JObject 或 JArray
            try
            {
                token = JToken.Parse(jsonString);
            }
            catch
            {
                // 如果不是有效的 JSON，返回原始字符串
                return jsonString;
            }

            // 尝试获取现有节点
            JToken selectedToken = token.SelectToken(jsonPath);

            if (selectedToken != null)
            {
                // 节点存在，替换值
                var value = JToken.FromObject(newValue);
                selectedToken.Replace(value);
            }
            else
            {
                // 节点不存在，需要创建路径
                CreatePathAndSetValue(token, jsonPath, newValue);
            }

            return token.ToString();
        }

        /// <summary>
        /// 创建路径并设置值
        /// </summary>
        private static void CreatePathAndSetValue(JToken rootToken, string jsonPath, object value)
        {
            // 使用 JSONPath 创建器
            var pathParts = ParseJsonPath(jsonPath);
            JToken current = rootToken;

            for (int i = 0; i < pathParts.Count; i++)
            {
                var part = pathParts[i];

                if (i == pathParts.Count - 1)
                {
                    // 最后一部分，设置值
                    if (part.IsArrayIndex)
                    {
                        var array = GetOrCreateArray(current, part.PropertyName);
                        SetArrayValue(array, part.ArrayIndex, value);
                    }
                    else
                    {
                        if (current is JObject obj)
                        {
                            obj[part.PropertyName] = JToken.FromObject(value);
                        }
                    }
                }
                else
                {
                    // 中间部分，确保节点存在
                    if (part.IsArrayIndex)
                    {
                        var array = GetOrCreateArray(current, part.PropertyName);
                        current = GetOrCreateArrayElement(array, part.ArrayIndex);
                    }
                    else
                    {
                        current = GetOrCreateObjectProperty(current, part.PropertyName);
                    }
                }
            }
        }

        /// <summary>
        /// 解析 JSON 路径
        /// </summary>
        private static List<PathPart> ParseJsonPath(string jsonPath)
        {
            var parts = new List<PathPart>();
            var segments = jsonPath.Split('.');

            foreach (var segment in segments)
            {
                if (segment.Contains('['))
                {
                    int bracketStart = segment.IndexOf('[');
                    int bracketEnd = segment.IndexOf(']');

                    string propertyName = segment[..bracketStart];
                    string indexStr = segment.Substring(bracketStart + 1, bracketEnd - bracketStart - 1);

                    if (int.TryParse(indexStr, out int index))
                    {
                        parts.Add(new PathPart { PropertyName = propertyName, IsArrayIndex = true, ArrayIndex = index });
                    }
                    else
                    {
                        parts.Add(new PathPart { PropertyName = segment, IsArrayIndex = false });
                    }
                }
                else
                {
                    parts.Add(new PathPart { PropertyName = segment, IsArrayIndex = false });
                }
            }

            return parts;
        }

        private static JArray GetOrCreateArray(JToken parent, string propertyName)
        {
            if (parent[propertyName] == null)
            {
                parent[propertyName] = new JArray();
            }
            return parent[propertyName] as JArray;
        }

        private static JToken GetOrCreateArrayElement(JArray array, int index)
        {
            while (array.Count <= index)
            {
                array.Add(new JObject());
            }
            return array[index];
        }

        private static JToken GetOrCreateObjectProperty(JToken parent, string propertyName)
        {
            if (parent[propertyName] == null)
            {
                parent[propertyName] = new JObject();
            }
            return parent[propertyName];
        }

        private static void SetArrayValue(JArray array, int index, object value)
        {
            while (array.Count <= index)
            {
                array.Add(JValue.CreateNull());
            }
            array[index] = JToken.FromObject(value);
        }

        private class PathPart
        {
            public string PropertyName { get; set; }
            public bool IsArrayIndex { get; set; }
            public int ArrayIndex { get; set; }
        }
    }
}