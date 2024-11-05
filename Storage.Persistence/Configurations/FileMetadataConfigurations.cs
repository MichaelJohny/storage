using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Domain.Entities;

namespace Storage.Persistence.Configurations;

public class FileMetadataConfigurations : IEntityTypeConfiguration<FileMetadata>
{
    public void Configure(EntityTypeBuilder<FileMetadata> builder)
    {
        
        builder.ToTable("Cities");
        builder.HasKey(a => a.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}