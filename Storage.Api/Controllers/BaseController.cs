using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Storage.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private readonly IMediator _mediator;
    protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();
}