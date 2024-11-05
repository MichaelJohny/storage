using Microsoft.EntityFrameworkCore;
using Storage.Application.Common.Interfaces;
using Storage.Domain.Entities;

namespace Storage.Persistence;

public class StorageDbContext : DbContext , IStorageDbContext
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;

    public StorageDbContext()
    {
        
    }

    public StorageDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public StorageDbContext(DbContextOptions options , ICurrentUserService currentUserService,IDateTime dateTime )
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }

    public DbSet<FileMetadata> Files { get; set; }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.DetectChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

}