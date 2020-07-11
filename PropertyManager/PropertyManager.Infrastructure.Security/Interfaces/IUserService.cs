using System.Threading.Tasks;

namespace PropertyManager.Infrastructure.Security.Interfaces
{
    internal interface IUserService
    {
        Task<string> GetUsersNameByIdAsync(string id);
    }
}
