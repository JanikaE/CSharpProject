using FileManager.Configs;
using FileManager.Controls;
using System;
using System.Windows.Forms;

namespace FileManager.UserInterface
{
    public partial class UIDelete : UserControl
    {
        public UIDelete()
        {
            InitializeComponent();
            UpdatePanel();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var deletePattern = new DeletePattern();
            Config.Instance.DeletePatterns.Add(deletePattern);
            Config.Instance.Save();
            UserControlDeletePattern uc = new(deletePattern);
            panelDeletePatterns.Controls.Add(uc);
            SetPanelPosition();
        }

        public void UpdatePanel()
        {
            foreach (Control control in panelDeletePatterns.Controls)
            {
                control.Dispose();
            }
            panelDeletePatterns.Controls.Clear();

            foreach (DeletePattern deletePattern in Config.Instance.DeletePatterns)
            {
                UserControlDeletePattern uc = new(deletePattern);
                panelDeletePatterns.Controls.Add(uc);
            }
            SetPanelPosition();
        }

        public void SetPanelPosition()
        {
            int y = panelDeletePatterns.Top;
            foreach (Control control in panelDeletePatterns.Controls)
            {
                UserControlDeletePattern uc = control as UserControlDeletePattern;
                uc.Top = y;
                uc.Left = panelDeletePatterns.Left;
                uc.Width = panelDeletePatterns.Width;
                y += uc.Height;
            }
        }
    }
}
