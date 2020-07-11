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
        private readonly IUserService _userService;

        public IdentityService(
            IAuthenticationService authenticationService,
            ITokenGenerator tokenGenerator,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _tokenGenerator = tokenGenerator;
            _userService = userService;
        }

        public async Task<string> AuthenticateUserAsync(LoginViewModel model)
        {
            await _authenticationService.AuthenticateUserAsync(model);
            return await _tokenGenerator.GenerateForUserAsync(model.Email);
        }

        public Task<string> GetUsersNameByIdAsync(string id)
        {
            return _userService.GetUsersNameByIdAsync(id);
        }
    }
}
