using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PropertyManager.Infrastructure.Security.Exceptions;
using PropertyManager.ResponseModels;

namespace PropertyManager.Web.Api.Services
{
    public abstract class BaseService
    {
        protected IMediator Mediator { get; private set; }

        protected BaseService(IHttpContextAccessor httpContext)
        {
            Mediator = httpContext.HttpContext.RequestServices.GetService<IMediator>();
        }

        protected ResponseMessage GetResponseMessageForException(Exception ex)
        {
            switch (ex)
            {
                case IdentityValidationException identityValidation:
                    return BadRequestResponse(identityValidation.Errors, identityValidation.Message);
                case AccountLockedException accountLocked:
                    return UnauthorizedResponse(accountLocked.Message);
                default:
                    return InternalServerErrorResponse();
            }
        }

        protected ResponseMessage OkResponse(object result, string title = null)
        {
            var response = new OkApiResponse(result, title);
            return new ResponseMessage(response.Status, response);
        }

        protected ResponseMessage CreatedResponse(object id, string title = null)
        {
            var response = new CreatedApiResponse(id, title);
            return new ResponseMessage(response.Status, response);
        }

        protected ResponseMessage NoContentResponse(string title = null)
        {
            var response = new NoContentApiResponse(title);
            return new ResponseMessage(response.Status, response);
        }

        protected ResponseMessage BadRequestResponse(
            IDictionary<string, string[]> errors,
            string title = null)
        {
            var response = new BadRequestApiResponse(errors, title);
            return new ResponseMessage(response.Status, response);
        }

        protected ResponseMessage UnauthorizedResponse(string title = null)
        {
            var response = new UnauthorizedApiResponse(title);
            return new ResponseMessage(response.Status, response);
        }

        protected ResponseMessage ForbiddenResponse(string title = null)
        {
            var response = new ForbiddenApiResponse(title);
            return new ResponseMessage(response.Status, response);
        }

        protected ResponseMessage NotFoundResponse(string title = null)
        {
            var response = new NotFoundApiResponse(title);
            return new ResponseMessage(response.Status, response);
        }

        protected ResponseMessage InternalServerErrorResponse(string title = null)
        {
            var response = new InternalServerErrorApiResponse(title);
            return new ResponseMessage(response.Status, response);
        }
    }
}
