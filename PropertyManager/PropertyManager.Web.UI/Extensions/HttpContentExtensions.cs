using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PropertyManager.ResponseModels;

namespace PropertyManager.Web.UI.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> DeserializeAsAsync<T>(
            this HttpContent content)
        {
            var responseBody = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public static async Task<OkApiResponse> GetOkResponseAsync(
            this HttpContent content)
        {
            return await content.DeserializeAsAsync<OkApiResponse>();
        }

        public static async Task<CreatedApiResponse> GetCreatedResponseAsync(
            this HttpContent content)
        {
            return await content.DeserializeAsAsync<CreatedApiResponse>();
        }

        public static async Task<NoContentApiResponse> GetNoContentResponseAsync(
            this HttpContent content)
        {
            return await content.DeserializeAsAsync<NoContentApiResponse>();
        }

        public static async Task<BadRequestApiResponse> GetBadRequestResponseAsync(
            this HttpContent content)
        {
            return await content.DeserializeAsAsync<BadRequestApiResponse>();
        }

        public static async Task<UnauthorizedApiResponse> GetUnauthorizedResponseAsync(
            this HttpContent content)
        {
            return await content.DeserializeAsAsync<UnauthorizedApiResponse>();
        }

        public static async Task<ForbiddenApiResponse> GetForbiddenResponseAsync(
            this HttpContent content)
        {
            return await content.DeserializeAsAsync<ForbiddenApiResponse>();
        }

        public static async Task<NotFoundApiResponse> GetNotFoundResponseAsync(
            this HttpContent content)
        {
            return await content.DeserializeAsAsync<NotFoundApiResponse>();
        }

        public static async Task<InternalServerErrorApiResponse> GetInternalServerErrorResponseAsync(
            this HttpContent content)
        {
            return await content.DeserializeAsAsync<InternalServerErrorApiResponse>();
        }
    }
}
