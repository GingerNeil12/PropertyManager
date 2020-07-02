using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Infrastructure.Services;

namespace PropertyManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            // Solution wide DI
            services.AddScoped<IDateTime, DateTimeService>();

            return services;
        }
    }
}
