using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PropertyManager.Web.UI.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> DeserializeAs<T>(
            this HttpContent content)
        {
            var responseBody = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }
}
