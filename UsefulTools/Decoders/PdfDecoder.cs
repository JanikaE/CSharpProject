using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace UsefulTools.Decoders
{
    /// <summary>
    /// PDF 文档解码器 — 将字节数组解码为 PDF 并在 WebView2 中显示
    /// </summary>
    public partial class PdfDecoder : IDisposable
    {
        private string _tempFilePath;

        /// <summary>
        /// 验证 PDF 文件头
        /// </summary>
        public static bool IsValidPdf(byte[] data)
        {
            return data.Length >= 4 &&
                   data[0] == 0x25 && data[1] == 0x50 &&
                   data[2] == 0x44 && data[3] == 0x46;
        }

        /// <summary>
        /// 解码 PDF 字节数组并在 WebView2 中显示
        /// </summary>
        /// <returns>成功返回 null，失败返回错误信息</returns>
        public async Task<string> DecodeAndDisplayAsync(byte[] pdfBytes, WebView2 pdfViewer)
        {
            if (!IsValidPdf(pdfBytes))
            {
                return "输入数据不是有效的 PDF 文件（文件头校验失败）。";
            }

            try
            {
                // 写入临时文件
                string tempPath = Path.Combine(
                    ApplicationData.Current.TemporaryFolder.Path,
                    $"decoded_{Guid.NewGuid():N}.pdf");
                await File.WriteAllBytesAsync(tempPath, pdfBytes);

                // 清理旧临时文件
                CleanupTempFile();
                _tempFilePath = tempPath;

                // 加载 PDF（Source 属性会自动初始化 CoreWebView2）
                pdfViewer.Source = new Uri(tempPath);
                pdfViewer.Visibility = Visibility.Visible;
                return null;
            }
            catch (Exception ex)
            {
                return $"PDF 加载失败：{ex.Message}";
            }
        }

        /// <summary>
        /// 清理临时文件
        /// </summary>
        public void CleanupTempFile()
        {
            if (_tempFilePath != null && File.Exists(_tempFilePath))
            {
                try { File.Delete(_tempFilePath); }
                catch { /* 忽略清理失败 */ }
            }
            _tempFilePath = null;
        }

        public void Dispose()
        {
            CleanupTempFile();
            GC.SuppressFinalize(this);
        }
    }
}
