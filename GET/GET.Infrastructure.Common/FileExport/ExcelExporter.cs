using GET.Core.Application.Contracts.Infrastructure;
using GET.Core.Application.Features.ServiceRequests;
using System;
using System.Collections.Generic;
using System.IO;

namespace GET.Infrastructure.Common
{
    public class ExcelExporter : IExcelExporter
    {
        public byte[] ExportServiceRequestsToExcel(List<ServiceRequestExportDto> requestExportDtos)
        {

            ExportToExcelFileModel<ServiceRequestExportDto> exportToExcelFileModel = new ExportToExcelFileModel<ServiceRequestExportDto>();

            exportToExcelFileModel.Paramters = new Dictionary<string, string>();
            exportToExcelFileModel.DataHeader = new Dictionary<string, string>();
            exportToExcelFileModel.Result = new List<ServiceRequestExportDto>();

            //Report Sheet Name And Title
            exportToExcelFileModel.WorksheetName = "طلبات الخدمات";
            exportToExcelFileModel.ReportName = "قائمة طلبات الخدمات";

            // Paramters

            // data header
            exportToExcelFileModel.DataHeader.Add("RequestId", "رقم الطلب");
            exportToExcelFileModel.DataHeader.Add("UserName", "إسم المستخدم");
            exportToExcelFileModel.DataHeader.Add("ServiceName", "إسم الخدمة");
            exportToExcelFileModel.DataHeader.Add("RequestStatus", "حالة الطلب");

            // data
            exportToExcelFileModel.Result = requestExportDtos;

            // Footer
            exportToExcelFileModel.Totals.Add("إجمالى عدد الطلبات", requestExportDtos.Count.ToString());

            exportToExcelFileModel.CopyRightName = DateTime.Now.Year + " " + "جميع الحقوق محفوظة";


            var Wb = ExportToExcel.RenderExcelFile(exportToExcelFileModel, false);
            using (var ms = new MemoryStream())
            {
                Wb.SaveAs(ms);
                return ms.ToArray();
            }

        }
    }
}
