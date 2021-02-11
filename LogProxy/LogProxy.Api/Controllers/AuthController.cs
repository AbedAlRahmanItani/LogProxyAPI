using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LogProxy.Api.Controllers.Base;
using LogProxy.Application.CQRS.Auth.Models;
using LogProxy.Application.CQRS.Auth.Queries;

namespace LogProxy.Api.Controllers
{
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult<AuthenticationToken>> Authenticate([FromBody] GetAuthenticationTokenQuery query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await Mediator.Send(query));
        }
    }
}