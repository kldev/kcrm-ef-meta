using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using KCrm.Core.Exceptions;
using KCrm.Core.Util;
using Microsoft.AspNetCore.Http;

namespace KCrm.Server.Api.Infrastructure {
    public class ErrorHandlingMiddleware {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next) {
            this.next = next;
        }
        public async Task Invoke(HttpContext context /* other dependencies */) {
            try {
                await next (context);
            }
            catch (Exception ex) {
                await HandleExceptionAsync (context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex) {
            var code = HttpStatusCode.InternalServerError;

#if DEBUG
            Console.WriteLine ("STACK:" + ex.StackTrace);
#endif
            var result = JsonUtil.SerializeObject (new { Error = ex.Message });
            if (ex is ValidationException ve) {
                result = JsonUtil.SerializeObject (new { Error = ex.Message, ValidationErrors = ve.Errors.ToList ( ) });
            }

            if (ex is AppLogicRuleException appEx) {
                result = JsonUtil.SerializeObject (new { Error = appEx.Code, Message = ex.Message });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync (result);
        }
    }
}
