using ClosedXML.Excel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UsefulTools.Decoders
{
    /// <summary>
    /// Excel 表格解码器 — 将字节数组解码为 Excel 并以 HTML 表格形式在 WebView2 中显示
    /// </summary>
    public partial class ExcelDecoder : IDisposable
    {
        private string _tempFilePath;

        /// <summary>
        /// 验证 Excel 文件头（.xlsx 为 ZIP 格式，.xls 为 OLE2 格式）
        /// </summary>
        public static bool IsValidExcel(byte[] data)
        {
            bool isZip = data.Length >= 4 &&
                         data[0] == 0x50 && data[1] == 0x4B &&
                         data[2] == 0x03 && data[3] == 0x04;
            bool isOle2 = data.Length >= 8 &&
                          data[0] == 0xD0 && data[1] == 0xCF &&
                          data[2] == 0x11 && data[3] == 0xE0;
            return isZip || isOle2;
        }

        /// <summary>
        /// 解码 Excel 字节数组并以 HTML 表格形式在 WebView2 中显示
        /// </summary>
        /// <returns>成功返回 null，失败返回错误信息</returns>
        public async Task<string> DecodeAndDisplayAsync(byte[] excelBytes, WebView2 excelViewer)
        {
            if (!IsValidExcel(excelBytes))
            {
                return "输入数据不是有效的 Excel 文件（文件头校验失败）。";
            }

            try
            {
                // 确定扩展名
                bool isZip = excelBytes[0] == 0x50;
                string ext = isZip ? ".xlsx" : ".xls";

                // 写入临时文件
                string tempPath = Path.Combine(
                    ApplicationData.Current.TemporaryFolder.Path,
                    $"excel_{Guid.NewGuid():N}{ext}");
                await File.WriteAllBytesAsync(tempPath, excelBytes);

                // 清理旧临时文件
                CleanupTempFile();
                _tempFilePath = tempPath;

                // 使用 ClosedXML 读取并转为 HTML
                using var workbook = new XLWorkbook(tempPath);
                var worksheet = workbook.Worksheet(1);
                string html = ConvertWorksheetToHtml(worksheet);

                // 在 WebView2 中显示
                await excelViewer.EnsureCoreWebView2Async();
                excelViewer.CoreWebView2.NavigateToString(html);
                excelViewer.Visibility = Visibility.Visible;
                return null;
            }
            catch (Exception ex)
            {
                return $"Excel 加载失败：{ex.Message}";
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

        #region HTML 转换

        /// <summary>
        /// 将 ClosedXML 工作表转换为 HTML 表格字符串（支持合并单元格）
        /// </summary>
        private static string ConvertWorksheetToHtml(IXLWorksheet worksheet)
        {
            var usedRange = worksheet.RangeUsed();
            if (usedRange == null)
            {
                return "<html><body><p>工作表为空。</p></body></html>";
            }

            int firstRow = usedRange.FirstRow().RowNumber();
            int lastRow = usedRange.LastRow().RowNumber();
            int firstCol = usedRange.FirstColumn().ColumnNumber();
            int lastCol = usedRange.LastColumn().ColumnNumber();

            // 构建合并单元格信息
            var mergeSpan = new Dictionary<(int row, int col), (int rowspan, int colspan)>();
            var skipCells = new HashSet<(int row, int col)>();

            foreach (var mergedRange in worksheet.MergedRanges)
            {
                int mrFirstRow = mergedRange.RangeAddress.FirstAddress.RowNumber;
                int mrFirstCol = mergedRange.RangeAddress.FirstAddress.ColumnNumber;
                int mrLastRow = mergedRange.RangeAddress.LastAddress.RowNumber;
                int mrLastCol = mergedRange.RangeAddress.LastAddress.ColumnNumber;

                int rowspan = mrLastRow - mrFirstRow + 1;
                int colspan = mrLastCol - mrFirstCol + 1;

                mergeSpan[(mrFirstRow, mrFirstCol)] = (rowspan, colspan);

                for (int r = mrFirstRow; r <= mrLastRow; r++)
                {
                    for (int c = mrFirstCol; c <= mrLastCol; c++)
                    {
                        if (r != mrFirstRow || c != mrFirstCol)
                            skipCells.Add((r, c));
                    }
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html><head><meta charset='utf-8'>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: 'Segoe UI', sans-serif; margin: 10px; }");
            sb.AppendLine("table { border-collapse: collapse; width: max-content; }");
            sb.AppendLine("th, td { border: 1px solid #ccc; padding: 4px 8px; min-width: 60px; white-space: nowrap; }");
            sb.AppendLine("th { background-color: #4472C4; color: white; font-weight: bold; text-align: center; }");
            sb.AppendLine("td { text-align: left; }");
            sb.AppendLine("tr:nth-child(even) { background-color: #f2f2f2; }");
            sb.AppendLine("</style></head><body>");
            sb.AppendLine($"<p>工作表: {worksheet.Name} | 行: {lastRow - firstRow + 1}, 列: {lastCol - firstCol + 1}</p>");
            sb.AppendLine("<table>");

            // 表头行
            sb.AppendLine("<tr>");
            for (int col = firstCol; col <= lastCol; col++)
            {
                if (skipCells.Contains((firstRow, col)))
                    continue;

                string header = worksheet.Cell(firstRow, col).GetString();
                if (mergeSpan.TryGetValue((firstRow, col), out var span))
                    sb.AppendLine($"<th colspan=\"{span.colspan}\" rowspan=\"{span.rowspan}\">{System.Net.WebUtility.HtmlEncode(header)}</th>");
                else
                    sb.AppendLine($"<th>{System.Net.WebUtility.HtmlEncode(header)}</th>");
            }
            sb.AppendLine("</tr>");

            // 数据行
            for (int row = firstRow + 1; row <= lastRow; row++)
            {
                sb.AppendLine("<tr>");
                for (int col = firstCol; col <= lastCol; col++)
                {
                    if (skipCells.Contains((row, col)))
                        continue;

                    var cell = worksheet.Cell(row, col);
                    string value = GetCellDisplayValue(cell);
                    if (mergeSpan.TryGetValue((row, col), out var span))
                        sb.AppendLine($"<td colspan=\"{span.colspan}\" rowspan=\"{span.rowspan}\">{System.Net.WebUtility.HtmlEncode(value)}</td>");
                    else
                        sb.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(value)}</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table></body></html>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取单元格的显示值（日期/数字格式化，文本直接返回）
        /// </summary>
        private static string GetCellDisplayValue(IXLCell cell)
        {
            if (cell.IsEmpty())
                return "";

            try
            {
                if (cell.DataType == XLDataType.DateTime)
                    return cell.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");

                if (cell.DataType == XLDataType.Number)
                    return cell.Value.ToString();
            }
            catch { /* 回退到字符串值 */ }

            return cell.GetString();
        }

        #endregion
    }
}
