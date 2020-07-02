using System.Net;

namespace PropertyManager.ResponseModels
{
    public class NoContentApiResponse : ApiResponse
    {
        public NoContentApiResponse(string title = null)
            : base((int)HttpStatusCode.NoContent, title)
        {

        }
    }
}
