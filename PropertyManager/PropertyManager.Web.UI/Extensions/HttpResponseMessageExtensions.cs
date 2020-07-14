using System.Net.Http;
using System.Threading.Tasks;
using PropertyManager.ResponseModels;

namespace PropertyManager.Web.UI.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<OkApiResponse> GetOkResponseAsync(
            this HttpResponseMessage message)
        {
            return await message.Content.GetOkResponseAsync();
        }

        public static async Task<CreatedApiResponse> GetCreatedResponseAsync(
            this HttpResponseMessage message)
        {
            return await message.Content.GetCreatedResponseAsync();
        }

        public static async Task<NoContentApiResponse> GetNoContentResponseAsync(
            this HttpResponseMessage message)
        {
            return await message.Content.GetNoContentResponseAsync();
        }

        public static async Task<BadRequestApiResponse> GetBadRequestResponseAsync(
            this HttpResponseMessage message)
        {
            return await message.Content.GetBadRequestResponseAsync();
        }

        public static async Task<UnauthorizedApiResponse> GetUnauthorizedResponseAsync(
            this HttpResponseMessage message)
        {
            return await message.Content.GetUnauthorizedResponseAsync();
        }

        public static async Task<ForbiddenApiResponse> GetForbiddenResponseAsync(
            this HttpResponseMessage message)
        {
            return await message.Content.GetForbiddenResponseAsync();
        }

        public static async Task<NotFoundApiResponse> GetNotFoundResponseAsync(
            this HttpResponseMessage message)
        {
            return await message.Content.GetNotFoundResponseAsync();
        }

        public static async Task<InternalServerErrorApiResponse> GetInternalServerErrorResponseAsync(
            this HttpResponseMessage message)
        {
            return await message.Content.GetInternalServerErrorResponseAsync();
        }
    }
}
