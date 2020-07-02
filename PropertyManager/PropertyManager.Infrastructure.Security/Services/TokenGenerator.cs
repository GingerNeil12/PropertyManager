using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Infrastructure.Security.Interfaces;
using PropertyManager.Infrastructure.Security.Models;

namespace PropertyManager.Infrastructure.Security.Services
{
    internal class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDateTime _dateTime;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenGenerator> _logger;

        public TokenGenerator(
            UserManager<ApplicationUser> userManager,
            IDateTime dateTime,
            IConfiguration configuration,
            ILogger<TokenGenerator> logger)
        {
            _userManager = userManager;
            _dateTime = dateTime;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GenerateForUserAsync(string email)
        {
            _logger.LogInformation($"Generating token for: {email}");
            var user = await _userManager.FindByEmailAsync(email);
            await AddRefreshTokenToUserAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("RefreshToken", user.RefreshToken)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims,
                notBefore: _dateTime.Now,
                expires: _dateTime.Now.AddYears(1),
                issuedAt: _dateTime.Now);

            var token = new JwtSecurityToken(header, payload);

            _logger.LogInformation($"Token generated for: {email}");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task AddRefreshTokenToUserAsync(ApplicationUser user)
        {
            user.RefreshToken = GenerateRefeshToken();
            user.RefreshTokenExpiry = _dateTime.Now.AddYears(1);
            await _userManager.UpdateAsync(user);
        }

        private string GenerateRefeshToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var result = new byte[32];
                rng.GetBytes(result);
                return Convert.ToBase64String(result);
            }
        }
    }
}
