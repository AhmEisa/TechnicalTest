namespace GET.Core.Application.Features.ServiceRequests
{
    public class ServiceRequestExportFileVm
    {
        public string ExportFileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
