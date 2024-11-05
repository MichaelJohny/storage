using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Features.Commands;
using Storage.Application.Features.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Storage.Api.Controllers;

public class StorageController : BaseController
{
    [HttpPost("upload")]
    public async Task UploadFile([FromForm] IFormFile file)
        => Ok(await Mediator.Send(new UploadFileCommand(file, string.Empty)));

    [HttpGet("download")]
    [AllowAnonymous]
    public async Task<IActionResult> DownloadImage([FromQuery] DownloadFileQuery query)
    {
        var file = await Mediator.Send(query);
        return File(file.FileData, file.ContentType, file.Name);
    }

    [HttpGet("files/{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
        => Ok(await Mediator.Send(new GetFileQueryById(id)));

    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] GetFilesQuery query)
        => Ok(await Mediator.Send(query));

}