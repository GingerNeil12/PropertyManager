using System.Net;

namespace PropertyManager.ResponseModels
{
    public class InternalServerErrorApiResponse : ApiResponse
    {
        public InternalServerErrorApiResponse(string title = null)
            : base((int)HttpStatusCode.InternalServerError, title)
        {

        }
    }
}
