using Asp.Versioning;
using IDP.Api.Controllers.Base;
using IDP.Application.Command.Auth;
using IDP.Application.Command.User;
using IDP.Application.Query.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/Auth")]
    public class AuthController(IMediator _mediator) : IBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {
            return Ok(await _mediator.Send(user));
        }


        /// <summary>
        ///    ثبت نام وارسال كد به شماره موبايل
        /// </summary>
        /// <param name="authQuery"></param>
        /// <returns></returns>
        [HttpPost("RegisterAndSendOtp")]

        public async Task<IActionResult> RegisterAndSendOtp([FromBody] LoginMobileUser authQuery)
        {

            var res = await _mediator.Send(authQuery);

            return Ok(res);
        }

    }
}
