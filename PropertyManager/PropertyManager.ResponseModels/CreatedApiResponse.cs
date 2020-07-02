using System.Net;

namespace PropertyManager.ResponseModels
{
    public class CreatedApiResponse : ApiResponse
    {
        public object Id { get; private set; }

        public CreatedApiResponse(object id, string title = null)
            : base((int)HttpStatusCode.Created, title)
        {
            Id = id;
        }
    }
}
