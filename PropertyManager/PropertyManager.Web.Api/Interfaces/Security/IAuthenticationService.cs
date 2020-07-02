using System.Threading.Tasks;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Security;

namespace PropertyManager.Web.Api.Interfaces.Security
{
    public interface IAuthenticationService
    {
        Task<ResponseMessage> AuthenticateUserAsync(LoginViewModel model);
    }
}
