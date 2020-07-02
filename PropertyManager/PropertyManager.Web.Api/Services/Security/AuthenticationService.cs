using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Security;
using PropertyManager.Web.Api.Interfaces.Security;

namespace PropertyManager.Web.Api.Services.Security
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly IIdentityService _identityService;

        public AuthenticationService(
            IHttpContextAccessor httpContext,
            IIdentityService identityService)
            : base(httpContext)
        {
            _identityService = identityService;
        }

        public async Task<ResponseMessage> AuthenticateUserAsync(LoginViewModel model)
        {
            try
            {
                var result = await _identityService.AuthenticateUserAsync(model);
                return OkResponse(result);
            }
            catch (Exception ex)
            {
                return GetResponseMessageForException(ex);
            }
        }
    }
}
