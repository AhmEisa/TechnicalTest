using FastMember;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using GET.Core.Application.Contracts.Infrastructure;
using GET.Core.Application.Features.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace GET.Infrastructure.Common
{
    public class PdfExporter : IPdfExporter
    {
        private readonly string ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports\\");

        public byte[] ExportServiceRequestsToPdf(List<ServiceRequestExportDto> requestExportDtos)
        {

            string ReportName = "ServiceRequestReport.frx";
            var executionRequests = new DataTable("ServiceRequests");
            using (var reader = ObjectReader.Create(requestExportDtos)) { executionRequests.Load(reader); }

            var dataSet = new DataSet();
            dataSet.Tables.Add(executionRequests);
            var ReportModel = PrepareWebReport(ReportName, dataSet);

            ReportModel.Report.Prepare();
            using (MemoryStream ms = new MemoryStream())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(ReportModel.Report, ms);
                ms.Flush();
                return ms.ToArray();
            }
        }

        private WebReport PrepareWebReport(string ReportName, DataSet dataSet)
        {
            string FullReportPath = string.Concat(ReportPath, ReportName);
            WebReport ReportModel = new WebReport();
            ReportModel.Report.Load(FullReportPath);
            ReportModel.Report.RegisterData(dataSet, "L");
            return ReportModel;
        }
    }
}
