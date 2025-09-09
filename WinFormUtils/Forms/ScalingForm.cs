using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormUtils.Forms
{
    /// <summary>
    /// 控件大小可随窗体大小等比例缩放
    /// 需要在构造函数中调用InitTag()方法
    /// </summary>
    public partial class ScalingForm : Form
    {
        public ScalingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化，需要在构造函数中调用
        /// </summary>
        protected void InitTag()
        {
            x = Width;
            y = Height;
            SetTag(this);
        }

        /// <summary>
        /// 定义当前窗体的宽度
        /// </summary>
        private float x;
        /// <summary>
        /// 定义当前窗体的高度
        /// </summary>
        private float y;

        private static void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    SetTag(con);
                }
            }
        }

        private static void SetControls(float newx, float newy, Control cons)
        {
            // 遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                // 获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string tag = con.Tag.ToString();
                    if (string.IsNullOrEmpty(tag))
                        continue;
                    string[] mytag = tag.Split([';']);
                    // 根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(Convert.ToSingle(mytag[0]) * newx);
                    con.Height = Convert.ToInt32(Convert.ToSingle(mytag[1]) * newy);
                    con.Left = Convert.ToInt32(Convert.ToSingle(mytag[2]) * newx);
                    con.Top = Convert.ToInt32(Convert.ToSingle(mytag[3]) * newy);
                    // 字体大小
                    float currentSize = Convert.ToSingle(mytag[4]) * newy;
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        SetControls(newx, newy, con);
                    }
                }
            }
        }

        protected void ScalingForm_Resize(object sender, EventArgs e)
        {
            float newx = Width / x;
            float newy = Height / y;
            SetControls(newx, newy, this);
        }
    }
}
