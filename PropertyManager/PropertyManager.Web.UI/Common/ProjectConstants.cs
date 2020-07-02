using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PropertyManager.Web.UI.Common
{
    public class ProjectConstants
    {
        public const string CLIENT_NAME = "PropertyManagerClient";
        public const string APPLICATION_JSON = "application/json";
        public const string AUTH_COOKIE_NAME = "PropertyManager.AuthCookie";
        public const string ACCESS_TOKEN_CLAIM = "access_token";
        public const string BEARER = JwtBearerDefaults.AuthenticationScheme;
    }
}
