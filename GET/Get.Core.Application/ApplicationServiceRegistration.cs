using GET.Core.Application.Contracts;
using GET.Core.Application.Features;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GET.Core.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IHasher, Hasher>();
            return services;
        }
    }
}
