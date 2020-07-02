using System.Threading.Tasks;

namespace PropertyManager.Web.UI.Interfaces
{
    public interface IAuthCookie
    {
        Task Create(string token);
        Task Destroy();
    }
}
