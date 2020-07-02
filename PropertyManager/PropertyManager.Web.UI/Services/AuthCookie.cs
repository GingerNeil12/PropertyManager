using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using PropertyManager.Web.UI.Common;
using PropertyManager.Web.UI.Interfaces;

namespace PropertyManager.Web.UI.Services
{
    public class AuthCookie : IAuthCookie
    {
        private readonly IHttpContextAccessor _httpContext;

        public AuthCookie(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public async Task Create(string token)
        {
            var claims = new List<Claim>(GetClaimsFromToken(token))
            {
                new Claim(ProjectConstants.ACCESS_TOKEN_CLAIM, token)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Email,
                ClaimTypes.Role);

            var authProperties = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.Now,
                IsPersistent = false
            };

            await _httpContext.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public Task Destroy()
        {
            return _httpContext.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IEnumerable<Claim> GetClaimsFromToken(string tokenString)
        {
            var token = new JwtSecurityToken(tokenString);
            return token.Claims;
        }
    }
}
