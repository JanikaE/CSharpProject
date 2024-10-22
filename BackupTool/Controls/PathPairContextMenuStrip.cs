using BackupTool.Config;
using System.Windows.Forms;

namespace BackupTool.Controls
{
    public partial class PathPairContextMenuStrip : ContextMenuStrip
    {
        public PathPair pathPair;

        public PathPairContextMenuStrip(PathPair pathPair)
        {
            InitializeComponent();
            this.pathPair = pathPair;
        }
    }
}
