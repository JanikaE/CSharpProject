using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace Utils.Sample
{
    public class ExcelSample
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public static List<ExcelSample> ReadExcel()
        {
            List<ExcelSample> result = new();
            // 读取文件
            FileInfo fileInfo = new("xxx.xlsx");
            // 设置License
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // 获取Excel文件
            ExcelPackage package = new(fileInfo);
            // 获取Sheet
            ExcelWorksheet sheet = package.Workbook.Worksheets[0];
            // 读取每行，第一行是列标题
            // 行与列都从1开始计数
            for (int row = 2; row <= sheet.Dimension.Rows; row++)
            {
                result.Add(new ExcelSample()
                {
                    Id = sheet.Cells[row, 1].Value.ToString(),
                    Name = sheet.Cells[row, 2].Value.ToString()
                });
            }
            return result;
        }
    }
}