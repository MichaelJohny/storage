using Microsoft.EntityFrameworkCore;
using Storage.Domain.Entities;

namespace Storage.Application.Common.Interfaces;

public interface IStorageDbContext
{
    public DbSet<FileMetadata> Files { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}