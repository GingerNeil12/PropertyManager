using System;
using System.Net;

namespace PropertyManager.ResponseModels
{
    public class ApiResponse
    {
        public int Status { get; private set; }
        public string Title { get; private set; }
        public DateTime RequestDate => DateTime.Now;

        public ApiResponse(int status, string title = null)
        {
            Status = status;
            Title = title ?? GetDefaultTitleForStatus(status);
        }

        private string GetDefaultTitleForStatus(int status)
        {
            switch (status)
            {
                case (int)HttpStatusCode.OK:
                    return "Requested completed successfully.";
                case (int)HttpStatusCode.Created:
                    return "Resource created successfully.";
                case (int)HttpStatusCode.NoContent:
                    return "No Content.";
                case (int)HttpStatusCode.BadRequest:
                    return "One or more validation errors occured.";
                case (int)HttpStatusCode.Unauthorized:
                    return "Access to the resource is unauthorized.";
                case (int)HttpStatusCode.Forbidden:
                    return "Access to the resource is forbidden.";
                case (int)HttpStatusCode.NotFound:
                    return "Resource not found.";
                case (int)HttpStatusCode.InternalServerError:
                    return "An unhandled error occured.";
                default:
                    return "Default Title.";
            }
        }
    }
}
