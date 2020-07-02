using System.Net;

namespace PropertyManager.ResponseModels
{
    public class NotFoundApiResponse : ApiResponse
    {
        public NotFoundApiResponse(string title = null)
            : base((int)HttpStatusCode.NotFound, title)
        {

        }
    }
}
