namespace Storage.Application.Features.Dtos;

public class FileDownloadResultDto
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public byte[] FileData { get; set; }
}