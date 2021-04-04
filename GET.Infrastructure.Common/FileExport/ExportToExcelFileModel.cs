using System.Collections.Generic;

namespace GET.Infrastructure.Common
{
    public class ExportToExcelFileModel<T>
    {
        public string WorksheetName { get; set; }
        public string ReportName { get; set; }
        public string TotalFooterName { get; set; }
        public string CopyRightName { get; set; }
        public string WebRootPath { get; set; }
        public Dictionary<string, string> DataHeader { get; set; }
        public List<T> Result { get; set; }
        public Dictionary<string, string> Paramters { get; set; }
        public Dictionary<string, string> Totals { get; set; } = new Dictionary<string, string>();
    }
}
