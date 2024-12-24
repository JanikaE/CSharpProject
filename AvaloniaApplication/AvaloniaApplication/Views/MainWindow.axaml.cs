using Avalonia.Controls;
using CefNet.Avalonia;

namespace AvaloniaApplication.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Initialized += MainWindow_Initialized;        
    }

    private void MainWindow_Initialized(object? sender, System.EventArgs e)
    {
        WebView webview = new() { Focusable = true };
        Content = webview;

        webview.BrowserCreated += (s, e) => webview.Navigate("https://www.baidu.com");
    }
}