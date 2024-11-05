using Storage.Application.Common.Mapping;
using Storage.Domain.Entities;

namespace Storage.Application.Features.Dtos;

public class FileMetadataDto : IMapFrom<FileMetadata>
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string Category { get; set; }
    public DateTime UploadDate { get; set; }
    public string UploadedBy { get; set; }
    public string StoragePath { get; set; }
    public string Version { get; set; }
}