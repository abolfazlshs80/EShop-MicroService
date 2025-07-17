using Asp.Versioning;
using IDP.Api.Controllers.Base;
using IDP.Application.Command.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/Users")]
    public class UsersController(IMediator _mediator) : IBaseController
    {
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> Insert1(CreateUser user)
        {
            return Ok(await _mediator.Send(user));
        }
        [MapToApiVersion(2)]
        [HttpPost]
        public async Task<IActionResult> Insert2(CreateUser user)
        {
            return Ok(await _mediator.Send(user));
        }
    }
}
