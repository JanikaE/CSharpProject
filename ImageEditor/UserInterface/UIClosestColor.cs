using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormUtils.Tool;

namespace ImageEditor.UserInterface
{
    public partial class UIClosestColor : UserControl
    {
        public MainForm MainForm { get; set; }

        public UIClosestColor()
        {
            InitializeComponent();
            InitComboBox();
            InitPanel();
            RefreshPanel(0);
        }

        #region ComboBox

        private void InitComboBox()
        {
            if (Config.Instance.ColorLists == null || Config.Instance.ColorLists.Count == 0)
            {
                Config.Instance.ColorLists.Add(new ColorList()
                {
                    Name = "Default",
                    Colors = [Color.Black, Color.White]
                });
            }
            else
            {
                foreach (var colorList in Config.Instance.ColorLists)
                {
                    colorList.Colors ??= [];
                    if (colorList.Colors.Count == 0)
                    {
                        colorList.Colors.Add(Color.Black);
                        colorList.Colors.Add(Color.White);
                    }
                }
            }
            Config.Instance.Save();
            comboBox.DataSource = Config.Instance.ColorLists;
            comboBox.DisplayMember = "Name";
            comboBox.SelectedIndex = 0;
        }

        private void RefreshComboBox()
        {
            comboBox.DataSource = null;
            comboBox.DataSource = Config.Instance.ColorLists;
            comboBox.DisplayMember = "Name";
        }

        #endregion

        #region Panel

        private void InitPanel()
        {
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("AddOne", null, (sender, e) =>
            {
                int index = comboBox.SelectedIndex;
                Config.Instance.ColorLists[index].Colors.Add(Color.Green);
                RefreshPanel(index);
            });
            contextMenuStrip.Items.Add("AddList", null, (sender, e) =>
            {
                Config.Instance.ColorLists.Add(new ColorList()
                {
                    Name = "New List",
                    Colors = [Color.Black, Color.White]
                });
                RefreshComboBox();
                RefreshPanel(Config.Instance.ColorLists.Count - 1);
            });
            contextMenuStrip.Items.Add("DeleteList", null, (sender, e) =>
            {
                int index = comboBox.SelectedIndex;
                if (Config.Instance.ColorLists.Count > 1)
                {
                    Config.Instance.ColorLists.RemoveAt(index);
                    RefreshComboBox();
                    RefreshPanel(0);
                }
                else
                {
                    MessageBox.Show("At least one color list must be kept.");
                }
            });
            contextMenuStrip.Items.Add("Rename", null, (sender, e) =>
            {
                int index = comboBox.SelectedIndex;
                string name = InputTool.ShowDialog(Config.Instance.ColorLists[index].Name);
                if (!string.IsNullOrEmpty(name))
                {
                    Config.Instance.ColorLists[index].Name = name;
                    RefreshComboBox();
                    RefreshPanel(index);
                }
            });
            panel.ContextMenuStrip = contextMenuStrip;
        }

        private void RefreshPanel(int selectedIndex)
        {
            Config.Instance.Save();
            comboBox.SelectedIndex = selectedIndex;

            foreach (Control control in panel.Controls)
            {
                control.Dispose();
            }
            panel.Controls.Clear();
            var colors = Config.Instance.ColorLists[selectedIndex].Colors;
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Remove", null, ColorRemove);
            foreach (var color in colors)
            {
                PictureBox pictureBox = new()
                {
                    BackColor = color,
                    Size = new Size(30, 30),
                    Tag = color
                };
                pictureBox.Click += ColorClick;
                pictureBox.ContextMenuStrip = contextMenuStrip;
                panel.Controls.Add(pictureBox);
            }
            int x = panel.Left;
            int y = panel.Top;
            foreach (Control control in panel.Controls)
            {
                PictureBox p = control as PictureBox;
                p.Left = x;
                p.Top = y;
                x += p.Width;
                if (x > panel.Left + panel.Width)
                {
                    x = panel.Left;
                    y += p.Height;
                }
            }
        }

        private void ColorClick(object sender, EventArgs e)
        {
            var p = (PictureBox)sender;
            var c = (Color)p.Tag;
            ColorDialog colorDialog = new()
            {
                Color = c
            };
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                p.BackColor = colorDialog.Color;
                p.Tag = colorDialog.Color;
                int index = comboBox.SelectedIndex;
                int colorIndex = panel.Controls.IndexOf(p);
                Config.Instance.ColorLists[index].Colors[colorIndex] = colorDialog.Color;
                RefreshPanel(index);
            }
        }

        private void ColorRemove(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var contextMenuStrip = (ContextMenuStrip)menuItem.GetCurrentParent();
            var pictureBox = (PictureBox)contextMenuStrip.SourceControl;
            int index = comboBox.SelectedIndex;
            int colorIndex = panel.Controls.IndexOf(pictureBox);
            Config.Instance.ColorLists[index].Colors.RemoveAt(colorIndex);
            RefreshPanel(index);
        }

        #endregion

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            int index = comboBox.SelectedIndex;
            var colors = Config.Instance.ColorLists[index].Colors.ToArray();
            Image newImage = Editor.ClosestColor(MainForm.ImportImage, colors);
            MainForm.ExportImage = newImage;
        }
    }
}
