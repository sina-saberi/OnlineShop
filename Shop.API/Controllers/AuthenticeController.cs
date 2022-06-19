using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.CQRS.UserCommandQuery.Command;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticeController : BaseController
    {
        public AuthenticeController(IMediator mediator) : base(mediator)
        {

        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand input)
        {
            var result = await mediator.Send(input);
            return Ok(result);
        }

        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommand input)
        {
            var result = await mediator.Send(input);
            return Ok(result);
        }

        [Route("GenerateToken")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateNewToken input)
        {
            var result = await mediator.Send(input);
            return Ok(result);
        }
    }
}
