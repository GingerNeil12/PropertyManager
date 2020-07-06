using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PropertyManager.Application;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Infrastructure;
using PropertyManager.Infrastructure.Persistence;
using PropertyManager.Infrastructure.Persistence.DataSeeding;
using PropertyManager.Infrastructure.Security;
using PropertyManager.Web.Api.Interfaces.Application;
using PropertyManager.Web.Api.Interfaces.Security;
using PropertyManager.Web.Api.Services.Application;
using PropertyManager.Web.Api.Services.Common;
using PropertyManager.Web.Api.Services.Security;

namespace PropertyManager.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure();
            services.AddSecurity(Configuration);
            services.AddPersistence(Configuration);

            services.AddHttpContextAccessor();

            // Solution wide DI
            services.AddScoped<ICurrentUser, CurrentUser>();

            // Project wide DI
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ILandlordService, LandlordService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ISeedData seedData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                seedData.SeedAsync().Wait();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
