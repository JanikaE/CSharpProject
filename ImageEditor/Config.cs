using System.Collections.Generic;
using System.Drawing;
using Utils.Config;

namespace ImageEditor
{
    public class Config : BaseConfig<Config>
    {
        public List<ColorList> ColorLists { get; set; } = [];
    }

    public class ColorList
    {
        public string Name { get; set; }

        public List<Color> Colors { get; set; }
    }
}
