using FileManager.Forms;
using FileManager.UserInterface;
using System;
using System.Windows.Forms;

namespace FileManager
{
    public partial class FormMain : ScalingForm_1
    {
        private readonly PanelBackup panelBackup;
        private readonly PanelDelete panelDelete;

        public FormMain()
        {
            InitializeComponent();
            InitTag();

            panelBackup = new PanelBackup
            {
                Dock = DockStyle.Fill
            };
            tabPage1.Controls.Add(panelBackup);
            panelDelete = new PanelDelete
            {
                Dock = DockStyle.Fill
            };
            tabPage2.Controls.Add(panelDelete);

            Global.FormMain = this;
            Global.PanelBackup = panelBackup;
            Global.PanelDelete = panelDelete;

            SetRectangle();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            panelBackup.SetPanelPosition();
        }
    }
}
