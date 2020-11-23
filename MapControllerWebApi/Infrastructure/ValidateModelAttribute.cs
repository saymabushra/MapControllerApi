using MapControllerWebApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapControllerWebApi.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!context.ModelState.IsValid)
            {
                var message = string.Join(" | ", context.ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage));
                if (string.IsNullOrEmpty(message)) message = ApiConstants.API_MODEL_ERROR;
                //context.Result= context.HttpContext.Response.Create(
                //     HttpStatusCode.BadRequest, message);
            }
            if (context.ActionArguments.Any(kv => kv.Value == null))
            {
                //actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Required Parameters Should Not Empty Or Null");
            }
        }
    }
}
