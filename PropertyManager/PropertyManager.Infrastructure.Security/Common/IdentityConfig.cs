using Microsoft.AspNetCore.Identity;

namespace PropertyManager.Infrastructure.Security.Common
{
    public class IdentityConfig
    {
        public static void Options(IdentityOptions options)
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;

            options.User.RequireUniqueEmail = true;
        }
    }
}
