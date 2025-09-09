using FileManager.Configs;
using System.Drawing;
using System.Windows.Forms;
using WinFormUtils.Forms;

namespace FileManager.Forms
{
    public partial class ScalingForm_1 : ScalingForm
    {
        public ScalingForm_1()
        {
            InitializeComponent();
        }

        #region 保存窗体大小位置

        public virtual void SetRectangle()
        {
            if (Config.Instance.FormRectangle.TryGetValue(Name, out Rectangle rectangle))
            {
                Left = rectangle.Left;
                Top = rectangle.Top;
                Width = rectangle.Width;
                Height = rectangle.Height;
            }
            else
            {
                SaveRectangle();
            }
        }

        public virtual void SaveRectangle()
        {
            Rectangle rectangle = new(Left, Top, Width, Height);
            Config.Instance.FormRectangle[Name] = rectangle;
            Config.Instance.Save();
        }

        #endregion

        private void ScalingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRectangle();
        }
    }
}
