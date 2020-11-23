using MapControllerWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MapControllerWebApi.Infrastructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal server error, please contact with administrator";
            ResponseMessage<string> response = new ResponseMessage<string>();
            response.Code = context.Response.StatusCode;
            response.MessageType = EnumMessageType.Error;
            response.Message = exception.Message;// message;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() }));
        }
    }
}
