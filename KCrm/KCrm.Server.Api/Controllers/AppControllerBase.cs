using System;
using System.Net;
using KCrm.Logic.Core;
using Microsoft.AspNetCore.Mvc;

namespace KCrm.Server.Api.Controllers {
    public abstract class AppControllerBase : ControllerBase {
        protected IActionResult ApiResult<T>(ResponseBase<T> result, HttpStatusCode? errorStatusCode = null) {
            return result.Data != null ? Ok (result.Data)
                : errorStatusCode != null ? ApiError (result.Error, errorStatusCode.Value)
                : BadRequest (result.Error);
        }

        private IActionResult ApiError(ErrorDto resultError, HttpStatusCode errorStatusCode) {
            var status = new ObjectResult (resultError) { StatusCode = (int)errorStatusCode };
            return status;
        }

        protected IActionResult ApiCreatedResult(Guid id) {
            return Ok (new EntityCreatedDto (id));
        }
    }
}
