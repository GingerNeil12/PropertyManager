using System.Collections.Generic;
using System.Net;

namespace PropertyManager.ResponseModels
{
    public class BadRequestApiResponse : ApiResponse
    {
        public IDictionary<string, string[]> Errors { get; private set; }

        public BadRequestApiResponse(
            IDictionary<string, string[]> errors,
            string title = null)
            : base((int)HttpStatusCode.BadRequest, title)
        {
            Errors = errors;
        }
    }
}
