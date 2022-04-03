using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Shopbridge_base.Domain.Constants;
using Shopbridge_base.Domain.Models.DTO;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shopbridge_base.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public ILoggerService _loggerService { get; }

        public LogActionFilter(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            await OnActionExecuting(filterContext);
            ActionExecutedContext actionExecutedContext = await next();
            await OnActionExecuted(actionExecutedContext);
        }


        private new async Task OnActionExecuting(ActionExecutingContext filterContext)
        {
            var descriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
            var actionName = descriptor?.ActionName;
            var controllerName = descriptor?.ControllerName;
            await _loggerService.MessageLoggerAsync($"{controllerName!}/{actionName!}",
                string.Format(ShoppingBridgeConstants.EnterMethodMessage, controllerName!, actionName!),
                filterContext.ActionArguments
                );
        }

        private new async Task OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var descriptor = actionExecutedContext.ActionDescriptor as ControllerActionDescriptor;
            var actionName = descriptor?.ActionName;
            var controllerName = descriptor?.ControllerName;
            if (actionExecutedContext.Exception != null)
            {
                HandleException(controllerName!, actionName!, actionExecutedContext);
            }
            else
            {
                await _loggerService.MessageLoggerAsync($"{controllerName!}/{actionName!}",
                  string.Format(ShoppingBridgeConstants.ExitMethodName, controllerName!, actionName!),
                  actionExecutedContext.Result
                  );
            }
        }

        private void HandleException(string controllerName, string actionName, ActionExecutedContext actionExecutedContext)
        {
            ObjectResult objectResult = new ObjectResult(
                new ApiError { Message = ShoppingBridgeConstants.ExceptionMessage }
                )
            {
                StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError)
            };
            _loggerService.ErrorLoggerAsync(actionExecutedContext.Exception!,
                string.Format(ShoppingBridgeConstants.ExitMethodNameWithException, controllerName, actionName)
                );
            actionExecutedContext.Exception = null!;
            actionExecutedContext.Result = objectResult;
        }
    }
}
