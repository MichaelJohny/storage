using Storage.Domain.Common;

namespace Storage.Domain.Entities;

public class FileMetadata : Entity<Guid> , ISoftDelete
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string Category { get; set; }
    public DateTime UploadDate { get; set; }
    public string UploadedBy { get; set; }
    public string StoragePath { get; set; }
    public string Version { get; set; }
    public bool IsDeleted { get; set; }
}