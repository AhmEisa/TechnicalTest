using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GET.Infrastructure.Common
{
    public static class ExportToExcel
    {

        public static XLWorkbook RenderExcelFile<T>(ExportToExcelFileModel<T> exportToExcelFileModel, bool withFooterRowsCount = true)
        {
            try
            {
                int ColumnsCount = exportToExcelFileModel.DataHeader.Count;
                int FixedRows = 3;

                XLWorkbook wb = new XLWorkbook();


                IXLWorksheet ws = wb.Worksheets.Add(exportToExcelFileModel.WorksheetName);
                ws.RightToLeft = true;

                ws.Rows().Style.Fill.BackgroundColor = XLColor.White;
                ws.Rows().Height = 30;
                ws.Style.Font.FontName = "Segoe UI";
                int rowIndex = 1;

                #region Report Header logos
                // logo header
                ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Merge();
                ws.Row(rowIndex).Height = 70;
                rowIndex++;

                // logo image
                //var logoPath = $@"{exportToExcelFileModel.WebRootPath}\theme\media\logo.png";
                //var image = ws.AddPicture(logoPath)
                //    .MoveTo(ws.Cell("A1"), 10, 10)
                //    .Scale(0.8);
                //logoPath = $@"{exportToExcelFileModel.WebRootPath}\theme\media\logo2.png";
                //image = ws.AddPicture(logoPath)
                //    .MoveTo(ws.Cell("B1"), 90, 20)
                //    .Scale(0.8);
                #endregion

                #region Report Header
                var row = ws.Row(rowIndex);
                row.Height = 30;
                row.Style.Font.FontColor = XLColor.White;
                row.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Style.Fill.BackgroundColor = XLColor.FromHtml("0d94a2");

                //Style of Report Name
                ws.Cell(rowIndex, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                ws.Cell(rowIndex, 1).Style.Font.FontSize = 15;
                ws.Cell(rowIndex, 1).Style.Font.Bold = true;

                //Style of Report Date time
                ws.Cell(rowIndex, (ColumnsCount - 2)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.Cell(rowIndex, (ColumnsCount - 2)).Style.Font.FontSize = 13;

                // title header
                ws.Range(rowIndex, 1, rowIndex, (ColumnsCount - 3)).Merge();
                ws.Range(rowIndex, (ColumnsCount - 2), rowIndex, ColumnsCount).Merge();


                ws.Cell(rowIndex, 1).Value = exportToExcelFileModel.ReportName;
                ws.Cell(rowIndex, (ColumnsCount - 2)).Value = DateTime.Now;
                rowIndex++;
                #endregion

                #region Filters and Paramters
                // filters and paramters
                row = ws.Row(rowIndex);
                row.Height = 30;
                foreach (KeyValuePair<string, string> item in exportToExcelFileModel.Paramters)
                {
                    row = ws.Row(rowIndex);
                    row.Style.Font.FontSize = 12;
                    row.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                    ws.Range(rowIndex, 1, rowIndex, 2).Merge();
                    ws.Range(rowIndex, 3, rowIndex, ColumnsCount).Merge();

                    ws.Cell(rowIndex, 1).Value = item.Key;
                    ws.Cell(rowIndex, 1).Style.Font.FontColor = XLColor.FromArgb(21, 124, 106);
                    ws.Cell(rowIndex, 1).Style.Font.Bold = true;
                    ws.Cell(rowIndex, 3).SetValue(item.Value);
                    rowIndex++;
                    FixedRows++;
                }
                #endregion

                #region Data Header
                // data header
                row = ws.Row(rowIndex);
                row.Height = 30;
                row.Style.Font.Bold = true;
                row.Style.Font.FontColor = XLColor.White;
                row.Style.Font.FontSize = 12;
                row.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                row.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Style.Fill.BackgroundColor = XLColor.FromHtml("aeaeae");// XLColor.FromArgb(179, 164, 106);

                for (int i = 0; i < ColumnsCount; i++)
                {
                    var item = exportToExcelFileModel.DataHeader.ElementAt(i);
                    ws.Cell(rowIndex, (i + 1)).Value = item.Value;
                    ws.Cell(rowIndex, (i + 1)).Style.Alignment.WrapText = true;
                }
                rowIndex++;
                #endregion

                #region List of Data
                // data
                var ModelProperties = exportToExcelFileModel.DataHeader.Keys.ToList();
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (T item in exportToExcelFileModel.Result)
                {
                    int j = 1;
                    var dataRow = ws.Row(rowIndex);
                    dataRow.Height = 30;
                    dataRow.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    dataRow.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    dataRow.Style.NumberFormat.Format = "0";
                    if (rowIndex % 2 == 0)
                    {
                        ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Style.Fill.BackgroundColor = XLColor.White;
                    }
                    else
                    {
                        ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Style.Fill.BackgroundColor = XLColor.FromArgb(242, 242, 242);
                    }
                    foreach (var prop in exportToExcelFileModel.DataHeader.ToList())
                    {
                        if (properties.Find(prop.Key.ToString(), true) != null)
                        {
                            ws.Cell(rowIndex, j).Value = properties.Find(prop.Key.ToString(), true).GetValue(item) ?? DBNull.Value;
                            ws.Cell(rowIndex, j).Style.Alignment.WrapText = true;
                            j++;
                        }

                    }
                    rowIndex++;
                }

                ws.Columns().AdjustToContents();
                // freez rows
                ws.SheetView.FreezeRows(FixedRows);
                #endregion
                #region Totals
                foreach (var totalItem in exportToExcelFileModel.Totals)
                {
                    row = ws.Row(rowIndex);
                    row.Height = 30;
                    row.Style.Font.FontSize = 14;
                    row.Style.Font.Bold = true;
                    row.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    row.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Style.Fill.BackgroundColor = XLColor.FromArgb(242, 242, 242);
                    ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Merge();
                    ws.Cell(rowIndex, 1).Value = totalItem.Key + " : " + totalItem.Value;
                    rowIndex++;
                }
                #endregion

                #region Total Footer
                if (withFooterRowsCount)
                {
                    row = ws.Row(rowIndex);
                    row.Height = 30;
                    row.Style.Font.FontSize = 14;
                    row.Style.Font.Bold = true;
                    row.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    row.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Style.Fill.BackgroundColor = XLColor.FromArgb(242, 242, 242);
                    ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Merge();
                    ws.Cell(rowIndex, 1).Value = exportToExcelFileModel.TotalFooterName + " " + exportToExcelFileModel.Result.Count;
                    rowIndex++;
                }

                #endregion

                #region Footer
                // footer
                row = ws.Row(rowIndex);
                row.Height = 50;
                row.Style.Font.FontColor = XLColor.White;
                row.Style.Font.FontName = "Segoe UI";
                row.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                ws.Cell(rowIndex, 1).Style.Font.FontSize = 12;
                ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Style.Fill.BackgroundColor = XLColor.FromHtml("0d94a2");// XLColor.FromHtml("157c6a");

                ws.Range(rowIndex, 1, rowIndex, (ColumnsCount - 2)).Merge();
                ws.Range(rowIndex, (ColumnsCount - 1), rowIndex, ColumnsCount).Merge();

                ws.Cell(rowIndex, 1).Value = exportToExcelFileModel.CopyRightName;
                var Cell = ws.Cell(rowIndex, ColumnsCount);

                //int iColumnWidth = Convert.ToInt32(ws.Column(ColumnsCount).Width - 1) * 7 + 12; // To convert column width in pixel unit.
                //int xOffset = (iColumnWidth - image.Width);
                //int yOffset = 10;

                #endregion
                ws.Range(1, ColumnsCount + 1, rowIndex, ColumnsCount + 1).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                ws.Range(rowIndex, 1, rowIndex, ColumnsCount).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                return wb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
