using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Utils.Tool
{
    public static class XmlTool
    {
        public static byte[] ToBinary(object obj)
        {
            using MemoryStream fs = new();
            XmlSerializer x = new(obj.GetType());
            x.Serialize(fs, obj);
            fs.Flush();
            return fs.ToArray();
        }

        public static T ToObject<T>(byte[] data)
        {
            using MemoryStream sr = new(data);
            XmlSerializer x = new(typeof(T));
            return (T)x.Deserialize(sr);
        }

        public static T ToObject<T>(string dataString)
        {
            using StringReader reader = new(dataString);
            XmlSerializer x = new(typeof(T));
            return (T)x.Deserialize(reader);
        }

        public static string ToString(object obj)
        {
            StringBuilder sb = new();
            using StringWriter writer = new(sb);
            XmlSerializer x = new(obj.GetType());
            x.Serialize(writer, obj);
            writer.Flush();
            return sb.ToString();
        }

        public static T Copy<T>(T obj)
        {
            return ToObject<T>(ToBinary(obj));
        }

        /// <summary>
        /// 将数据序列化为XML并写入文件
        /// </summary>
        public static void ToXmlFile(object data, string fileName)
        {
            using FileStream fs = new(fileName, FileMode.Create);
            XmlSerializer serializer = new(data.GetType());
            serializer.Serialize(fs, data);
        }

        /// <summary>
        /// 将指定XML数据文件还原为强类型数据
        /// </summary>
        public static T FromXmlFile<T>(string fileName)
        {
            using FileStream fs = new(fileName, FileMode.Open);
            XmlSerializer serializer = new(typeof(T));
            return (T)serializer.Deserialize(fs);
        }
    }
}
