using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Utils.Mathematical;

namespace MoveSimulation
{
    public partial class MainForm : Form
    {
        private readonly MovePoint point;

        public MainForm()
        {
            InitializeComponent();
            point = new MovePoint(new Vector2D(0, 0), new Vector2D(0, 0));

            textBoxFriction.Text = Setting.Friction.ToString();
            textBoxFriction.TextChanged += (s, e) =>
            {
                if (float.TryParse(textBoxFriction.Text, out float friction))
                {
                    Setting.Friction = friction;
                }
            };
            textBoxAcceleration.Text = Setting.AccelerationModulu.ToString();
            textBoxAcceleration.TextChanged += (s, e) =>
            {
                if (float.TryParse(textBoxAcceleration.Text, out float acceleration))
                {
                    Setting.AccelerationModulu = acceleration;
                }
            };
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

        private Vector2D GetAcceleration()
        {
            int x = 0;
            int y = 0;
            if (keyStates[Keys.W] || keyStates[Keys.Up]) y -= 1;
            if (keyStates[Keys.S] || keyStates[Keys.Down]) y += 1;
            if (keyStates[Keys.A] || keyStates[Keys.Left]) x -= 1;
            if (keyStates[Keys.D] || keyStates[Keys.Right]) x += 1;
            Vector2D acceleration = new(x, y);
            return acceleration.ToNormal();
        }

        private RelativePosition_8 GetDirection()
        {
            return RelativePosition.ToRelativePosition_8(GetAcceleration());
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

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            point.Update(GetDirection());

            labelX.Text = $"{point.Position.X:F2}";
            labelY.Text = $"{point.Position.Y:F2}";

            Point2D center = new(pictureBoxPoint.Width / 2, pictureBoxPoint.Height / 2);
            Point2D velocity = (Point2D)point.Velocity + center;
            Point2D acceleration = (Point2D)(GetAcceleration() * 20) + center;

            Graphics g = pictureBoxPoint.CreateGraphics();
            g.Clear(Color.White);
            g.DrawLine(Pens.Red, (Point)center, (Point)velocity);
            g.DrawLine(Pens.Blue, (Point)center, (Point)acceleration);
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            point.Reset();
        }
    }
}
