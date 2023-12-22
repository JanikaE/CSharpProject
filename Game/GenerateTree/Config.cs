using System.Configuration;
using System.Xml.Serialization;
using Utils.Tool;

namespace GenerateTree
{
    public class Config
    {
        [XmlIgnore]
        public static readonly string XMLPath = Path.Combine(Application.StartupPath, "config\\");

        /// <summary>
        /// 根节点角度
        /// </summary>
        public  float RootRad { get; set; } = 0f;

        /// <summary>
        /// 根节点尺寸
        /// </summary>
        public  float RootSize { get; set; } = 20;

        /// <summary>
        /// 根节点长度
        /// </summary>
        public  float RootLength { get; set; } = 50;

        /// <summary>
        /// 树最大深度
        /// </summary>
        public  int MaxDepth { get; set; } = 5;

        /// <summary>
        /// 子节点最小尺寸
        /// </summary>
        public  float MinSize { get; set; } = 1f;

        /// <summary>
        /// 子节点最小长度
        /// </summary>
        public  float MinLength { get; set; } = 2f;

        /// <summary>
        /// 子节点最大角度
        /// </summary>
        public  float MaxRad { get; set; } = 45;

        /// <summary>
        /// 子节点数量（正态分布均值）
        /// </summary>
        public  int ChildrenNumMu { get; set; } = 3;

        /// <summary>
        /// 子节点数量（正态分布标准差）
        /// </summary>
        public  float ChildrenNumSigma { get; set; } = 1.5f;

        /// <summary>
        /// 子节点尺寸变化（正态分布均值）
        /// </summary>
        public  float SizeChangeMu { get; set; } = 0.8f;

        /// <summary>
        /// 子节点尺寸变化（正态分布标准差）
        /// </summary>
        public  float SizeChangeSigma { get; set; } = 0.1f;

        /// <summary>
        /// 子节点长度变化（正态分布均值）
        /// </summary>
        public  float LengthChangeMu { get; set; } = 0.8f;

        /// <summary>
        /// 子节点长度变化（正态分布标准差）
        /// </summary>
        public  float LengthChangeSigma { get; set; } = 0.1f;

        public void Save()
        {
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml";
            string filePath = Path.Combine(XMLPath, fileName);
            DirectoryTool.CreateDirectoryByFilePath(filePath);
            XMLTool.ToXmlFile(this, filePath);
            MessageBox.Show($"Save Success:{fileName}");
        }
    }
}