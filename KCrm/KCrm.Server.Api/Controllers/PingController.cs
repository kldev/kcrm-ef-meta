using System;
using Microsoft.AspNetCore.Mvc;

namespace KCrm.Server.Api.Controllers {
    public class PingController : ControllerBase {

        [HttpGet]
        [Route ("api/ping")]
        public IActionResult Pong() => Ok ("Pong: " + Environment.MachineName);
    }
}
