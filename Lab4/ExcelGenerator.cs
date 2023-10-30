using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace Lab4
{
    public class ExcelGenerator<T> : IFileGenerator<T> where T : IEquatable<T>
    {
        public byte[] Generate(MyHashTable<T>  hashTable)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Hashset Distribution");

            var dist = hashTable.GetBucketEnumerator();
            int row = 1;
            while (dist.MoveNext())
            {
                sheet.Cells[row, 1].Value = dist.Current.Key;
                sheet.Cells[row, 2].Value = dist.Current.Count;
                row++;
            }

            var distributionChart = sheet.Drawings.AddChart("Distribution", eChartType.ColumnClustered);
            distributionChart.SetPosition(7, 0, 5, 0);
            distributionChart.SetSize(1600, 500);
            distributionChart.YAxis.MajorUnit = 1;
            distributionChart.YAxis.MaxValue = hashTable.MaxCount;

            var distributionData = distributionChart.Series.Add(sheet.Cells["B1:B3000"]);
            distributionData.Header = "Items";

            sheet.Protection.IsProtected = true;
            return package.GetAsByteArray();
        }
    }
}
