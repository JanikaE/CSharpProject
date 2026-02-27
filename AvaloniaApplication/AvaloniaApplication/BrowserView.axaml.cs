using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using System;
using System.Threading.Tasks;
using Xilium.CefGlue;
using Xilium.CefGlue.Avalonia;
using Xilium.CefGlue.Common.Handlers;

namespace AvaloniaApplication
{
    public partial class BrowserView : UserControl
    {
        private readonly AvaloniaCefBrowser browser;

        public BrowserView()
        {
            InitializeComponent();

            var browserWrapper = this.FindControl<Decorator>("browserWrapper");

            browser = new AvaloniaCefBrowser
            {
                Address = "https://www.baidu.com"
            };
            browser.LoadStart += OnBrowserLoadStart;
            browser.TitleChanged += OnBrowserTitleChanged;
            browser.LifeSpanHandler = new BrowserLifeSpanHandler();
            browserWrapper.Child = browser;
        }

        static Task<object> AsyncCallNativeMethod(Func<object> nativeMethod)
        {
            return Task.Run(() =>
            {
                var result = nativeMethod.Invoke();
                if (result is Task task)
                {
                    if (task.GetType().IsGenericType)
                    {
                        return ((dynamic)task).Result;
                    }

                    return task;
                }

                return result;
            });
        }

        public event Action<string> TitleChanged;

        private void OnBrowserTitleChanged(object sender, string title)
        {
            TitleChanged?.Invoke(title);
        }

        private void OnBrowserLoadStart(object sender, Xilium.CefGlue.Common.Events.LoadStartEventArgs e)
        {
            if (e.Frame.Browser.IsPopup || !e.Frame.IsMain)
            {
                return;
            }

            Dispatcher.UIThread.Post(() =>
            {
                var addressTextBox = this.FindControl<TextBox>("addressTextBox");
                addressTextBox?.Text = e.Frame.Url;
            });
        }

        private void OnAddressTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                browser.Address = ((TextBox)sender).Text;
            }
        }

        #region 浏览器操作

        /// <summary>
        /// 打开浏览器调试工具
        /// </summary>
        public void OpenDevTools()
        {
            browser.ShowDeveloperTools();
        }

        /// <summary>
        /// 刷新浏览器
        /// </summary>
        public void Refresh()
        {
            browser.Reload();
        }

        /// <summary>
        /// 释放浏览器资源
        /// </summary>
        public void Dispose()
        {
            browser.Dispose();
        }

        #endregion

        private class BrowserLifeSpanHandler : LifeSpanHandler
        {
            protected override bool OnBeforePopup(
                CefBrowser browser,
                CefFrame frame,
                string targetUrl,
                string targetFrameName,
                CefWindowOpenDisposition targetDisposition,
                bool userGesture,
                CefPopupFeatures popupFeatures,
                CefWindowInfo windowInfo,
                ref CefClient client,
                CefBrowserSettings settings,
                ref CefDictionaryValue extraInfo,
                ref bool noJavascriptAccess)
            {
                var bounds = windowInfo.Bounds;
                Dispatcher.UIThread.Post(() =>
                {
                    var popupBrowser = new AvaloniaCefBrowser
                    {
                        Address = targetUrl
                    };
                    var window = new Window
                    {
                        Content = popupBrowser,
                        Position = new PixelPoint(bounds.X, bounds.Y),
                        Height = bounds.Height,
                        Width = bounds.Width,
                        Title = targetUrl
                    };
                    window.Show();
                });
                return true;
            }
        }
    }
}
