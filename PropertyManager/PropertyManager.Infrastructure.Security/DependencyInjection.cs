using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Domain.Convertors;
using PropertyManager.Infrastructure.Security.Interfaces;
using PropertyManager.Infrastructure.Security.Services;
using PropertyManager.ResponseModels;

namespace PropertyManager.Infrastructure.Security
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSecurity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if(context.Exception is SecurityTokenExpiredException)
                            {
                                context.Response.Headers.Add("RefreshToken", "true");
                            }
                            var response = new UnauthorizedApiResponse("Token has expired.");
                            var body = ObjectToJson.ToByteArray(response);
                            context.Response.StatusCode = response.Status;
                            context.Response.ContentType = "application/json";
                            context.Response.Body.Write(body, 0, body.Length);
                            return Task.CompletedTask;
                        },

                        OnForbidden = context =>
                        {
                            var response = new ForbiddenApiResponse();
                            var body = ObjectToJson.ToByteArray(response);
                            context.Response.StatusCode = response.Status;
                            context.Response.ContentType = "application/json";
                            context.Response.Body.Write(body, 0, body.Length);
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            // Solution wide DI
            services.AddScoped<IIdentityService, IdentityService>();

            // Project wide DI
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
