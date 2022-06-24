using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.CQRS.MediaQueryCommand.Query;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : BaseController
    {
        public MediaController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("/Media/Attachment/{entity}/{fileName}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string fileName, string entity)
        {
            return await mediator.Send(new MediaGetQuery(fileName, entity));
        }
    }
}
