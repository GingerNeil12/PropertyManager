using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PropertyManager.Infrastructure.Security.Common
{
    public class AuthSchemes
    {
        public const string BEARER = JwtBearerDefaults.AuthenticationScheme;
    }
}
