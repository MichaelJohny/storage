using Microsoft.AspNetCore.Http;

namespace Storage.Application.Common.Events;

public class SaveFileEvent
{
    public IFormFile FileData { get; }
    public string FolderName { get; }
}