using System.Collections.Generic;
using System.Windows.Forms;
using Utils.Mathematical;

namespace MoveSimulation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private readonly Dictionary<Keys, bool> keyStates = new()
        {
            { Keys.W, false },
            { Keys.A, false },
            { Keys.S, false },
            { Keys.D, false },
            { Keys.Up, false },
            { Keys.Down, false },
            { Keys.Left, false },
            { Keys.Right, false }
        };

        private RelativePosition_8 GetDirection()
        {
            int x = 0;
            int y = 0;
            if (keyStates[Keys.W] || keyStates[Keys.Up]) y -= 1;
            if (keyStates[Keys.S] || keyStates[Keys.Down]) y += 1;
            if (keyStates[Keys.A] || keyStates[Keys.Left]) x -= 1;
            if (keyStates[Keys.D] || keyStates[Keys.Right]) x += 1;
            Point2D direction = new(x, y);
            return RelativePosition.ToRelativePosition_8(direction);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyStates.ContainsKey(e.KeyCode))
            {
                keyStates[e.KeyCode] = true;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (keyStates.ContainsKey(e.KeyCode))
            {
                keyStates[e.KeyCode] = false;
            }
        }
    }
}
