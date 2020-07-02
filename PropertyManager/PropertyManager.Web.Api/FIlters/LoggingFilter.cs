using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PropertyManager.Web.Api.FIlters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        private ILoggerFactory _loggerFactory;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _loggerFactory = context.HttpContext.RequestServices.GetService<ILoggerFactory>();
            var method = context.HttpContext.Request.Method;
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
            Log(context.RouteData, method, remoteIp);
            base.OnActionExecuting(context);
        }

        private void Log(RouteData routeData, string method, string remoteIp)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];
            var logger = _loggerFactory.CreateLogger($"{controller}Controller");
            logger.LogInformation($"Action: {action}. Method: {method}. Remote IP: {remoteIp}");
        }
    }
}
