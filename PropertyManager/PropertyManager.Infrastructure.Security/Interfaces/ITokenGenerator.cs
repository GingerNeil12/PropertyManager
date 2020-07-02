using System.Threading.Tasks;

namespace PropertyManager.Infrastructure.Security.Interfaces
{
    internal interface ITokenGenerator
    {
        Task<string> GenerateForUserAsync(string email);
    }
}
