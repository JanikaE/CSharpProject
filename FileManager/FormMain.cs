using FileManager.Configs;
using FileManager.Controls;
using FileManager.Forms;
using System;
using System.Windows.Forms;

namespace FileManager
{
    public partial class FormMain : ScalingForm_1
    {
        private readonly PanelBackup panelBackup;

        public FormMain()
        {
            InitializeComponent();
            InitTag();

            panelBackup = new PanelBackup
            {
                Dock = DockStyle.Fill
            };
            tabPage1.Controls.Add(panelBackup);

            Global.FormMain = this;
            Global.PanelBackup = this.panelBackup;

            SetRectangle();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            panelBackup.SetPanelPosition();
        }
    }
}
