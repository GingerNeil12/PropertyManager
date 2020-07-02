using System.Threading.Tasks;
using PropertyManager.ViewModels.Security;

namespace PropertyManager.Infrastructure.Security.Interfaces
{
    internal interface IAuthenticationService
    {
        Task AuthenticateUserAsync(LoginViewModel model);
    }
}
