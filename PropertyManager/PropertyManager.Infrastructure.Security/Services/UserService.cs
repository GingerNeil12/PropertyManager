using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PropertyManager.Application.Common.Helpers;
using PropertyManager.Infrastructure.Security.Interfaces;
using PropertyManager.Infrastructure.Security.Models;

namespace PropertyManager.Infrastructure.Security.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUsersNameByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return NameHelper.FormatFullName(user.FirstName, user.LastName);
        }
    }
}
