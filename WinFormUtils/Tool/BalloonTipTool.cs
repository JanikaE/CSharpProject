using System.Drawing;
using System.Windows.Forms;

namespace WinFormUtils.Tool
{
    public static class BalloonTipHelper
    {
        /// <summary>
        /// 显示一个气球提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">内容</param>
        /// <param name="icon">自定义图标（可选）</param>
        /// <param name="tooltipIcon">系统图标类型（默认信息）</param>
        /// <param name="timeout">显示时间（毫秒，默认1000）</param>
        public static void ShowBalloonTip(
            string title,
            string text,
            Icon? icon = null,
            ToolTipIcon tooltipIcon = ToolTipIcon.Info,
            int timeout = 1000)
        {
            NotifyIcon notifyIcon = new()
            {
                Icon = icon ?? SystemIcons.Application,
                Visible = true
            };

            // 设置气球提示
            notifyIcon.ShowBalloonTip(timeout, title, text, tooltipIcon);

            // 气球提示关闭后释放资源
            notifyIcon.BalloonTipClosed += (s, e) => Cleanup(notifyIcon);

            // 安全计时器确保资源释放
            Timer safeTimer = new()
            {
                Interval = timeout + 500 // 稍长于显示时间
            };
            safeTimer.Tick += (s, e) =>
            {
                safeTimer.Stop();
                safeTimer.Dispose();
                Cleanup(notifyIcon);
            };
            safeTimer.Start();
        }

        private static void Cleanup(NotifyIcon notifyIcon)
        {
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }
        }
    }
}