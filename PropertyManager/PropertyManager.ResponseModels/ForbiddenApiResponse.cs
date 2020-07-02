using System.Net;

namespace PropertyManager.ResponseModels
{
    public class ForbiddenApiResponse : ApiResponse
    {
        public ForbiddenApiResponse(string title = null)
            : base((int)HttpStatusCode.Forbidden, title)
        {

        }
    }
}
