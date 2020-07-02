using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Infrastructure.Persistence.DataSeeding;
using PropertyManager.Infrastructure.Security.Common;
using PropertyManager.Infrastructure.Security.Models;

namespace PropertyManager.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("Default")));

            services.AddIdentity<ApplicationUser, IdentityRole>(IdentityConfig.Options)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IApplicationDbContext>(
                provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<ISeedData, SeedDataService>();

            return services;
        }
    }
}
