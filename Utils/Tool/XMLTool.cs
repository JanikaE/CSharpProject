using System.IO;
using System.Xml.Serialization;

namespace Utils.Tool
{
    public static class XMLTool
    {
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
        public static T? FromXmlFile<T>(string fileName)
        {
            using FileStream fs = new(fileName, FileMode.Open);
            XmlSerializer serializer = new(typeof(T));
            return (T?)serializer.Deserialize(fs);
        }
    }
}
