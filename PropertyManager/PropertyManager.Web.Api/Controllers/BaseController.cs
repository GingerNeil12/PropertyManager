﻿using Microsoft.AspNetCore.Mvc;
using PropertyManager.Web.Api.Extensions;

namespace PropertyManager.Web.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected BadRequestObjectResult GetBadRequestResult()
        {
            return new BadRequestObjectResult(ModelState.ConvertToDictionary());
        }
    }
}
