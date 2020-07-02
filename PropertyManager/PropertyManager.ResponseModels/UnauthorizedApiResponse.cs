using System.Net;

namespace PropertyManager.ResponseModels
{
    public class UnauthorizedApiResponse : ApiResponse
    {
        public UnauthorizedApiResponse(string title = null)
            : base((int)HttpStatusCode.Unauthorized, title)
        {

        }
    }
}
