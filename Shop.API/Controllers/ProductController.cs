using Microsoft.AspNetCore.Mvc;
using MediatR;
using Shop.Application.CQRS.ProductQueryCommand.Query;
using Shop.Application.CQRS.ProductQueryCommand.Command;
using Microsoft.AspNetCore.Authorization;
using Shop.API.CustomAttributes;

namespace Shop.API.Controllers;

public class ProductController : BaseController
{
    public ProductController(IMediator mediator) : base(mediator)
    {

    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromQuery] ProductGetByIdQuery input)
    {
        var result = await mediator.Send(input);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new ProductGetAllQuery());
        return Ok(result);
    }

    [HttpPost("Add")]
    [AccessControll("product-add")]
    public async Task<IActionResult> Add([FromForm] ProductCreateCommand input)
    {
        var result = await mediator.Send(input);
        return Ok(result);
    }
}
