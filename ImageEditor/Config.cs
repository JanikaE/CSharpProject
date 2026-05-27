using System.Collections.Generic;
using System.Drawing;
using Utils.Config;

namespace ImageEditor
{
    public class Config : BaseConfig<Config>
    {
        public List<ColorList> ColorLists { get; set; } = [];

        /// <summary>
        /// 图片分割像素大小，默认 120
        /// </summary>
        public int SplitPixelSize { get; set; } = 120;

        /// <summary>
        /// 导出单张图片的像素大小，默认 120
        /// </summary>
        public int ExportPixelSize { get; set; } = 120;

        /// <summary>
        /// 导出文件名前缀，默认"测试"
        /// </summary>
        public string ExportFileName { get; set; } = "测试";

        /// <summary>
        /// 上次导出目录，默认为空
        /// </summary>
        public string LastExportDirectory { get; set; } = "";
    }

    public class ColorList
    {
        public string Name { get; set; }

        public List<Color> Colors { get; set; }
    }
}
