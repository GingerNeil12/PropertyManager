using System.Net;

namespace PropertyManager.ResponseModels
{
    public class OkApiResponse : ApiResponse
    {
        public object Result { get; private set; }

        public OkApiResponse(object result, string title = null)
            : base((int)HttpStatusCode.OK, title)
        {
            Result = result;
        }
    }
}
