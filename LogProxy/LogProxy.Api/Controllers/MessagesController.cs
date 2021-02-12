using System.Collections.Generic;
using System.Threading.Tasks;
using LogProxy.Api.Controllers.Base;
using LogProxy.Application.CQRS.Messages.Commands;
using LogProxy.Application.CQRS.Messages.Models;
using LogProxy.Application.CQRS.Messages.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogProxy.Api.Controllers
{
    [Authorize]
    public class MessagesController : BaseController
    {
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MessagesViewModel>>> Get(int maxRecords, string view)
        {
            return Ok(await Mediator.Send(new GetMessagesQuery
            {
                MaxRecords = maxRecords,
                View = view
            }));
        }

        [HttpPost("")]
        public async Task<ActionResult<IEnumerable<MessagesViewModel>>> Post([FromBody] CreateMessagesCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await Mediator.Send(command));
        }
    }
}
