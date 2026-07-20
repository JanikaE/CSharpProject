using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace UsefulTools.Converters
{
    public enum NameCase
    {
        PascalCase,
        CamelCase,
        SnakeCase
    }

    public static class NameCaseConverter
    {
        private static readonly JsonSerializerOptions _prettyOptions = new()
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public static string AutoConvert(string input, NameCase targetCase)
        {
            var trimmed = input.TrimStart();
            if (trimmed.StartsWith("{") || trimmed.StartsWith("["))
                return ConvertJson(input, targetCase);
            if (trimmed.StartsWith("<"))
                return ConvertXml(input, targetCase);
            throw new FormatException("无法识别输入格式。请输入有效的 JSON 或 XML 字符串。");
        }

        public static string ConvertJson(string json, NameCase targetCase)
        {
            var node = JsonNode.Parse(json);
            if (node == null)
                throw new FormatException("JSON 解析失败。请检查输入格式。");
            RenameJsonProperties(node, targetCase);
            return node.ToJsonString(_prettyOptions);
        }

        public static string ConvertXml(string xml, NameCase targetCase)
        {
            var doc = XDocument.Parse(xml);
            if (doc.Root == null)
                throw new FormatException("XML 解析失败。请检查输入格式。");
            var newRoot = RenameXmlElement(doc.Root, targetCase);
            var newDoc = new XDocument(doc.Declaration, newRoot);
            return newDoc.ToString();
        }

        public static string ConvertName(string name, NameCase targetCase)
        {
            var words = SplitWords(name);
            if (words.Count == 0)
                return name;

            return targetCase switch
            {
                NameCase.PascalCase => ToPascalCase(words),
                NameCase.CamelCase => ToCamelCase(words),
                NameCase.SnakeCase => ToSnakeCase(words),
                _ => name
            };
        }

        private static List<string> SplitWords(string name)
        {
            var words = new List<string>();
            var current = new StringBuilder();

            foreach (char c in name)
            {
                if (c == '_')
                {
                    if (current.Length > 0)
                    {
                        words.Add(current.ToString());
                        current.Clear();
                    }
                }
                else if (char.IsUpper(c) && current.Length > 0 && !char.IsUpper(current[^1]))
                {
                    words.Add(current.ToString());
                    current.Clear();
                    current.Append(c);
                }
                else
                {
                    current.Append(c);
                }
            }

            if (current.Length > 0)
                words.Add(current.ToString());

            return words;
        }

        private static string ToPascalCase(List<string> words)
        {
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    sb.Append(char.ToUpper(word[0]));
                    sb.Append(word[1..].ToLower());
                }
            }
            return sb.ToString();
        }

        private static string ToCamelCase(List<string> words)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < words.Count; i++)
            {
                var word = words[i];
                if (word.Length > 0)
                {
                    sb.Append(i == 0 ? char.ToLower(word[0]) : char.ToUpper(word[0]));
                    sb.Append(word[1..].ToLower());
                }
            }
            return sb.ToString();
        }

        private static string ToSnakeCase(List<string> words)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < words.Count; i++)
            {
                if (i > 0)
                    sb.Append('_');
                sb.Append(words[i].ToLower());
            }
            return sb.ToString();
        }

        private static void RenameJsonProperties(JsonNode node, NameCase targetCase)
        {
            if (node is JsonObject obj)
            {
                var renames = new Dictionary<string, string>();
                foreach (var kvp in obj)
                {
                    var newName = ConvertName(kvp.Key, targetCase);
                    if (newName != kvp.Key)
                        renames[kvp.Key] = newName;
                }

                foreach (var rename in renames)
                {
                    var value = obj[rename.Key];
                    obj.Remove(rename.Key);
                    obj[rename.Value] = value?.DeepClone();
                }

                foreach (var kvp in obj)
                {
                    RenameJsonProperties(kvp.Value, targetCase);
                }
            }
            else if (node is JsonArray arr)
            {
                foreach (var item in arr)
                {
                    if (item != null)
                        RenameJsonProperties(item, targetCase);
                }
            }
        }

        private static XElement RenameXmlElement(XElement element, NameCase targetCase)
        {
            var ns = element.GetDefaultNamespace();
            var newName = string.IsNullOrEmpty(element.GetPrefixOfNamespace(ns))
                ? ConvertName(element.Name.LocalName, targetCase)
                : element.GetPrefixOfNamespace(ns) + ":" + ConvertName(element.Name.LocalName, targetCase);

            // Use the same namespace as original
            var xName = element.Name.Namespace + ConvertName(element.Name.LocalName, targetCase);
            var newElement = new XElement(xName);

            foreach (var attr in element.Attributes())
                newElement.Add(attr);

            foreach (var node in element.Nodes())
            {
                if (node is XElement childElement)
                    newElement.Add(RenameXmlElement(childElement, targetCase));
                else
                    newElement.Add(node);
            }

            return newElement;
        }
    }
}
