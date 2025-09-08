using FileManager.Configs;
using System.Windows.Forms;

namespace FileManager.Controls
{
    public partial class PathPairRichTextBox : RichTextBox
    {
        public PathPair pathPair;

        public PathPairRichTextBox(PathPair pathPair)
        {
            InitializeComponent();
            this.pathPair = pathPair;
        }
    }
}
