using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        public readonly IMediator mediator;

        public BaseController(IMediator mediator)
        {
            if (this.mediator == null)
                this.mediator = mediator;
        }
    }
}
