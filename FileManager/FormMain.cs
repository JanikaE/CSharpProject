using FileManager.Forms;
using FileManager.UserInterface;
using System;
using System.Windows.Forms;

namespace FileManager
{
    public partial class FormMain : ScalingForm_1
    {
        public FormMain()
        {
            InitializeComponent();
            InitTag();

            Global.FormMain = this;
            Global.UIBackup = uiBackup;
            Global.UIDelete = uiDelete;

            SetRectangle();
        }
    }
}
