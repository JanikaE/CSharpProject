using BackupTool.Config;
using System.Windows.Forms;

namespace BackupTool.Controls
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
