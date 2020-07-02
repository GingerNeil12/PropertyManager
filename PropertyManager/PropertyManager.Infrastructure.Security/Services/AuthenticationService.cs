using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PropertyManager.Infrastructure.Security.Exceptions;
using PropertyManager.Infrastructure.Security.Interfaces;
using PropertyManager.Infrastructure.Security.Models;
using PropertyManager.ViewModels.Security;

namespace PropertyManager.Infrastructure.Security.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task AuthenticateUserAsync(LoginViewModel model)
        {
            _logger.LogInformation($"Authenticating user: {model.Email}");

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogWarning($"Account not found: {model.Email}");
                throw new IdentityValidationException(GetEmailPasswordError());
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                _logger.LogWarning($"Account locked: {model.Email}");
                throw new AccountLockedException();
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                _logger.LogWarning($"Incorrect password for: {model.Email}");
                throw new IdentityValidationException(GetEmailPasswordError());
            }

            _logger.LogInformation($"User authenticated: {model.Email}");
        }

        private IEnumerable<IdentityError> GetEmailPasswordError()
        {
            var errorMessage = "Email or Password incorrect.";
            var result = new List<IdentityError>()
            {
                new IdentityError()
                {
                    Code = nameof(LoginViewModel.Email),
                    Description = errorMessage
                },
                new IdentityError()
                {
                    Code = nameof(LoginViewModel.Password),
                    Description = errorMessage
                },
            };
            return result;
        }
    }
}
