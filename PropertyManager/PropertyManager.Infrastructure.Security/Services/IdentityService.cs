using System.Threading.Tasks;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Infrastructure.Security.Interfaces;
using PropertyManager.ViewModels.Security;

namespace PropertyManager.Infrastructure.Security.Services
{
    internal class IdentityService : IIdentityService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenGenerator _tokenGenerator;

        public IdentityService(
            IAuthenticationService authenticationService,
            ITokenGenerator tokenGenerator)
        {
            _authenticationService = authenticationService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> AuthenticateUserAsync(LoginViewModel model)
        {
            await _authenticationService.AuthenticateUserAsync(model);
            return await _tokenGenerator.GenerateForUserAsync(model.Email);
        }
    }
}
