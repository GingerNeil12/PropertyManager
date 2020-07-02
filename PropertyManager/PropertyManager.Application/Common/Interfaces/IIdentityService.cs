using System.Threading.Tasks;
using PropertyManager.ViewModels.Security;

namespace PropertyManager.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> AuthenticateUserAsync(LoginViewModel model);
    }
}
