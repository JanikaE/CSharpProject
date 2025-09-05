using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupTool.Forms
{
    public partial class ScalingForm : Form
    {
        public ScalingForm()
        {
            InitializeComponent();
        }

        #region 控件大小随窗体大小等比例缩放

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

        #endregion

        #region 保存窗体大小位置

        public virtual void SetRectangle()
        {
            if (Config.Config.Instance.FormRectangle.TryGetValue(Name, out Rectangle rectangle))
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
            Config.Config.Instance.FormRectangle[Name] = rectangle;
            Config.Config.Instance.Save();
        }

        #endregion

        private void ScalingForm_Resize(object sender, EventArgs e)
        {
            float newx = Width / x;
            float newy = Height / y;
            SetControls(newx, newy, this);
        }

        private void ScalingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRectangle();
        }
    }
}
