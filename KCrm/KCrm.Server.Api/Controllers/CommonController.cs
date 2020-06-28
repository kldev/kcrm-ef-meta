using System.Collections.Generic;
using System.Threading.Tasks;
using KCrm.Logic.Core;
using KCrm.Logic.Services.Common.Model;
using KCrm.Logic.Services.Common.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KCrm.Server.Api.Controllers {

    
    [Route ("api/common")]
    public class CommonController : AppAuthorizedControllerBase {

        public CommonController(IMediator mediator) : base (mediator) {
        }

        [SwaggerResponse (200, "The countries result", typeof (List<CountryDto>))]
        [SwaggerResponse (500, "The error", typeof (ErrorDto))]
        [Route ("countries")]
        [HttpPost]
        public async Task<IActionResult> GetCountries([FromBody] GetCountriesQuery request) {

            var result = await _mediator.Send (request);

            return Ok (result);
        }

    }
}
