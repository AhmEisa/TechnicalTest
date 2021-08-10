using GET.Core.Application.Contracts.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace GET.Infrastructure.Common
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPdfExporter, PdfExporter>();
            services.AddScoped<IExcelExporter, ExcelExporter>();

            return services;
        }

    }
}
