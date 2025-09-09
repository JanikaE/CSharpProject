using FileManager.Configs;
using System.Windows.Forms;

namespace FileManager.Controls
{
    public partial class RichTextBoxPathPair : RichTextBox
    {
        public PathPair pathPair;

        public RichTextBoxPathPair(PathPair pathPair)
        {
            InitializeComponent();
            this.pathPair = pathPair;
        }
    }
}
