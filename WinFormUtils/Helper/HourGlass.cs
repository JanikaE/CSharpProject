using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinFormUtils.Helper
{
    /// <summary>
    /// 等待操作，将鼠标指针变为沙漏，直到操作完成。
    /// </summary>
    public partial class HourGlass : IDisposable
    {
        public static HourGlass New()
        {
            return new HourGlass();
        }

        private HourGlass()
        {
            Enabled = true;
        }

        public void Dispose()
        {
            Enabled = false;
            GC.SuppressFinalize(this);
        }

        public static bool Enabled
        {
            get { return Application.UseWaitCursor; }
            set
            {
                if (value == Application.UseWaitCursor) return;
                Application.UseWaitCursor = value;
                Form f = Form.ActiveForm;
                if (f != null && !f.Disposing && !f.IsDisposed) // Send WM_SETCURSOR
                {
                    SendMessage(f.Handle, 0x20, f.Handle, 1);
                }
            }
        }

        [LibraryImport("user32.dll", EntryPoint = "SendMessageW")]
        private static partial nint SendMessage(nint hWnd, int msg, nint wp, nint lp);
    }
}
